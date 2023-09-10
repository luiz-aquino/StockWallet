using StockWallet.Domain.Models;
using StockWallet.Domain.Models.Dtos;

namespace StockWallet.Domain.Services.Interfaces;

public interface IEventService
{
    Task<(StockEventDto stockEvent, string error)> Get(int id);
    Task<(List<StockEventDto> stockEvents, string error)> AllWallet(int id);
    Task<(List<StockEventDto> stockEvents, string error)> AllCompany(int id);
    Task<(bool success, string error)> Insert(StockEventDto item);
    Task<(bool success, string error)> Delete(int id);
}