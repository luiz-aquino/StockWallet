namespace StockWallet.Portal.Models.Dtos;

public class StockEventDto
{
    public int EventId { get; set; }
    public int WalletId { get; set; }
    public int CompanyId { get; set; }
    public EventType EventType { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    public decimal Total => Math.Round(Quantity * Price, 2, MidpointRounding.ToEven);
}