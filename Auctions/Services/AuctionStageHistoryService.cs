using Nop.Data;
using Nop.Plugin.Misc.EliteAuctions.Auctions.Domain;

namespace Nop.Plugin.Misc.EliteAuctions.Auctions.Services;

/// <summary>
/// Represents the service for managing auction stage history records.
/// </summary>
public class AuctionStageHistoryService : IAuctionStageHistoryService
{
    #region Fields

    private readonly IRepository<AuctionStageHistory> _auctionStageHistoryRepository;

    #endregion

    #region Constructor

    public AuctionStageHistoryService(IRepository<AuctionStageHistory> auctionStageHistoryRepository)
    {
        _auctionStageHistoryRepository = auctionStageHistoryRepository;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Gets the current stage auction by auction id
    /// </summary>
    /// <param name="auctionId">Auction identifier</param>
    /// <returns>Auction Stage History</returns>
    public async Task<AuctionStageHistory> GetCurrentAuctionStageByAuctionId(int auctionId)
    {
        return await _auctionStageHistoryRepository.Table
            .Where(ash => ash.AuctionId == auctionId)
            .OrderByDescending(ash => ash.CreatedOnUtc)
            .FirstOrDefaultAsync();
    }

    /// <summary>
    /// Inserts an auction stage
    /// </summary>
    /// <param name="auctionStageHistory">Auction stage</param>
    public async Task InsertAuctionStageHistory(AuctionStageHistory auctionStageHistory)
    {
        ArgumentNullException.ThrowIfNull(auctionStageHistory);

        await _auctionStageHistoryRepository.InsertAsync(auctionStageHistory);
    }

    #endregion
}
