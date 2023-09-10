namespace StockWallet.Models;

public class InsertResult
{
    public bool Success { get; set; }
    public string Error { get; set; } = string.Empty;
    public int Id { get; set; }
}