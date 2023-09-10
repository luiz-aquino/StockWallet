using Microsoft.AspNetCore.Mvc;
using StockWallet.Portal.Models.Dtos;

namespace StockWallet.Portal.Controllers;

public class WalletController : Controller
{
    private readonly HttpClient _apiClient;
    
    public WalletController(IHttpClientFactory httpFactory)
    {
        _apiClient = httpFactory.CreateClient("walletApi");
    }
    // GET
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> All()
    {
        var result = await _apiClient.GetAsync("Wallet/all");

        if (!result.IsSuccessStatusCode) return BadRequest();

        var wallets = await result.Content.ReadFromJsonAsync<List<WalletDto>>();
        
        return Ok(wallets);
    }

    [HttpGet]
    public async Task<IActionResult> Find(int id)
    {
        var result = await _apiClient.GetAsync($"Wallet/id/{id}");
        
        if (!result.IsSuccessStatusCode) return BadRequest();

        WalletDto wallet = await result.Content.ReadFromJsonAsync<WalletDto>() ?? new WalletDto();

        return Ok(wallet);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(WalletDto wallet)
    {
        var result = await _apiClient.PostAsJsonAsync("Wallet", wallet);

        if (result.IsSuccessStatusCode) return Ok();

        var message= await result.Content.ReadAsStringAsync();

        return BadRequest(message);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _apiClient.DeleteAsync($"Wallet/id/{id}");

        if (result.IsSuccessStatusCode) return Ok();

        var message= await result.Content.ReadAsStringAsync();

        return BadRequest(message);
    }
    
}