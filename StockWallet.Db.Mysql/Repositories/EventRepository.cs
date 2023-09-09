using Microsoft.EntityFrameworkCore;
using StockWallet.Domain.Infraestructure;
using StockWallet.Domain.Models;

namespace StockWallet.Db.Mysql.Repositories;

public class EventRepository: IEventRepository
{
    private readonly StockWalletContext _context;
    
    public EventRepository(StockWalletContext context)
    {
        _context = context;
    }

    public async Task<(StockEvent stockEvent, string error)> Get(int id)
    {
        string error = string.Empty;
        StockEvent? stockEvent = null;
        
        try
        {
            stockEvent = await _context.StockEvents.FirstOrDefaultAsync(x => x.EventId == id);

            if (stockEvent == null) error = "Item not found";
        }
        catch (Exception e)
        {
            error = e.Message;
        }

        return (stockEvent ?? new StockEvent(), error);
    }

    public async Task<(List<StockEvent> stockEvents, string error)> AllWallet(int id)
    {
        string error = string.Empty;
        var stockEvents = new List<StockEvent>(0);

        try
        {
            stockEvents = await _context.StockEvents.Where(x => x.WalletId == id).ToListAsync();
        }
        catch (Exception e)
        {
            error = e.Message;
        }
        
        return (stockEvents, error);
    }

    public async Task<(List<StockEvent> stockEvents, string error)> AllCompany(int id)
    {
        string error = string.Empty;
        var stockEvents = new List<StockEvent>(0);

        try
        {
            stockEvents = await _context.StockEvents.Where(x => x.CompanyId == id).ToListAsync();
        }
        catch (Exception e)
        {
            error = e.Message;
        }

        return (stockEvents, error);
    }

    public async Task<(bool success, string error)> Insert(StockEvent item)
    {
        string error = string.Empty;

        try
        {
            _context.StockEvents.Attach(item);

            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            error = e.Message;
        }

        return (string.IsNullOrEmpty(error), error);
    }

    public async Task<(bool success, string error)> Delete(int id)
    {
        string error = string.Empty;

        try
        {
            var stockEvent = await _context.StockEvents.FirstOrDefaultAsync(x => x.EventId == id);

            if (stockEvent != null) _context.StockEvents.Remove(stockEvent);

            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            error = e.Message;
        }

        return (string.IsNullOrEmpty(error), error);
    }
}