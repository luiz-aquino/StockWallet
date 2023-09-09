using Microsoft.AspNetCore.Mvc;
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
    
    // GET
    public IActionResult Index()
    {
        return Ok();
    }
}