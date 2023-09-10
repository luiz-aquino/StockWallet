using StockWallet.Domain.Models;
using StockWallet.Domain.Models.Serivces;

namespace StockWallet.Domain.Infraestructure;

public interface IEventRepository
{
    Task<(StockEvent stockEvent, string error)> Get(int id);
    Task<(List<StockEvent> stockEvents, string error)> AllWallet(int id);
    Task<(List<StockEvent> stockEvents, string error)> AllCompany(int id);

    Task<(bool success, string error)> Insert(StockEvent item);
    Task<(bool success, string error)> Delete(int id);
    Task<(List<int> companies, string error)> WalletCompanies(int walletId);
    Task<(List<StockEvent> stockEvents, string error)> WalletEvents(int walletId, List<StockEventFilter> filters);
}