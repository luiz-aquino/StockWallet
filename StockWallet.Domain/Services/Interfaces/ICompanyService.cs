using StockWallet.Domain.Models.Dtos;

namespace StockWallet.Domain.Services.Interfaces;

public interface ICompanyService
{
    Task<(CompanyDto company, string error)> Get(int id);
    Task<(bool success, string error)> Remove(int id);
    Task<(bool success, string error)> Insert(CompanyDto item);
    Task<(List<CompanyDto> companies, string error)> All();
}