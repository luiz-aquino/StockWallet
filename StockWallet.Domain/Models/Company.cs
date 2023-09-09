using StockWallet.Domain.Models.Serivces;

namespace StockWallet.Domain.Models;

public class Company
{
    public int CompanyId { get; set; }
    public string Identification { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    public ICollection<StockEvent>? StockEvents { get; set; }
    public ICollection<StockSummary>? Summaries { get; set; }
}