using StockWallet.Domain.Infraestructure;
using StockWallet.Domain.Models.Serivces;

namespace StockWallet.Db.Mysql;

public class SummaryRepository: ISummaryRepository
{
    public void Dispose()
    {
        // TODO release managed resources here
    }

    public StockSummary Get(int id)
    {
        throw new NotImplementedException();
    }

    public List<StockSummary> All(int walletId)
    {
        throw new NotImplementedException();
    }

    public (bool success, string error) Insert(StockSummary item)
    {
        throw new NotImplementedException();
    }
}