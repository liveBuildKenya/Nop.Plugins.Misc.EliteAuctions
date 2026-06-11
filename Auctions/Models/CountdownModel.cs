using Nop.Web.Framework.Models;

namespace Nop.Plugin.Misc.EliteAuctions.Auctions.Models;

/// <summary>
/// Represents the countdown model
/// </summary>
public record CountdownModel : BaseNopEntityModel
{
    /// <summary>
    /// Gets or sets a value indicating if a product is on auction
    /// </summary>
    public bool IsOnAuction { get; set; }

    /// <summary>
    /// Gets or sets the end date and time
    /// </summary>
    public DateTime? EndDateUtc { get; set; }
}
