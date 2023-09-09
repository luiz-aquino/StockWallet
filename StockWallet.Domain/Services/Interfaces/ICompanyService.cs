using StockWallet.Domain.Models;

namespace StockWallet.Domain.Services.Interfaces;

public interface ICompanyService
{
    Task<(Company company, string error)> Get(int id);
    Task<(bool success, string error)> Remove(int id);
    Task<(bool success, string error)> Insert(Company item);
    Task<(List<Company> companies, string error)> All();
}