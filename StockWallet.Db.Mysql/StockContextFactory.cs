using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace StockWallet.Db.Mysql;

public class StockContextFactory: IDesignTimeDbContextFactory<StockWalletContext>
{
    public StockWalletContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<StockWalletContext>();

        string connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__Default") ?? "Server=localhost; User ID=root; Password=pass; Database=blog";
        connectionString = connectionString.Replace("\"", string.Empty);
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        
        return new StockWalletContext(optionsBuilder.Options);
    }
}