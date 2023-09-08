using StockWallet.Domain.Models.Contols;

namespace StockWallet.Domain.Models;

public class StockEvent
{
    public int EventId { get; set; }
    public int WalletId { get; set; }
    public int CompanyId { get; set; }
    public EventType EventType { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    public Wallet? Wallet { get; set; }
    public Company? Company { get; set; }
}