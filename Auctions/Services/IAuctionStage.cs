using Nop.Plugin.Misc.EliteAuctions.Auctions.Domain;

namespace Nop.Plugin.Misc.EliteAuctions.Auctions.Services;

/// <summary>
/// Represents the auction stage interface
/// </summary>
public interface IAuctionStageService
{
    Task<AuctionStage> GetBeginingAuctionStage();

    /// <summary>
    /// Represents the auction stage
    /// </summary>
    /// <param name="auctionStage">Auction Stage</param>
    Task InsertAuctionStage(AuctionStage auctionStage);
}
