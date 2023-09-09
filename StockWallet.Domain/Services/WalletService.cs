using StockWallet.Domain.Infraestructure;
using StockWallet.Domain.Models;
using StockWallet.Domain.Services.Interfaces;

namespace StockWallet.Domain.Services;

public class WalletService: IWalletService
{
    private readonly IWalletRepository _walletRepository;

    public WalletService(IWalletRepository repository)
    {
        _walletRepository = repository;
    }

    public Task<(Wallet wallet, string error)> Get(int id)
    {
        return _walletRepository.Get(id);
    }

    public Task<(bool success, string error)> Delete(int id)
    {
        return _walletRepository.Delete(id);
    }

    public Task<(List<Wallet> wallets, string error)> All()
    {
        return _walletRepository.All();
    }

    public Task<(bool success, string error)> Insert(Wallet item)
    {
        return _walletRepository.Insert(item);
    }
}