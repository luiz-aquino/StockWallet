using System.ComponentModel.DataAnnotations;

namespace StockWallet.Portal.Models.Dtos;

public class StockEventDto
{
    public int EventId { get; set; }
    
    [Required, Range(1, int.MaxValue)] 
    public int WalletId { get; set; }
    
    [Required, Range(1, int.MaxValue)] 
    public int CompanyId { get; set; }
    
    [Required, Range(0, 1)] 
    public EventType EventType { get; set; }
    
    [Required, Range(1, int.MaxValue)]
    public int Quantity { get; set; }
    
    [Required, Range(0.01, double.MaxValue)] 
    public decimal Price { get; set; }

    public decimal Total => Math.Round(Quantity * Price, 2, MidpointRounding.ToEven);
}