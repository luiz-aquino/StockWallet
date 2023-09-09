using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace StockWallet.Db.Mysql;

public class StockContextFactory: IDesignTimeDbContextFactory<StockWalletContext>
{
    public StockWalletContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<StockWalletContext>();

        string connectionString = Environment.GetEnvironmentVariable("ConnectionStrings_Default") ?? "Server=localhost; User ID=root; Password=pass; Database=blog";
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        
        return new StockWalletContext(optionsBuilder.Options);
    }
}