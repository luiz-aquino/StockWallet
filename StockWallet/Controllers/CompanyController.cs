using Microsoft.AspNetCore.Mvc;
using StockWallet.Domain.Models;
using StockWallet.Domain.Services.Interfaces;

namespace StockWallet.Controllers;

[ApiController, Route("[controller]")]
public class CompanyController : ControllerBase
{
    private readonly ICompanyService _companyService;

    public CompanyController(ICompanyService companyService)
    {
        _companyService = companyService;
    }

    [HttpGet, Route("all"), Produces("text/json", Type = typeof(Company))]
    public async Task<IActionResult> Get()
    {
        var companies = await _companyService.All();
        return Ok(companies);
    }
    
    // GET
    public IActionResult Index()
    {
        return Ok();
    }
}