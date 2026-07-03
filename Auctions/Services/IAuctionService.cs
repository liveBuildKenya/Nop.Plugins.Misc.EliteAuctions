using Nop.Plugin.Misc.EliteAuctions.Auctions.Domain;

namespace Nop.Plugin.Misc.EliteAuctions.Auctions.Services;

/// <summary>
/// Represents an auction service
/// </summary>
public interface IAuctionService
{
    /// <summary>
    /// Gets an auction by product id
    /// </summary>
    /// <param name="productId">Product Identifier</param>
    /// <returns>Auction</returns>
    Task<Auction> GetAuctionByProductId(int productId);

    /// <summary>
    /// Inserts an auction
    /// </summary>
    /// <param name="auction">Auction</param>
    Task InsertAuction(Auction auction);

    /// <summary>
    /// Updates an auction
    /// </summary>
    /// <param name="auction">Auction</param>
    /// <returns></returns>
    Task UpdateAuction(Auction auction);

    /// <summary>
    /// Gets an auction by identifier
    /// </summary>
    /// <param name="auctionId">AuctionIdentifier</param>
    /// <returns>Auction</returns>
    Task<Auction> GetAuctionById(int auctionId);

    /// <summary>
    /// Gets auctions on bidding
    /// </summary>
    /// <returns>Auction list</returns>
    Task<List<Auction>> GetAuctionsOnBidding();
}
