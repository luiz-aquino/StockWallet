namespace StockWallet.Domain.Models.Dtos;

public class CompanyDto
{
    public int CompanyId { get; set; }
    public string Identification { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}