using Nop.Plugin.Misc.EliteAuctions.Auctions.Domain;

namespace Nop.Plugin.Misc.EliteAuctions.Auctions.Services;

/// <summary>
/// Represents the auction stage interface
/// </summary>
public interface IAuctionStageService
{
    /// <summary>
    /// Gets the initial auction stage
    /// </summary>
    /// <returns>Auction stage</returns>
    Task<AuctionStage> GetInitialAuctionStage();

    /// <summary>
    /// Represents the auction stage
    /// </summary>
    /// <param name="auctionStage">Auction Stage</param>
    Task InsertAuctionStage(AuctionStage auctionStage);

    /// <summary>
    /// Represents the auction stage
    /// </summary>
    /// <param name="systemName">System Name</param>
    Task<AuctionStage> GetAuctionStageBySystemName(string systemName);

    /// <summary>
    /// Gets an auction stage by identifier
    /// </summary>
    /// <param name="auctionStageId">Auction stage identifer</param>
    /// <returns>Auction stage</returns>
    Task<AuctionStage> GetAuctionStageById(int auctionStageId);
}
