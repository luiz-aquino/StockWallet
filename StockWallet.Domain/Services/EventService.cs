using StockWallet.Domain.Infraestructure;
using StockWallet.Domain.Models;
using StockWallet.Domain.Services.Interfaces;

namespace StockWallet.Domain.Services;

public class EventService: IEventService
{
    private readonly IEventRepository _eventRepository;
    public EventService(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public Task<(StockEvent stockEvent, string error)> Get(int id)
    {
        return _eventRepository.Get(id);
    }

    public Task<(List<StockEvent> stockEvents, string error)> AllWallet(int id)
    {
        return _eventRepository.AllWallet(id);
    }

    public Task<(List<StockEvent> stockEvents, string error)> AllCompany(int id)
    {
        return _eventRepository.AllCompany(id);
    }

    public Task<(bool success, string error)> Insert(StockEvent item)
    {
        return _eventRepository.Insert(item);
    }

    public Task<(bool success, string error)> Delete(int id)
    {
        return _eventRepository.Delete(id);
    }
}