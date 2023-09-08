using StockWallet.Domain.Models;

namespace StockWallet.Domain.Infraestructure;

public interface IEventRepository: IDisposable
{
    StockEvent Get(int id);
    List<StockEvent> AllWallet(int id);
    List<StockEvent> AllCompany(int id);

    (bool success, string error) Insert(StockEvent item);
    (bool success, string error) Delete(int id);
}