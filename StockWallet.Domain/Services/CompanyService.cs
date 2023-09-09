using StockWallet.Domain.Infraestructure;
using StockWallet.Domain.Models;
using StockWallet.Domain.Services.Interfaces;

namespace StockWallet.Domain.Services;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _companyRepository;
    
    public CompanyService(ICompanyRepository repository)
    {
        _companyRepository = repository;
    }

    public Task<(Company company, string error)> Get(int id)
    {
        return _companyRepository.Get(id);
    }

    public Task<(bool success, string error)> Remove(int id)
    {
        return _companyRepository.Remove(id);
    }

    public Task<(bool success, string error)> Insert(Company item)
    {
        return _companyRepository.Insert(item);
    }

    public Task<(List<Company> companies, string error)> All()
    {
        return _companyRepository.All();
    }
}