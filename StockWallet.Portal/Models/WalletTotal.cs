namespace StockWallet.Portal.Models;

public class WalletTotal
{
    public int WalletId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Total { get; set; }
}