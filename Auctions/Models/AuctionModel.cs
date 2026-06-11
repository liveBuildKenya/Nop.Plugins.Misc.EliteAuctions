using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Misc.EliteAuctions.Auctions.Models;

/// <summary>
/// Represents the auction model
/// </summary>
public record AuctionModel : BaseNopEntityModel
{
    /// <summary>
    /// Gets or sets the product identifier
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether an auction should be enabled
    /// </summary>
    [NopResourceDisplayName("Plugins.Misc.EliteAuctions.ActivateAuction")]
    public bool ActivateAuction { get; set; }
}
