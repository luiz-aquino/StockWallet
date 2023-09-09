using StockWallet.Domain.Infraestructure;
using StockWallet.Domain.Models;

namespace StockWallet.Db.Mysql;

public class EventRepository: IEventRepository
{
    public void Dispose()
    {
        // TODO release managed resources here
    }

    public StockEvent Get(int id)
    {
        throw new NotImplementedException();
    }

    public List<StockEvent> AllWallet(int id)
    {
        throw new NotImplementedException();
    }

    public List<StockEvent> AllCompany(int id)
    {
        throw new NotImplementedException();
    }

    public (bool success, string error) Insert(StockEvent item)
    {
        throw new NotImplementedException();
    }

    public (bool success, string error) Delete(int id)
    {
        throw new NotImplementedException();
    }
}