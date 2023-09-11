namespace StockWallet.Portal.Models.Dtos;

public class InsertResult
{
    public bool Success { get; set; }
    public string Error { get; set; } = string.Empty;
    public int Id { get; set; }
}