using Nop.Core;

namespace Nop.Plugin.Misc.EliteAuctions.Auctions.Domain;

/// <summary>
/// Represents the auction stage
/// </summary>
public class AuctionStage : BaseEntity
{
    /// <summary>
    /// Gets or sets the name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the display order
    /// </summary>
    public int DisplayOrder { get; set; }
}
