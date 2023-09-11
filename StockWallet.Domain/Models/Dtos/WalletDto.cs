using System.ComponentModel.DataAnnotations;

namespace StockWallet.Domain.Models.Dtos;

public class WalletDto
{
    public int WalletId { get; set; }
    [Required] public string Name { get; set; } = string.Empty;
}