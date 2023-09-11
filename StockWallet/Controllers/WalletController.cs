using Microsoft.AspNetCore.Mvc;
using StockWallet.Domain.Models.Dtos;
using StockWallet.Domain.Services.Interfaces;
using StockWallet.Models;

namespace StockWallet.Controllers;

[ApiController, Route("[controller]")]
public class WalletController : ControllerBase
{
    private readonly IWalletService _walletService;

    public WalletController(IWalletService walletService)
    {
        _walletService = walletService;
    }

    [HttpGet, Route("all"), Produces("application/json", Type = typeof(List<WalletDto>))]
    public async Task<IActionResult> Get()
    {
        (List<WalletDto> wallets, string _) = await _walletService.All();
        
        return Ok(wallets);
    }

    [HttpGet, Route("id/{id}"), Produces("application/json", Type = typeof(WalletDto))]
    public async Task<IActionResult> Get(int id)
    {
        (WalletDto wallet, string _) = await _walletService.Get(id);

        return Ok(wallet);
    }

    [HttpPost, Produces("application/json", Type = typeof(InsertResult))]
    public async Task<IActionResult> Post([FromBody] WalletDto wallet)
    {
        if (!ModelState.IsValid) return BadRequest();
        
        (bool success, string error) = await _walletService.Insert(wallet);

        var result = new InsertResult { Success = success, Error = error, Id = wallet.WalletId };

        return success ? Ok(result) : BadRequest(result);
    }

    [HttpDelete, Route("id/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        (bool success, string error) = await _walletService.Delete(id);

        return success ? Ok() : BadRequest(error);
    }
}