using Nop.Data;
using Nop.Plugin.Misc.EliteAuctions.Auctions.Domain;

namespace Nop.Plugin.Misc.EliteAuctions.Auctions.Services;

/// <summary>
/// Represents the service for managing auction stage history records.
/// </summary>
public class AuctionStageHistoryService : IAuctionStageHistoryService
{
    private readonly IRepository<AuctionStageHistory> _auctionStageHistoryRepository;

    public AuctionStageHistoryService(IRepository<AuctionStageHistory> auctionStageHistoryRepository)
    {
        _auctionStageHistoryRepository = auctionStageHistoryRepository;
    }

    public async Task<AuctionStageHistory> GetCurrentAuctionStageByAuctionId(int auctionId)
    {
        return await _auctionStageHistoryRepository.Table
            .Where(ash => ash.AuctionId == auctionId)
            .OrderByDescending(ash => ash.CreatedOnUtc)
            .FirstOrDefaultAsync();
    }

    public async Task InsertAuctionStageHistory(AuctionStageHistory auctionStageHistory)
    {
        ArgumentNullException.ThrowIfNull(auctionStageHistory);

        await _auctionStageHistoryRepository.InsertAsync(auctionStageHistory);
    }
}
