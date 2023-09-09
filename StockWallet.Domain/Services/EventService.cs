using StockWallet.Domain.Infraestructure;
using StockWallet.Domain.Services.Interfaces;

namespace StockWallet.Domain.Services;

public class EventService: IEvenService
{
    private readonly IEventRepository _eventRepository;
    public EventService(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

}