using StockWallet.Domain.Models.Serivces;

namespace StockWallet.Domain.Services.Interfaces;

public interface ISummaryService
{
    Task<(StockSummary summary, string error)> Get(int id);
    Task<(List<StockSummary> summaries, string error)> All(int walletId);
    Task<(bool success, string error)> Insert(StockSummary item);
}