using Microsoft.AspNetCore.Mvc;
using StockWallet.Portal.Models.Dtos;

namespace StockWallet.Portal.Controllers;

public class SummaryController : Controller
{
    private readonly HttpClient _apiClient;
    
    public SummaryController(IHttpClientFactory httpFactory)
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
        var result = await _apiClient.GetAsync($"Summary/wallet/id/{id}");

        if (!result.IsSuccessStatusCode) return BadRequest();

        var summaries = await result.Content.ReadFromJsonAsync<List<SummaryDto>>();

        return Ok(summaries);
    }

    [HttpPost]
    public async Task<IActionResult> Process()
    {
        var result = await _apiClient.PostAsync("Summary/process", null);

        if (result.IsSuccessStatusCode) return Ok();

        var message = result.Content.ReadAsStringAsync();

        return Ok(message);
    }
    
}