using StockWallet.Domain.Infraestructure;
using StockWallet.Domain.Models.Serivces;
using StockWallet.Domain.Services.Interfaces;

namespace StockWallet.Domain.Services;

public class SummaryService: ISummaryService
{
    private readonly ISummaryRepository _summaryRepository;
    
    public SummaryService(ISummaryRepository summaryRepository)
    {
        _summaryRepository = summaryRepository;
    }

    public Task<(StockSummary summary, string error)> Get(int id)
    {
        return _summaryRepository.Get(id);
    }

    public Task<(List<StockSummary> summaries, string error)> All(int walletId)
    {
        return _summaryRepository.All(walletId);
    }

    public Task<(bool success, string error)> Insert(StockSummary item)
    {
        return _summaryRepository.Insert(item);
    }
}