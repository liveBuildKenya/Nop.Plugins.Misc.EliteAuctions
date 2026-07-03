using Nop.Plugin.Misc.EliteAuctions.Auctions.Domain;

namespace Nop.Plugin.Misc.EliteAuctions.Auctions.Services;

/// <summary>
/// Represents an auction stage history service
/// </summary>
public interface IAuctionStageHistoryService
{
    /// <summary>
    /// Gets the current auction stage by auction identifier
    /// </summary>
    /// <param name="auctionId">Auction identifier</param>
    /// <returns>Auction stage history</returns>
    Task<AuctionStageHistory> GetCurrentAuctionStageByAuctionId(int auctionId);

    /// <summary>
    /// Inserts auction stage history
    /// </summary>
    /// <param name="auctionStageHistory">Auction stage history</param>
    /// <returns></returns>
    Task InsertAuctionStageHistory(AuctionStageHistory auctionStageHistory);

}
