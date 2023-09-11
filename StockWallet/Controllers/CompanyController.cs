using Microsoft.AspNetCore.Mvc;
using StockWallet.Domain.Models.Dtos;
using StockWallet.Domain.Services.Interfaces;
using StockWallet.Models;

namespace StockWallet.Controllers;

[ApiController, Route("[controller]")]
public class CompanyController : ControllerBase
{
    private readonly ICompanyService _companyService;

    public CompanyController(ICompanyService companyService)
    {
        _companyService = companyService;
    }

    [HttpGet, Route("all"), Produces("application/json", Type = typeof(List<CompanyDto>))]
    public async Task<IActionResult> Get()
    { 
        (List<CompanyDto> companies, string _) = await _companyService.All();
        
        return Ok(companies);
    }

    [HttpGet, Route("id/{id}"), Produces("application/json", Type = typeof(CompanyDto))]
    public async Task<IActionResult> Get(int id)
    {
        (CompanyDto company, string _) = await _companyService.Get(id);

        return Ok(company);
    }

    [HttpPost, Produces("application/Json", Type = typeof(InsertResult))]
    public async Task<IActionResult> Post([FromBody] CompanyDto company)
    {
        if (!ModelState.IsValid) return BadRequest();
        
        (bool success, string error) = await _companyService.Insert(company);

        var result = new InsertResult { Id = company.CompanyId, Error = error, Success = success };

        return success ? Ok(result) : BadRequest(result);
    }

    [HttpDelete, Route("id/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        (bool success, string error) = await _companyService.Remove(id);

        return success ? Ok() : BadRequest(error);
    }
}