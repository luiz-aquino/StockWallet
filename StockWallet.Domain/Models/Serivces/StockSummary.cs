namespace StockWallet.Domain.Models.Serivces;

public class StockSummary
{
    public int SummaryId { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Quantity { get; set; }
    public decimal AveragePrice { get; set; }

    public int WalletId { get; set; }
    public Wallet? Wallet { get; set; }
    
    public int CompanyId { get; set; }
    public Company? Company { get; set; }
}