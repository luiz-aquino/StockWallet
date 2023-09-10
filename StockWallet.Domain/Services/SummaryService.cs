using StockWallet.Domain.Infraestructure;
using StockWallet.Domain.Models.Dtos;
using StockWallet.Domain.Models.Serivces;
using StockWallet.Domain.Services.Interfaces;

namespace StockWallet.Domain.Services;

public class SummaryService: ISummaryService
{
    private readonly ISummaryRepository _summaryRepository;
    
    public SummaryService(ISummaryRepository summaryRepository)
    {
        _summaryRepository = summaryRepository;
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
}