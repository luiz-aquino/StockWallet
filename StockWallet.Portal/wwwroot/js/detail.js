var detail = (function (){
    
    let walletId = $('#walletId').val();    
    let api = {
        walletId: walletId,
        routes: {
            summary: "/Summary/Wallet/" + walletId,
            events: "/StockEvent/Wallet/" + walletId,
            companies: "/Company/All",
            summaryuRun: "/Summary/Process"
        },
        companies: [],
        loadingModal: new bootstrap.Modal(document.getElementById('mdLoading'), {}),
        init: function () {
            this.ui.init();
            this.events.init();
            this.methods.loadCompanies();
            this.methods.loadSummaries();
            this.methods.loadEvents();
        },
        ui: {
            init: function () {
                
            }
        },
        events: {
            init: function () {
                this.onClickConsolidate();
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

                        api.methods.updateSummaryCompany();
                        api.methods.updateEventCompany();
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