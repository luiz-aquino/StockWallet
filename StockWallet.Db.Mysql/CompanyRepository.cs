using StockWallet.Domain.Infraestructure;
using StockWallet.Domain.Models;

namespace StockWallet.Db.Mysql;

public class CompanyRepository: ICompanyRepository
{
    public void Dispose()
    {
        // TODO release managed resources here
    }

    public Company Get(int id)
    {
        throw new NotImplementedException();
    }

    public (bool success, string error) Remove(int id)
    {
        throw new NotImplementedException();
    }

    public (bool success, string error) Insert(Company item)
    {
        throw new NotImplementedException();
    }

    public List<Company> All()
    {
        throw new NotImplementedException();
    }
}