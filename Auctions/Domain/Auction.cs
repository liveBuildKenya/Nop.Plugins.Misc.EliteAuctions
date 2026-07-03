using Nop.Core;

namespace Nop.Plugin.Misc.EliteAuctions.Auctions.Domain
{
    /// <summary>
    /// Represents an auction entity
    /// </summary>
    public class Auction : BaseEntity
    {
        public Auction()
        {
            DateTimeCreatedUtc = DateTime.UtcNow;
            DateTimeUpdatedUtc = DateTime.UtcNow;
        }

        /// <summary>
        /// Gets or sets the product identifier
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the winning customer
        /// </summary>
        public int? WinningCustomerId { get; set; }

        /// <summary>
        /// Gets or sets the store id
        /// </summary>
        public int StoreId { get; set; }

        /// <summary>
        /// Gets or sets the starting bid price
        /// </summary>
        public decimal StartingBidPrice { get; set; }

        /// <summary>
        /// Gets or sets the current bid price
        /// </summary>
        public decimal CurrentBidPrice { get; set; }

        /// <summary>
        /// Gets or sets the reserve bid price
        /// </summary>
        public decimal ReserveBidPrice { get; set; }

        /// <summary>
        /// Gets or sets the start date and time in UTC
        /// </summary>
        public DateTime StartDateTimeUtc { get; set; }

        /// <summary>
        /// Gets or sets the end date and time in UTC
        /// </summary>
        public DateTime EndDateTimeUtc { get; set; }

        /// <summary>
        /// Gets or sets the seconds to extend the EndDateTimeUtc when a bid is placed withing those seconds. Preventing bid snipping.
        /// </summary>
        public int ExtensionTriggerSeconds { get; set; }

        /// <summary>
        /// Gets or sets the date and time of entity creation
        /// </summary>
        public DateTime DateTimeCreatedUtc { get; set; }

        /// <summary>
        /// Gets or sets the date and time of entity update
        /// </summary>
        public DateTime DateTimeUpdatedUtc { get; set; }
    }
}
