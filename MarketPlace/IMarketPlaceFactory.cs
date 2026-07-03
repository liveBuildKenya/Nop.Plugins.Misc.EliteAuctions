using Nop.Plugin.Misc.EliteAuctions.Auctions.Models;
using Nop.Plugin.Misc.EliteAuctions.Bids.Models;

namespace Nop.Plugin.Misc.EliteAuctions.MarketPlace
{
    public interface IMarketPlaceFactory
    {
        /// <summary>
        /// Prepares a product for auctioning
        /// </summary>
        /// <param name="auctionModel"></param>
        /// <returns></returns>
        Task PrepareProductAuction(AuctionModel auctionModel);

        /// <summary>
        /// Places a bid
        /// </summary>
        /// <param name="bidModel">Bid model</param>
        /// <returns></returns>
        Task PlaceBid(BidModel bidModel);

        /// <summary>
        /// Moves an auction to the next stage
        /// </summary>
        /// <param name="auctionId">AuctionId</param>
        /// <returns></returns>
        Task MoveAuctionToNextStage(int auctionId);
    }
}
