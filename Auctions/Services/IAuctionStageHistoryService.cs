using Nop.Plugin.Misc.EliteAuctions.Auctions.Domain;

namespace Nop.Plugin.Misc.EliteAuctions.Auctions.Services;

/// <summary>
/// Represents an auction stage history service
/// </summary>
public interface IAuctionStageHistoryService
{
    Task<AuctionStageHistory> GetCurrentAuctionStageByAuctionId(int auctionId);
    Task InsertAuctionStageHistory(AuctionStageHistory auctionStageHistory);
}
