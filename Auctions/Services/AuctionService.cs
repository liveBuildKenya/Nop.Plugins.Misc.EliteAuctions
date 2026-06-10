using Nop.Data;
using Nop.Plugin.Misc.EliteAuctions.Auctions.Domain;

namespace Nop.Plugin.Misc.EliteAuctions.Auctions.Services;

public class AuctionService : IAuctionService
{
    private readonly IRepository<Auction> _auctionRepository;

    public AuctionService(IRepository<Auction> auctionRepository)
    {
        _auctionRepository = auctionRepository;
    }

    public async Task<Auction> GetAuctionByProductId(int productId)
    {
        var auction = await _auctionRepository.Table
            .Where(auction => auction.ProductId == productId)
            .FirstOrDefaultAsync();

        return auction;
    }

    public async Task InsertAuction(Auction auction)
    {
        ArgumentNullException.ThrowIfNull(auction);

        await _auctionRepository.InsertAsync(auction);
    }
}
