using Microsoft.AspNetCore.Mvc;
using StockWallet.Portal.Models.Dtos;

namespace StockWallet.Portal.Controllers;

public class CompanyController : Controller
{
    private readonly HttpClient _apiClient;
    
    public CompanyController(IHttpClientFactory httpFactory)
    {
        _apiClient = httpFactory.CreateClient("walletApi");
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> All()
    {
        var result = await _apiClient.GetAsync("Company/all");

        if (!result.IsSuccessStatusCode) return BadRequest();

        var companies = await result.Content.ReadFromJsonAsync<List<CompanyDto>>();

        return Ok(companies);
    }

    [HttpGet]
    public async Task<IActionResult> Find(int id)
    {
        var result = await _apiClient.GetAsync($"Company/id/{id}");

        if (!result.IsSuccessStatusCode) return BadRequest();

        var company = await result.Content.ReadFromJsonAsync<CompanyDto>();

        return Ok(company);
    }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] CompanyDto item)
    {
        var result = await _apiClient.PostAsJsonAsync("Company", item);

        if (result.IsSuccessStatusCode)
        {
            var insertResult = await result.Content.ReadFromJsonAsync<InsertResult>();
            
            return Ok(insertResult);
        }

        var message = result.Content.ReadAsStringAsync();

        return BadRequest(message);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _apiClient.DeleteAsync($"Company/Delete/{id}");

        if (result.IsSuccessStatusCode) return Ok();

        var message = result.Content.ReadAsStringAsync();

        return BadRequest(message);
    }
    
}