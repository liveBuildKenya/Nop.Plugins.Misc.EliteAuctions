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
            this.CreatedOnUtc = DateTime.UtcNow;
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
        /// Gets or sets the date and time of entity creation
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }
    }
}
