namespace StockWallet.Domain.Models;

public class Company
{
    public int CompanyId { get; set; }
    public string Identification { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    public List<StockEvent>? StockEvents { get; set; }
}