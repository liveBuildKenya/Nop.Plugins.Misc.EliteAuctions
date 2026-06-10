using Nop.Core;

namespace Nop.Plugin.Misc.EliteAuctions.Auctions.Domain;

/// <summary>
/// Represents an auction status log entry
/// </summary>
public class AuctionStageHistory : BaseEntity
{
    public AuctionStageHistory()
    {
        this.CreatedOnUtc = DateTime.UtcNow;
    }

    /// <summary>
    /// Gets or sets the auction identifier
    /// </summary>
    public int AuctionId { get; set; }

    /// <summary>
    /// Gets or sets the auction stage identifer
    /// </summary>
    public int AuctionStageId { get; set; }

    /// <summary>
    /// Gets or sets the date and time of entity creation
    /// </summary>
    public DateTime CreatedOnUtc { get; set; }
}
