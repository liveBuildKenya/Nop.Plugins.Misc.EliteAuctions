using Nop.Core;

namespace Nop.Plugin.Misc.EliteAuctions.Bids.Domain;

/// <summary>
/// Represents a bid entity
/// </summary>
public class Bid : BaseEntity
{
    public Bid()
    {
        this.CreatedOnUtc = DateTime.UtcNow;
    }

    /// <summary>
    /// Gets or sets the auction identifier
    /// </summary>
    public int AuctionId { get; set; }

    /// <summary>
    /// Gets or sets the customer identifier
    /// </summary>
    public int CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the bid amount
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Gets or sets the date and time of entity creation
    /// </summary>
    public DateTime CreatedOnUtc { get; set; }
}
