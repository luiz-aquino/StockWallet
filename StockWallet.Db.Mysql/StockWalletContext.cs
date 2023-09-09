using Microsoft.EntityFrameworkCore;
using StockWallet.Domain.Models;
using StockWallet.Domain.Models.Serivces;

namespace StockWallet.Db.Mysql;

public class StockWalletContext: DbContext
{
    // private readonly string _connectionString;
    public StockWalletContext(DbContextOptions<StockWalletContext> options) : base(options)
    {
        
    }

    public DbSet<Wallet>? Wallets { get; set; }
    public DbSet<Company>? Companies { get; set; }
    public DbSet<StockEvent>? StockEvents { get; set; }
    public DbSet<StockSummary>? Summaries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Wallet>().HasKey(x => x.WalletId);

        modelBuilder.Entity<Wallet>().HasMany(x => x.StockEvents)
            .WithOne(x => x.Wallet)
            .HasForeignKey(x => x.WalletId);

        modelBuilder.Entity<Wallet>().HasMany(x => x.Summaries)
            .WithOne(x => x.Wallet)
            .HasForeignKey(x => x.WalletId);

        modelBuilder.Entity<Company>().HasKey(x => x.CompanyId);
        modelBuilder.Entity<Company>().HasMany(x => x.StockEvents)
            .WithOne(x => x.Company)
            .HasForeignKey(x => x.CompanyId);

        modelBuilder.Entity<Company>().HasMany(x => x.Summaries)
            .WithOne(x => x.Company)
            .HasForeignKey(x => x.CompanyId);

        modelBuilder.Entity<StockEvent>().HasKey(x => x.EventId);
        modelBuilder.Entity<StockSummary>().HasKey(x => x.SummaryId);
    }
}