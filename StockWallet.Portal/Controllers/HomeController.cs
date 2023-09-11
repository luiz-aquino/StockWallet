using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StockWallet.Portal.Models;
using StockWallet.Portal.Models.Dtos;

namespace StockWallet.Portal.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly HttpClient _apiClient;
    public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpFactory)
    {
        _logger = logger;
        _apiClient = httpFactory.CreateClient("walletApi");
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var wallets = new List<WalletDto>(0);

        try
        {
            var walletsResult = await _apiClient.GetAsync("/Wallet/all");

            if (walletsResult.IsSuccessStatusCode)
            {
                wallets = await walletsResult.Content.ReadFromJsonAsync<List<WalletDto>>() ?? new List<WalletDto>(0);
            }
        }
        catch (Exception e)
        {
            _logger.LogError("Falha listar carteiras", e);
        }
        
        var totals = new List<WalletTotal>(wallets.Count);

        try
        {
            foreach (var wallet in wallets)
            {
                var summaryResult = await _apiClient.GetAsync($"/Summary/wallet/id/{wallet.WalletId}");

                var walletTotal = new WalletTotal { WalletId = wallet.WalletId, Name = wallet.Name };
                totals.Add(walletTotal);

                if (!summaryResult.IsSuccessStatusCode) continue;

                var summaries = await summaryResult.Content.ReadFromJsonAsync<List<SummaryDto>>() ??
                                new List<SummaryDto>();

                walletTotal.Total = Math.Round(summaries.Sum(x => x.Total), 2, MidpointRounding.ToEven);
            }
        }
        catch (Exception e)
        {
            _logger.LogError("Falha listar consolidado", e);
        }
        
        return View(totals);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}