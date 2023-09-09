using Microsoft.AspNetCore.Mvc;
using StockWallet.Domain.Services.Interfaces;

namespace StockWallet.Controllers;

[ApiController, Route("[controller]")]
public class StockEventController : ControllerBase
{
    private readonly IEventService _eventService;
    
    public StockEventController(IEventService stockEventService)
    {
        _eventService = stockEventService;
    }
    
    // GET
    public IActionResult Index()
    {
        return Ok();
    }
}