using StockWallet.Domain.Models.Contols;

namespace StockWallet.Domain.Models.Dtos;

public class StockEventDto
{
    public int EventId { get; set; }
    public int WalletId { get; set; }
    public int CompanyId { get; set; }
    public EventType EventType { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}