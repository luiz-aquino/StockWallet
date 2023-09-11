var detail = (function (){
    
    let walletId = $('#walletId').val();    
    let api = {
        walletId: walletId,
        routes: {
            summary: "/Summary/Wallet/" + walletId,
            events: "/StockEvent/Wallet/" + walletId,
            companies: "/Company/All",
            summaryuRun: "/Summary/Process",
            saveEvent: "/StockEvent/Insert",
            saveCompany: "/Company/Insert"
        },
        companies: [],
        loadingModal: new bootstrap.Modal(document.getElementById('mdLoading'), {}),
        modalAdd: new bootstrap.Modal(document.getElementById('mdNovo'), {}),
        infoModal: new bootstrap.Modal(document.getElementById('mdInfo'), {}),
        modalCompany: new bootstrap.Modal(document.getElementById('mdCompany'), {}),
        selectCompany: 0,
        clearTagsRegex: /<[^>]*(>|$)/g,
        init: function () {
            this.ui.init();
            this.events.init();
            this.methods.loadCompanies();
            this.methods.loadSummaries();
            this.methods.loadEvents();
        },
        ui: {
            init: function () {
                this.inputMask();
            },
            inputMask: function () {
                $('#companyDoc').inputmask('99.999.999/9999-99');
                $('#eventQty').inputmask('integer', { allowMinus: false, min: 1, unmaskAsNumber: true });
                $('#eventPrice').inputmask('currency', { radixPoint: ',', allowMinus: false, min:0.1, unmaskAsNumber: true });
            }
        },
        events: {
            init: function () {
                this.onClickConsolidate();
                this.onClickAdd();
                this.onClickSave();
                this.onClickAddCompany();
                this.onCancelCompany();
                this.onClickSaveCompany();
            },
            onClickConsolidate: function () {
                $('#btnConsolidate').off('click').on('click', function () {
                    api.loadingModal.show();
                    $.ajax({
                        url: api.routes.summaryuRun,
                        type: 'POST',
                        contentType: 'application/json; charset=UTF-8;',
                        success: function () {
                            $('#tbSummaries').addClass('d-none');
                            $('#loading-summary').removeClass('d-none');

                            $('#tbSummaries .summaryTr').remove();
                            
                            api.methods.loadSummaries();
                        },
                        error: function () {
                            
                        },
                        complete: function () {
                            setTimeout(function () {
                                api.loadingModal.hide();
                            }, 600);
                        }
                    })
                });
            },
            onClickAdd: function () {
                $('#btnAdd').off('click').on('click', function (){
                    $('#frmEvent').trigger('reset');
                   api.modalAdd.show();
                });
            },
            onClickSave: function () {
                $('#btnSave').off('click').on('click', function (){
                     
                    if(!api.methods.isEventValid()) return;
                    
                    let eventObj = {
                        walletId: parseInt(api.walletId),
                        companyId: parseInt($('#eventCompany').val()),
                        eventType: parseInt($('#eventType').val()),
                        quantity: $('#eventQty').inputmask('unmaskedvalue'),
                        price: $('#eventPrice').inputmask('unmaskedvalue')
                    };

                    api.modalAdd.hide();
                    api.loadingModal.show();
                    
                    $.ajax({
                        url: api.routes.saveEvent,
                        type: 'POST',
                        contentType: 'application/json; charset=UTF-8;',
                        data: JSON.stringify(eventObj),
                        success: function () {
                            
                            $('#tbEvents').addClass('d-none');
                            $('#loading-event').removeClass('d-none');

                            $('#tbEvents .eventTr').remove();
                            
                            api.methods.loadEvents();
                            
                            $('#mdInfoLabel').html("Aviso");
                            
                            $('#mdInfoBody').html('<p> Novas movimentações precisam ser consolidadas, para refletir no total da aba consolidado. </p>')
                            
                            setTimeout(function (){
                                api.infoModal.show();
                            }, 600);
                        },
                        error: function (error) {
                            console.log('error', error);
                        },
                        complete: function () {
                            setTimeout(function () {
                                api.loadingModal.hide();
                            }, 600);
                        }
                    })
                });
            },
            onClickAddCompany: function () {
                $('#btnAddCompany').off('click').on('click', function (){
                    $('#frmCompany').trigger('reset');
                    api.modalCompany.show();
                    api.modalAdd.hide();
                });
            },
            onCancelCompany: function () {
                $('#btnCompanyDismiss').off('click').on('click', function () {
                    api.modalCompany.hide();
                    api.modalAdd.show();
                });
            },
            onClickSaveCompany: function () {
                $('#btnSaveCompany').off('click').on('click', function () {
                    if(!api.methods.isCompanyValid()) return;
                    
                    api.modalCompany.hide();
                    api.loadingModal.show();
                    
                    let companyObj = {
                        identification: $('#companyDoc').val(),
                        name: $('#companyName').val().replace(api.clearTagsRegex, '')
                    };
                    
                    $.ajax({
                        url: api.routes.saveCompany,
                        type: 'POST',
                        contentType: 'application/json; charset=UTF-8;',
                        data: JSON.stringify(companyObj),
                        success: function (result) {
                            api.methods.loadCompanies();
                            
                            api.selectCompany = result.id;
                            
                            setTimeout(function () {
                                api.loadingModal.hide();
                                api.modalAdd.show();
                            }, 600);
                        },
                        error: function (error) {
                            console.log('error', error)
                        }
                    })
                })
            }
        },
        methods: {
            loadSummaries: function () {
                $.ajax({
                    url: api.routes.summary,
                    type: 'GET',
                    contentType: 'application/json; charset=UTF-8;',
                    success: function (result) {
                        
                        let sampleRow = $('#tbSummaries .sampleRow');
                        
                        for (let i = 0; i < result.length; i++)
                        {
                            let item = result[i];
                            let newRow = sampleRow.clone();

                            $(newRow).removeClass('d-none');
                            $(newRow).removeClass('sampleRow');
                            $(newRow).addClass('summaryTr');

                            $('.tdCompany', newRow).html(item.companyId);
                            $('.tdCompany', newRow).data('id', item.companyId);
                            
                            $('.tdQty', newRow).html(item.quantity.toString());
                            $('.tdAvgPrice', newRow).html(item.averagePrice);
                            $('.tdTotal', newRow).html(item.total);
                            $(newRow).data('id', item.summaryId);

                            $('#tbSummaries tbody').append(newRow);
                        }

                        $('#tbSummaries').removeClass('d-none');
                        $('#loading-summary').addClass('d-none');

                        api.methods.updateSummaryCompany();
                    },
                    error: function () {
                        
                    }
                })
            },
            loadEvents: function () {
                $.ajax({
                    url: api.routes.events,
                    type: 'GET',
                    contentType: 'application/json; charset=UTF-8;',
                    success: function (result) {

                        let sampleRow = $('#tbEvents .sampleRow');
                        
                        for (let i = 0; i < result.length; i++)
                        {
                            let item = result[i];
                            let newRow = sampleRow.clone();

                            $(newRow).removeClass('d-none');
                            $(newRow).removeClass('sampleRow');
                            $(newRow).addClass('eventTr');

                            $('.tdCompany', newRow).html(item.companyId);
                            $('.tdCompany', newRow).data('id', item.companyId);

                            $('.tdQty', newRow).html(item.quantity.toString());
                            $('.tdType', newRow).html(item.eventType ? 'Sell' : 'Buy');
                            $('.tdPrice', newRow).html(item.price);
                            $('.tdTotal', newRow).html(item.total);
                            $(newRow).data('id', item.summaryId);

                            $('#tbEvents tbody').append(newRow);
                        }
                        $('#tbEvents').removeClass('d-none');
                        $('#loading-event').addClass('d-none');

                        api.methods.updateEventCompany();
                    },
                    error: function () {
                        
                    }
                });
            },
            loadCompanies: function () {
                $.ajax({
                    url: api.routes.companies,
                    type: 'GET',
                    contentType: 'application/json; charset=UTF-8',
                    success: function (result) {
                        api.companies = result;

                        for (let i = 0; i < api.companies.length; i++) {
                            let company = api.companies[i];
                            company.name = company.name.replace(api.clearTagsRegex, '');
                        }
                        
                        api.methods.updateSummaryCompany();
                        api.methods.updateEventCompany();
                        api.methods.loadCompanySelect();
                    },
                    error: function () {
                        
                    }
                })
            },
            updateSummaryCompany: function () {
                $('#tbSummaries .summaryTr').each(function () {
                   let companyTd = $('.tdCompany', this);
                   let companyId = companyTd.data('id');
                   
                   for (let i = 0; i < api.companies.length; i++) {
                       let company = api.companies[i];
                       
                       if(company.companyId === companyId)  {
                           companyTd.html(company.name);
                           break;
                       }
                   }
                })
            },
            updateEventCompany: function () {
                $('#tbEvents .eventTr').each(function () {
                    let companyTd = $('.tdCompany', this);
                    let companyId = companyTd.data('id');

                    for (let i = 0; i < api.companies.length; i++) {
                        let company = api.companies[i];

                        if(company.companyId === companyId)  {
                            companyTd.html(company.name);
                            break;
                        }
                    }
                })
            },
            loadCompanySelect: function () {
                let options = '<option value="">Selecione</option>';
                
                for (let i = 0; i < api.companies.length; i++)
                {
                    let company = api.companies[i];
                    options += `<option value="${company.companyId}">${company.name} (${company.identification})</option>>`
                }
                
                let slCompany = $('#eventCompany');
                slCompany.val('')
                slCompany.html(options);
                if(api.selectCompany) slCompany.val(api.selectCompany)
            },
            isEventValid: function () {
                let valid = true;
                
                let company = $('#eventCompany');
                if(company.val()) 
                    company.removeClass("is-invalid");
                else {
                    company.addClass("is-invalid");
                    valid = false;
                }
                
                let qty = $('#eventQty');
                
                if(parseInt(qty.val())) 
                    qty.removeClass("is-invalid");
                else {
                    qty.addClass("is-invalid");
                    valid = false;
                }                
                
                let price = $('#eventPrice');
                
                if(parseFloat(price.val()))
                    price.removeClass("is-invalid");
                else {
                    price.addClass("is-invalid");
                    valid = false;
                }
                
                return valid;
            },
            isCompanyValid: function () {
                let valid = true;
                
                let document = $('#companyDoc');
                
                if(document.val())
                    document.removeClass("is-invalid");
                else {
                    document.addClass("is-invalid");
                    valid = false;
                }
                
                let nome = $('#companyName');
                
                if(nome.val())
                    nome.removeClass("is-invalid");
                else 
                    nome.addClass("is-invalid");
                
                return valid;
            }
        }
    }
    
    return {
        init: function (){
            api.init();
        }
    }
    
})();

$(function (){
    detail.init(); 
});