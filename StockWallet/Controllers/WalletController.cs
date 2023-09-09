using Microsoft.AspNetCore.Mvc;
using StockWallet.Domain.Models;
using StockWallet.Domain.Services.Interfaces;

namespace StockWallet.Controllers;

[ApiController, Route("[controller]")]
public class WalletController : ControllerBase
{
    private readonly IWalletService _walletService;

    public WalletController(IWalletService walletService)
    {
        _walletService = walletService;
    }

    [HttpGet, Route(""), Produces("text/json", Type = typeof(Wallet))]
    public async Task<IActionResult> Get()
    {
        var companies = await _walletService.All();
        return Ok(companies);
    }
    
    // GET
    public IActionResult Index()
    {
        return Ok();
    }
}