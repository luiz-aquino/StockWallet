var wallet = function () {

    let api = {
        routes: {
            all: "/Wallet/All",
            add: "/Wallet/Insert",
            rm: "/Wallet/Delete",
            detail: "/Wallet/Detalhar"
        },
        loadingModal: new bootstrap.Modal(document.getElementById('mdLoading'), {}),
        infoModal: new bootstrap.Modal(document.getElementById('mdInfo'), {}),
        addModal: new bootstrap.Modal(document.getElementById('mdNovo'), {}),
        confirmModel: new bootstrap.Modal(document.getElementById('mdConfirm'), {}),
        init: function () {
            this.ui.init();
            this.events.init();

            api.methods.loadWallets();
        },
        ui: {
            init: function () {

            }
        },
        events: {
            init: function () {
                this.onClickAdd();
                this.onClickSave();
                this.onClickDelete();
                this.onClickDetail();
            },
            onClickAdd: function () {
                $('#btnAdd').off('click').on('click', function () {
                    api.addModal.show();
                    api.methods.clearForm();
                })
            },
            onClickSave: function () {
                $('#btnSave').off('click').on('click', function () {

                    api.addModal.hide();
                    api.loadingModal.show();

                    let walletName = $('#walletName').val();

                    $.ajax({
                        url: api.routes.add,
                        type: 'POST',
                        contentType: 'application/json; charset=UTF-8',
                        data: JSON.stringify({name: walletName}),
                        success: function () {
                            api.methods.clearTable();
                            api.methods.loadWallets();
                        },
                        error: function (error) {
                            console.log('error', error)
                        },
                        complete: function () {
                            setTimeout(function () {
                                api.loadingModal.hide();
                            }, 600);

                            api.methods.clearForm();
                        }
                    })
                });
            },
            onClickDelete: function () {
                $('#btnRemove').off('click').on('click', function () {
                    
                    let inputSelected = $('#tbWallets input:checked');
                    if(!inputSelected.val()) return;
                    
                    let wallet = inputSelected.closest('tr').find('.tdName').text();

                    $('#mdConfirm .message').html('Deseja realmente remover ' + wallet + ' ?');
                    
                    api.confirmModel.show();

                    $('#mdConfirm .btnConfirm').off('click').on('click', function () {
                        api.confirmModel.hide();
                        
                        $.ajax({
                            url: api.routes.rm + '/' + inputSelected.data('id'),
                            type: 'POST',
                            contentType: 'application/json; charset=UTF-8;',
                            start: function (){                                
                                api.loadingModal.show();
                            },
                            success: function (){
                                api.methods.clearTable();
                                api.methods.loadWallets();
                            },
                            error: function (error) {
                                console.log('error', error)
                            },
                            complete: function () {
                                setTimeout(function () {
                                    api.loadingModal.hide();
                                }, 600);
                            }
                        })
                    });
                    
                });
            },
            onClickDetail: function () {
                $('#btnDisplay').off('click').on('click', function (){
                    let inputSelected = $('#tbWallets input:checked');
                    if(!inputSelected.val()) return;

                    location.href = api.routes.detail + '/' + inputSelected.data('id');
                });
            }
        },
        methods: {
            loadWallets: function () {

                api.loadingModal.show();

                $.ajax({
                    url: api.routes.all,
                    type: 'GET',
                    contentType: 'application/json; charset=UTF-8;',
                    success: function (result) {

                        let sampleRow = $('#tbWallets .sampleRow');

                        for (let i = 0; i < result.length; i++) {
                            let item = result[i];
                            let newRow = sampleRow.clone();

                            $(newRow).removeClass('d-none');
                            $(newRow).removeClass('sampleRow');
                            $(newRow).addClass('walletTr');

                            $('.tdRadio input', newRow).data('id', item.walletId);
                            $('.tdName', newRow).html(item.name);

                            $('#tbWallets tbody').append(newRow);
                        }

                    },
                    error: function (error) {
                        console.log('error', error)
                    },
                    complete: function () {
                        console.log('end function ran')
                        //Request is to fast and it does not finish opening when hide is called
                        setTimeout(function () {
                            api.loadingModal.hide();
                        }, 600);
                    }
                })
            },
            clearTable: function () {
                $('#tbWallets tbody .walletTr').remove();
            },
            clearForm: function () {
                $('#walletName').val('');
            }
        }
    }

    return {
        init: function () {
            api.init();
        }
    }
}();

$(function () {
    wallet.init();
})