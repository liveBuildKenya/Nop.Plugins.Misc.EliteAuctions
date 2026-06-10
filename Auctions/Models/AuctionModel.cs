using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Misc.EliteAuctions.Auctions.Models;

public record AuctionModel : BaseNopEntityModel
{
    /// <summary>
    /// Gets or sets the product identifier
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether an auction should be enabled
    /// </summary>
    [NopResourceDisplayName("Plugins.Misc.EliteAuctions.EnableAuction")]
    public bool EnableAuction { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether proxy bidding is enabled
    /// </summary>
    [NopResourceDisplayName("Plugins.Misc.EliteAuctions.EnableProxyBidding")]
    public bool EnableProxyBidding{ get; set; }
}
