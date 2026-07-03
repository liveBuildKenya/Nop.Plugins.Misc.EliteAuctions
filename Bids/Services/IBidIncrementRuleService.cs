using Nop.Plugin.Misc.EliteAuctions.Bids.Domain;

namespace Nop.Plugin.Misc.EliteAuctions.Bids.Services
{
    /// <summary>
    /// Represents a bid increment rule service
    /// </summary>
    public interface IBidIncrementRuleService
    {
        /// <summary>
        /// Gets the bid increment rule for a given bid amount
        /// </summary>
        /// <param name="bidAmount">The bid amount</param>
        /// <returns>The bid increment rule</returns>
        Task<BidIncrementRule> GetBidIncrementRule(decimal bidAmount);

        /// <summary>
        /// Gets the global bid increment rules
        /// </summary>
        /// <returns>List of bid increment rules</returns>
        Task<List<BidIncrementRule>> GetGlobalBidIncremetRule();

        /// <summary>
        /// Gets the override bid increment rules for a given auction
        /// </summary>
        /// <param name="auctionId">Auction Identifier</param>
        /// <returns>List of bid increment rules</returns>
        Task<List<BidIncrementRule>> GetOverrideBidIncrementRule(int auctionId);
    }
}
