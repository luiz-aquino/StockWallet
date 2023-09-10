using StockWallet.Domain.Infraestructure;
using StockWallet.Domain.Models;
using StockWallet.Domain.Models.Dtos;
using StockWallet.Domain.Services.Interfaces;

namespace StockWallet.Domain.Services;

public class EventService: IEventService
{
    private readonly IEventRepository _eventRepository;
    public EventService(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<(StockEventDto stockEvent, string error)> Get(int id)
    {
        (StockEvent stockEvent, string error) = await _eventRepository.Get(id);

        var dto = new StockEventDto();

        if (!string.IsNullOrEmpty(error)) return (dto, error);

        dto.EventId = stockEvent.EventId;
        dto.EventType = stockEvent.EventType;
        dto.Price = stockEvent.Price;
        dto.Quantity = stockEvent.Quantity;
        
        return (dto, error);
    }

    public async Task<(List<StockEventDto> stockEvents, string error)> AllWallet(int id)
    {
        (List<StockEvent> stockEvents, string error) = await _eventRepository.AllWallet(id);

        var dtos = new List<StockEventDto>(stockEvents.Count);

        if (!string.IsNullOrEmpty(error)) return (dtos, error);
        
        dtos.AddRange(stockEvents.Select(x => new StockEventDto
        {
            EventId = x.EventId,
            EventType = x.EventType,
            Price = x.Price,
            Quantity = x.Quantity
        }));
        
        return (dtos, error);
    }

    public async Task<(List<StockEventDto> stockEvents, string error)> AllCompany(int id)
    {
        (List<StockEvent> stockEvents, string error) = await _eventRepository.AllCompany(id);

        var dtos = new List<StockEventDto>(stockEvents.Count);

        if (!string.IsNullOrEmpty(error)) return (dtos, error);
        
        dtos.AddRange(stockEvents.Select(x => new StockEventDto
        {
            EventId = x.EventId,
            EventType = x.EventType,
            Price = x.Price,
            Quantity = x.Quantity
        }));
        
        return (dtos, error);
    }

    public async Task<(bool success, string error)> Insert(StockEventDto item)
    {
        var stockEvent = new StockEvent
        {
            EventType = item.EventType,
            Price = item.Price,
            Quantity = item.Quantity
        };
        
        (bool success, string error) = await _eventRepository.Insert(stockEvent);

        if (success) item.EventId = stockEvent.EventId;
        
        return (success, error);
    }

    public Task<(bool success, string error)> Delete(int id)
    {
        return _eventRepository.Delete(id);
    }
}