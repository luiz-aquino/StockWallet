using StockWallet.Domain.Infraestructure;
using StockWallet.Domain.Services.Interfaces;

namespace StockWallet.Domain.Services;

public class WalletService: IWalletService
{
    private readonly IWalletRepository _walletRepository;

    public WalletService(IWalletRepository repository)
    {
        _walletRepository = repository;
    }

    public void Dispose()
    {
        _walletRepository.Dispose();
    }
}