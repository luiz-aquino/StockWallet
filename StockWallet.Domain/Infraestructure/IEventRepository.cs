using StockWallet.Domain.Models;

namespace StockWallet.Domain.Infraestructure;

public interface IEventRepository
{
    Task<(StockEvent stockEvent, string error)> Get(int id);
    Task<(List<StockEvent> stockEvents, string error)> AllWallet(int id);
    Task<(List<StockEvent> stockEvents, string error)> AllCompany(int id);

    Task<(bool success, string error)> Insert(StockEvent item);
    Task<(bool success, string error)> Delete(int id);
}