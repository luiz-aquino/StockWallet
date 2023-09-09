using Microsoft.EntityFrameworkCore;
using StockWallet.Domain.Infraestructure;
using StockWallet.Domain.Models.Serivces;

namespace StockWallet.Db.Mysql.Repositories;

public class SummaryRepository: ISummaryRepository
{
    private readonly StockWalletContext _context;

    public SummaryRepository(StockWalletContext context)
    {
        _context = context;
    }

    public async Task<(StockSummary summary, string error)> Get(int id)
    {
        string error = string.Empty;
        StockSummary? summary = null;
        
        try
        {
            summary = await _context.Summaries.FirstOrDefaultAsync(x => x.SummaryId == id);
            if (summary == null) error = "Item not found";
        }
        catch (Exception e)
        {
            error = e.Message;
        }

        return (summary ?? new StockSummary(), error);
    }

    public async Task<(List<StockSummary> summaries, string error)> All(int walletId)
    {
        string error = string.Empty;
        var summaries = new List<StockSummary>();

        try
        {
            summaries = await _context.Summaries.Where(x => x.WalletId == walletId)
                .OrderByDescending(x => x.CompanyId)
                .ThenBy(x => x.CreatedAt)
                .GroupBy(x => x.CompanyId)
                .Select(x => x.First())
                .ToListAsync();
        }
        catch (Exception e)
        {
            error = e.Message;
        }
        
        return (summaries, error);
    }

    public async Task<(bool success, string error)> Insert(StockSummary item)
    {
        string error = string.Empty;

        try
        {
            _context.Summaries.Attach(item);

            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            error = e.Message;
        }

        return (string.IsNullOrEmpty(error), error);
    }
}