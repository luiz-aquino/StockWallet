using StockWallet.Domain.Infraestructure;
using StockWallet.Domain.Models;

namespace StockWallet.Db.Mysql;

public class WalletRepository: IWalletRepository
{
    public void Dispose()
    {
        // TODO release managed resources here
    }

    public Wallet Get(int id)
    {
        throw new NotImplementedException();
    }

    public (bool success, string error) Delete(int id)
    {
        throw new NotImplementedException();
    }

    public List<Wallet> All()
    {
        throw new NotImplementedException();
    }

    public (bool success, string error) Insert(Wallet item)
    {
        throw new NotImplementedException();
    }
}