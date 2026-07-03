using Nop.Core;

namespace Nop.Plugin.Misc.EliteAuctions.Bids.Domain
{
    /// <summary>
    /// Represents a bid increment rule
    /// </summary>
    public class BidIncrementRule : BaseEntity
    {
        /// <summary>
        /// Gets or sets the bid increment type
        /// </summary>
        public BidIncrementType BidIncrementType { get; set; }
        /// <summary>
        /// Gets or sets the lower band bid amount
        /// </summary>
        public decimal LowerBandBidAmount { get; set; }
        /// <summary>
        /// Gets or sets the upper band bid amount
        /// </summary>
        public decimal UpperBandBidAmount { get; set; }
        /// <summary>
        /// Gets or sets the increment amount
        /// </summary>
        public decimal IncrementAmount { get; set; }
        /// <summary>
        /// Gets or sets the priority
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// Gets or sets the auction identifier
        /// </summary>
        public int? AuctionId { get; set; }
    }

    public enum BidIncrementType
    {
        Percentage,
        FixedAmount
    }
}
