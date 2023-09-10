using StockWallet.Domain.Infraestructure;
using StockWallet.Domain.Models;
using StockWallet.Domain.Models.Dtos;
using StockWallet.Domain.Services.Interfaces;

namespace StockWallet.Domain.Services;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _companyRepository;
    
    public CompanyService(ICompanyRepository repository)
    {
        _companyRepository = repository;
    }

    public async Task<(CompanyDto company, string error)> Get(int id)
    {
        (Company company, string error) = await _companyRepository.Get(id);

        var dto = new CompanyDto();

        if (!string.IsNullOrEmpty(error)) return (dto, error);

        dto.CompanyId = company.CompanyId;
        dto.Identification = company.Identification;
        dto.Name = company.Name;
        
        return (dto, error);
    }

    public Task<(bool success, string error)> Remove(int id)
    {
        return _companyRepository.Remove(id);
    }

    public async Task<(bool success, string error)> Insert(CompanyDto item)
    {
        var company = new Company
        {
            Identification = item.Identification,
            Name = item.Name
        };
        
        (bool success, string error) = await _companyRepository.Insert(company);

        if(success) item.CompanyId = company.CompanyId;

        return (success, error);
    }

    public async Task<(List<CompanyDto> companies, string error)> All()
    {
        (List<Company> companies, string error) = await _companyRepository.All();

        var dtos = new List<CompanyDto>(companies.Count);

        if (!string.IsNullOrEmpty(error)) return (dtos, error);
        
        dtos.AddRange(companies.Select(x => new CompanyDto
        {
            CompanyId = x.CompanyId,
            Identification = x.Identification,
            Name = x.Name
        }));

        return (dtos, error);
    }
}