namespace StockWallet.Domain.Models;

public class Wallet
{
    public int WalletId { get; set; }
    public string Name { get; set; } = string.Empty;

    public List<StockEvent>? StockEvents { get; set; }
}