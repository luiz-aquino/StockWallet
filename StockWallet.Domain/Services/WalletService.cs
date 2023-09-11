using StockWallet.Domain.Infraestructure;
using StockWallet.Domain.Models;
using StockWallet.Domain.Models.Dtos;
using StockWallet.Domain.Services.Interfaces;

namespace StockWallet.Domain.Services;

public class WalletService: IWalletService
{
    private readonly IWalletRepository _walletRepository;

    public WalletService(IWalletRepository repository)
    {
        _walletRepository = repository;
    }

    public async Task<(WalletDto wallet, string error)> Get(int id)
    {
        (Wallet wallet, string error) = await _walletRepository.Get(id);

        var dto = new WalletDto();

        if (!string.IsNullOrEmpty(error)) return (dto, error);

        dto.WalletId = wallet.WalletId;
        dto.Name = wallet.Name;
        
        return (dto, error);
    }

    public Task<(bool success, string error)> Delete(int id)
    {
        return _walletRepository.Delete(id);
    }

    public async Task<(List<WalletDto> wallets, string error)> All()
    {
        (List<Wallet> wallets, string error) = await _walletRepository.All();

        var dtos = new List<WalletDto>(wallets.Count);
        
        if (!string.IsNullOrEmpty(error)) return (dtos, error);
        
        dtos.AddRange(wallets.Select(x => new WalletDto
        {
            WalletId = x.WalletId,
            Name = x.Name
        }));
        
        return (dtos, error);
    }

    public async Task<(bool success, string error)> Insert(WalletDto item)
    {
        var wallet = new Wallet
        {
            Name = item.Name
        };
        
        (bool success, string error) = await _walletRepository.Insert(wallet);

        if (success) item.WalletId = wallet.WalletId;

        return (success, error);
    }
}