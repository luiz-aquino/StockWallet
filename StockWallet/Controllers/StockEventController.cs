using Microsoft.AspNetCore.Mvc;
using StockWallet.Domain.Models.Dtos;
using StockWallet.Domain.Services.Interfaces;
using StockWallet.Models;

namespace StockWallet.Controllers;

[ApiController, Route("[controller]")]
public class StockEventController : ControllerBase
{
    private readonly IEventService _eventService;
    
    public StockEventController(IEventService stockEventService)
    {
        _eventService = stockEventService;
    }
    
    [HttpGet, Route("wallet/id/{id}"), Produces("application/json", Type = typeof(List<StockEventDto>))]
    public async Task<IActionResult> Wallet(int id)
    {
        (List<StockEventDto> stockEvents, string _) = await _eventService.AllWallet(id);
        
        return Ok(stockEvents);
    }
    
    [HttpGet, Route("company/id/{id}"), Produces("application/json", Type = typeof(List<StockEventDto>))]
    public async Task<IActionResult> Company(int id)
    {
        (List<StockEventDto> stockEvents, string _) = await _eventService.AllCompany(id);
        
        return Ok(stockEvents);
    }

    [HttpPost, Produces("application/json", Type = typeof(InsertResult))]
    public async Task<IActionResult> Post([FromBody] StockEventDto stockEvent)
    {
        (bool success, string error) = await _eventService.Insert(stockEvent);

        var result = new InsertResult { Success = success, Error = error, Id = stockEvent.EventId};
        
        return success ? Ok(result) : BadRequest(result);
    }
}