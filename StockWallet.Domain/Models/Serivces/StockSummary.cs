namespace StockWallet.Domain.Models.Serivces;

public class StockSummary
{
    public int SummaryId { get; set; }
    public int WalletId { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Quantity { get; set; }
    public decimal AveragePrice { get; set; }

    public List<Wallet>? Wallets { get; set; }
}