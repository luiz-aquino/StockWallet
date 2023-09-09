using StockWallet.Domain.Infraestructure;
using StockWallet.Domain.Services.Interfaces;

namespace StockWallet.Domain.Services;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _companyRepository;
    
    public CompanyService(ICompanyRepository repository)
    {
        _companyRepository = repository;
    }

}