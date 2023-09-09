using StockWallet.Domain.Models;

namespace StockWallet.Domain.Infraestructure;

public interface ICompanyRepository
{
    Task<(Company company, string error)> Get(int id);
    Task<(bool success, string error)> Remove(int id);
    
    Task<(bool success, string error)> Insert(Company item);
    Task<(List<Company> companies, string error)> All();
}