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
}
