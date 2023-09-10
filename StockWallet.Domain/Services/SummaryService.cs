using StockWallet.Domain.Infraestructure;
using StockWallet.Domain.Models;
using StockWallet.Domain.Models.Dtos;
using StockWallet.Domain.Models.Serivces;
using StockWallet.Domain.Services.Interfaces;

namespace StockWallet.Domain.Services;

public class SummaryService: ISummaryService
{
    private readonly ISummaryRepository _summaryRepository;
    private readonly IWalletRepository _walletRepository;
    private readonly IEventRepository _eventRepository;
    
    public SummaryService(ISummaryRepository summaryRepository, IWalletRepository walletRepository, IEventRepository eventRepository)
    {
        _summaryRepository = summaryRepository;
        _walletRepository = walletRepository;
        _eventRepository = eventRepository;
    }

    public async Task<(SummaryDto summary, string error)> Get(int id)
    {
        (StockSummary summary, string error) = await _summaryRepository.Get(id);

        var dto = new SummaryDto();

        if (!string.IsNullOrEmpty(error)) return (dto, error);

        dto.SummaryId = summary.SummaryId;
        dto.WalletId = summary.WalletId;
        dto.CompanyId = summary.CompanyId;
        dto.AveragePrice = summary.AveragePrice;
        dto.Quantity = summary.Quantity;
        
        return (dto, error);
    }

    public async Task<(List<SummaryDto> summaries, string error)> All(int walletId)
    {
        (List<StockSummary> summaries, string error) = await _summaryRepository.All(walletId);

        var dtos = new List<SummaryDto>(summaries.Count);

        if (!string.IsNullOrEmpty(error)) return (dtos, error);
        
        dtos.AddRange(summaries.Select(x => new SummaryDto
        {
            SummaryId = x.SummaryId,
            WalletId = x.WalletId,
            CompanyId = x.CompanyId,
            Quantity = x.Quantity,
            AveragePrice = x.AveragePrice
        }));
        
        return (dtos, error);
    }

    public async Task<(bool success, string error)> Process()
    {
        string error = string.Empty;

        try
        {
            (List<Wallet> wallets, string _) = await _walletRepository.All();

            foreach (var wallet in wallets)
            {
                (List<int> companies, string _) = await _eventRepository.WalletCompanies(wallet.WalletId);

                (List<StockSummary> summaries, string _) = await _summaryRepository.All(wallet.WalletId);

                (List<StockEvent> stockEvents, string _) = await _eventRepository.WalletEvents(wallet.WalletId, companies);

                var updatedSummaries = new List<StockSummary>(companies.Count);
                var summariesToInsert = new List<StockSummary>(companies.Count);
                foreach (var companyId in companies)
                {
                    StockSummary summary = summaries.FirstOrDefault(x => x.CompanyId == companyId) ?? new StockSummary
                    {
                        WalletId = wallet.WalletId,
                        CompanyId = companyId,
                        CreatedAt = DateTime.Now
                    };

                    List<StockEvent> currentEvents = stockEvents.Where(x => x.CompanyId == companyId).ToList();

                    int quantity = currentEvents.Sum(x => x.Quantity);
                    decimal totalPrice = currentEvents.Sum(x => x.Price * x.Quantity);
                    
                    summary.AveragePrice = (summary.AveragePrice * summary.Quantity) + (totalPrice) / (quantity + summary.Quantity);
                    summary.Quantity += quantity;

                    summary.LastProcessedId = currentEvents.OrderBy(x => x.EventId).Select(x => x.EventId).LastOrDefault();
                    
                    if (summary.SummaryId == 0)
                    {
                        summariesToInsert.Add(summary);
                    }
                    else if(DateTime.Now.Date.Subtract(summary.CreatedAt.Date) >= TimeSpan.FromDays(1))
                    {
                        summariesToInsert.Add(new StockSummary
                        {
                            WalletId = summary.WalletId,
                            CompanyId = summary.CompanyId,
                            CreatedAt = summary.CreatedAt,
                            AveragePrice = summary.AveragePrice,
                            Quantity = summary.Quantity,
                            LastProcessedId = summary.LastProcessedId
                        });
                    }
                    else
                    {
                        updatedSummaries.Add(summary);
                    }

                    if (summariesToInsert.Any()) await _summaryRepository.Insert(summariesToInsert);
                    if (updatedSummaries.Any()) await _summaryRepository.Update(updatedSummaries);
                }
            } 
        }
        catch (Exception e)
        {
            error = e.Message;
        }
        
        return (string.IsNullOrEmpty(error), error);
    }
}