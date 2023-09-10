using Microsoft.AspNetCore.Mvc;
using StockWallet.Domain.Models.Dtos;
using StockWallet.Domain.Services.Interfaces;

namespace StockWallet.Controllers;

[ApiController, Route("[controller]")]
public class SummaryController : ControllerBase
{
    private readonly ISummaryService _summaryService;
    
    public SummaryController(ISummaryService summaryService)
    {
        _summaryService = summaryService;
    }
    
    [HttpGet, Route("wallet/id/{id}"), Produces("application/json", Type = typeof(List<SummaryDto>))]
    public async Task<IActionResult> Get(int id)
    {
        (List<SummaryDto> summaries, string _) = await _summaryService.All(id);
        
        return Ok(summaries);
    }
}