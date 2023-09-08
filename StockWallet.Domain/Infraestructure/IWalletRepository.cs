using StockWallet.Domain.Models;

namespace StockWallet.Domain.Infraestructure;

public interface IWalletRepository
{
    Wallet Get(int id);
    (bool success, string error) Delete(int id);

    List<Wallet> All();
    (bool success, string error) Insert(Wallet item);
}