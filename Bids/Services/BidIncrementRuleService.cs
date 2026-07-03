using Nop.Data;
using Nop.Plugin.Misc.EliteAuctions.Bids.Domain;

namespace Nop.Plugin.Misc.EliteAuctions.Bids.Services
{
    /// <summary>
    /// Represents a bid increment rule service
    /// </summary>
    public class BidIncrementRuleService : IBidIncrementRuleService
    {
        #region Fields

        private readonly IRepository<BidIncrementRule> _bidIncrementRuleRepository;

        #endregion

        #region Constructor

        public BidIncrementRuleService(IRepository<BidIncrementRule> bidIncrementRuleRepository)
        {
            _bidIncrementRuleRepository = bidIncrementRuleRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the bid increment rule for a given bid amount
        /// </summary>
        /// <param name="bidAmount">Bid Amount</param>
        /// <returns>Bid Increment Rule</returns>
        public Task<BidIncrementRule> GetBidIncrementRule(decimal bidAmount)
        {
            return _bidIncrementRuleRepository.Table
                .Where(bidIncrementRule => bidIncrementRule.LowerBandBidAmount <= bidAmount && bidIncrementRule.UpperBandBidAmount >= bidAmount)
                .OrderBy(bidIncrementRule => bidIncrementRule.Priority)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets the global bid increment rules
        /// </summary>
        /// <returns>List of Bid Increment Rules</returns>
        public Task<List<BidIncrementRule>> GetGlobalBidIncremetRule()
        {
            return _bidIncrementRuleRepository.Table
                .Where(rule => rule.AuctionId == null)
                .OrderBy(rule => rule.Priority).ToListAsync();
        }

        /// <summary>
        /// Gets the override bid increment rules for a given auction
        /// </summary>
        /// <param name="auctionId">Auction Id</param>
        /// <returns>List of Bid Increment Rules</returns>
        public Task<List<BidIncrementRule>> GetOverrideBidIncrementRule(int auctionId)
        {
            return _bidIncrementRuleRepository.Table
                .Where(rule => rule.AuctionId == auctionId)
                .OrderBy(rule => rule.Priority)
                .ToListAsync();
        }

        #endregion
    }
}
