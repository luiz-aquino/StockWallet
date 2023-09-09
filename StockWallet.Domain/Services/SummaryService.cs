using StockWallet.Domain.Infraestructure;
using StockWallet.Domain.Services.Interfaces;

namespace StockWallet.Domain.Services;

public class SummaryService: ISummaryService
{
    private readonly ISummaryRepository _summaryRepository;
    
    public SummaryService(ISummaryRepository summaryRepository)
    {
        _summaryRepository = summaryRepository;
    }

}