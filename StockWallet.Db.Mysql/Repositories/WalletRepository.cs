using Microsoft.EntityFrameworkCore;
using StockWallet.Domain.Infraestructure;
using StockWallet.Domain.Models;

namespace StockWallet.Db.Mysql.Repositories;

public class WalletRepository: IWalletRepository
{
    private readonly StockWalletContext _context;
    
    public WalletRepository(StockWalletContext context)
    {
        _context = context;
    }

    public async Task<(Wallet wallet, string error)> Get(int id)
    {
        string error = string.Empty;
        Wallet? wallet = null;

        try
        {
            wallet = await _context.Wallets.FirstOrDefaultAsync(x => x.WalletId == id);

            if (wallet == null) error = "Item not found";
        }
        catch (Exception e)
        {
            error = e.Message;
        }
            
        return (wallet ?? new Wallet(), error);
    }

    public async Task<(bool success, string error)> Delete(int id)
    {
        string error = string.Empty;

        try
        {
            Wallet? wallet = await _context.Wallets.FirstOrDefaultAsync(x => x.WalletId == id);

            if (wallet != null) _context.Wallets.Remove(wallet);

            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            error = e.Message;
        }

        return (string.IsNullOrEmpty(error), error);
    }

    public async Task<(List<Wallet> wallets, string error)> All()
    {
        string error = string.Empty;
        var wallets = new List<Wallet>(0);

        try
        {
            wallets = await _context.Wallets.ToListAsync();
        }
        catch (Exception e)
        {
            error = e.Message;
        }
        
        return (wallets, error);
    }

    public async Task<(bool success, string error)> Insert(Wallet item)
    {
        string error = string.Empty;

        try
        {
            _context.Wallets.Attach(item);

            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            error = e.Message;
        }

        return (string.IsNullOrEmpty(error), error);
    }
}