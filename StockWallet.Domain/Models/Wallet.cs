using StockWallet.Domain.Models.Serivces;

namespace StockWallet.Domain.Models;

public class Wallet
{
    public int WalletId { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<StockEvent>? StockEvents { get; set; }
    public ICollection<StockSummary>? Summaries { get; set; }
}