namespace StockWallet.Domain.Models.Dtos;

public class SummaryDto
{
    public int SummaryId { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Quantity { get; set; }
    public decimal AveragePrice { get; set; }
    public int WalletId { get; set; }
    public int CompanyId { get; set; }
}