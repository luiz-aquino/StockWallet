using StockWallet.Db.Mysql.Repositories;
using StockWallet.Domain.Infraestructure;
using StockWallet.Domain.Services;
using StockWallet.Domain.Services.Interfaces;

namespace StockWallet.Utils;

public static class ServicesHelper
{
    public static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddTransient<IWalletRepository, WalletRepository>();
        services.AddTransient<ICompanyRepository, CompanyRepository>();
        services.AddTransient<IEventRepository, EventRepository>();
        services.AddTransient<ISummaryRepository, SummaryRepository>();
    }

    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddTransient<IWalletService, WalletService>();
        services.AddTransient<ICompanyService, CompanyService>();
        services.AddTransient<IEventService, EventService>();
        services.AddTransient<ISummaryService, SummaryService>();
    }
    
}