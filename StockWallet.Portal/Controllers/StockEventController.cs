using Microsoft.AspNetCore.Mvc;
using StockWallet.Portal.Models.Dtos;

namespace StockWallet.Portal.Controllers;

public class StockEventController : Controller
{
    private readonly HttpClient _apiClient;
    
    public StockEventController(IHttpClientFactory httpFactory)
    {
        _apiClient = httpFactory.CreateClient("walletApi");
    }
    // GET
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Wallet(int id)
    {
        var result = await _apiClient.GetAsync($"StockEvent/wallet/id/{id}");

        if (!result.IsSuccessStatusCode) return BadRequest();

        var stockEvents = await result.Content.ReadFromJsonAsync<StockEventDto>();

        return Ok(stockEvents);
    }
    
    [HttpGet]
    public async Task<IActionResult> Company(int id)
    {
        var result = await _apiClient.GetAsync($"StockEvent/company/id/{id}");

        if (!result.IsSuccessStatusCode) return BadRequest();

        var stockEvents = await result.Content.ReadFromJsonAsync<StockEventDto>();

        return Ok(stockEvents);
    }

    [HttpGet]
    public async Task<IActionResult> Insert(StockEventDto eventDto)
    {
        var result = await _apiClient.PostAsJsonAsync("StockEvent", eventDto);

        if (result.IsSuccessStatusCode) return Ok();

        var message = await result.Content.ReadAsStringAsync();

        return BadRequest(message);
    }
}