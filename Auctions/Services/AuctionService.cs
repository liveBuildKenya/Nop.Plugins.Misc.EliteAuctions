using Nop.Data;
using Nop.Plugin.Misc.EliteAuctions.Auctions.Domain;

namespace Nop.Plugin.Misc.EliteAuctions.Auctions.Services;

/// <summary>
/// Represents the auction service
/// </summary>
public class AuctionService : IAuctionService
{
    #region Fields

    private readonly IRepository<Auction> _auctionRepository;

    #endregion

    #region Constructor

    public AuctionService(IRepository<Auction> auctionRepository)
    {
        _auctionRepository = auctionRepository;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Gets an auction by product id
    /// </summary>
    /// <param name="productId">Product id</param>
    /// <returns>Auction</returns>
    public async Task<Auction> GetAuctionByProductId(int productId)
    {
        var auction = await _auctionRepository.Table
            .Where(auction => auction.ProductId == productId)
            .FirstOrDefaultAsync();

        return auction;
    }

    /// <summary>
    /// Inserts an auction
    /// </summary>
    /// <param name="auction">auction</param>
    public async Task InsertAuction(Auction auction)
    {
        ArgumentNullException.ThrowIfNull(auction);

        await _auctionRepository.InsertAsync(auction);
    }

    #endregion
}
