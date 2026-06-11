using Nop.Data;
using Nop.Plugin.Misc.EliteAuctions.Bids.Domain;

namespace Nop.Plugin.Misc.EliteAuctions.Bids.Services;

public class BidService : IBidService
{
    #region Fields

    private readonly IRepository<Bid> _bidRepository;

    #endregion

    #region Constructor

    public BidService(IRepository<Bid> bidRepository)
    {
        _bidRepository = bidRepository;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Gets the highest bid for a given auction. If there are multiple bids with the same amount, the earliest one is returned.
    /// </summary>
    /// <param name="auctionId">Auction Identifier</param>
    /// <returns>Highest Bid</returns>
    public Task<Bid> GetHighestBid(int auctionId)
    {
        return _bidRepository.Table
            .Where(b => b.AuctionId == auctionId)
            .OrderByDescending(b => b.Amount)
            .ThenBy(b => b.CreatedOnUtc)
            .FirstOrDefaultAsync();
    }

    /// <summary>
    /// Inserts a new bid for a given auction and customer. The CreatedOnUtc property is set to the current UTC time.
    /// </summary>
    /// <returns>Inserted Bid</returns>
    public async Task<Bid> InsertBid(Bid newBid)
    {
        if (newBid == null)
            throw new ArgumentNullException(nameof(Bid));

        await _bidRepository.InsertAsync(newBid);

        return newBid;
    }

    #endregion
}
