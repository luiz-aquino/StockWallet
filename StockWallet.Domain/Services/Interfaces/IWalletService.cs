using StockWallet.Domain.Models;

namespace StockWallet.Domain.Services.Interfaces;

public interface IWalletService
{
    Task<(Wallet wallet, string error)> Get(int id);
    Task<(bool success, string error)> Delete(int id);
    Task<(List<Wallet> wallets, string error)> All();
    Task<(bool success, string error)> Insert(Wallet item);
}