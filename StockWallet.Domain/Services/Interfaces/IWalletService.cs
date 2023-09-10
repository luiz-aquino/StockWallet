using StockWallet.Domain.Models.Dtos;

namespace StockWallet.Domain.Services.Interfaces;

public interface IWalletService
{
    Task<(WalletDto wallet, string error)> Get(int id);
    Task<(bool success, string error)> Delete(int id);
    Task<(List<WalletDto> wallets, string error)> All();
    Task<(bool success, string error)> Insert(WalletDto item);
}