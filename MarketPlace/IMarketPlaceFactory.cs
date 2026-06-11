using Nop.Plugin.Misc.EliteAuctions.Auctions.Models;
using Nop.Plugin.Misc.EliteAuctions.Bids.Models;

namespace Nop.Plugin.Misc.EliteAuctions.MarketPlace
{
    public interface IMarketPlaceFactory
    {
        Task PrepareProductAuction(AuctionModel auctionModel);
        Task PlaceBid(BidModel bidModel);
    }
}
