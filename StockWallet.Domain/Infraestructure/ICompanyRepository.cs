using StockWallet.Domain.Models;

namespace StockWallet.Domain.Infraestructure;

public interface ICompanyRepository: IDisposable
{
    Company Get(int id);
    (bool success, string error) Remove(int id);
    
    (bool success, string error) Insert(Company item);
    List<Company> All();

}