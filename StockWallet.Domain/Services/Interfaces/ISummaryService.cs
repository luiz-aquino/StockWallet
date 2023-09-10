using StockWallet.Domain.Models.Dtos;

namespace StockWallet.Domain.Services.Interfaces;

public interface ISummaryService
{
    Task<(SummaryDto summary, string error)> Get(int id);
    Task<(List<SummaryDto> summaries, string error)> All(int walletId);
    Task<(bool success, string error)> Process();
}