using System.ComponentModel.DataAnnotations;

namespace StockWallet.Domain.Models.Dtos;

public class CompanyDto
{
    public int CompanyId { get; set; }
    [Required] public string Identification { get; set; } = string.Empty;
    [Required] public string Name { get; set; } = string.Empty;
}