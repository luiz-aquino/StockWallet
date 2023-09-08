using StockWallet.Domain.Models.Serivces;

namespace StockWallet.Domain.Infraestructure;

public interface ISummaryRepository
{
    StockSummary Get(int id);
    List<StockSummary> All(int walletId);

    (bool success, string error) Insert(StockSummary item);
}