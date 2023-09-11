using System.ComponentModel.DataAnnotations;

namespace StockWallet.Portal.Models.Dtos;

public class WalletDto
{
    public int WalletId { get; set; }
    
    [Required]
    public string Name { get; set; } = string.Empty;
}