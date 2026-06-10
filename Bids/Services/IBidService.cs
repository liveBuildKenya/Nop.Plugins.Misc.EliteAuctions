using Nop.Plugin.Misc.EliteAuctions.Bids.Domain;

namespace Nop.Plugin.Misc.EliteAuctions.Bids.Services;

public interface IBidService
{
    /// <summary>
    /// Places a bid for a given auction and customer.
    /// </summary>
    /// <returns>Bid</returns>
    Task<Bid> InsertBid(Bid newBid);

    /// <summary>
    /// Retrieves the highest bid for a given auction.
    /// </summary>
    /// <param name="auctionId">Auction Identifier</param>
    /// <returns>Highest Bid</returns>
    Task<Bid> GetHighestBId(int auctionId);
}
