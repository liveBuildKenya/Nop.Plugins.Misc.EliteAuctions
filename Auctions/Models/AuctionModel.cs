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

    /// <summary>
    /// Gets or sets the starting bid price
    /// </summary>
    [NopResourceDisplayName("Plugins.Misc.EliteAuctions.StartingBidPrice")]
    public decimal StartingBidPrice { get; set; }

    /// <summary>
    /// Gets or sets the reserve bid price
    /// </summary>
    [NopResourceDisplayName("Plugins.Misc.EliteAuctions.ReserveBidPrice")]
    public decimal ReserveBidPrice { get; set; }

    /// <summary>
    /// Gets or sets the start date and time in UTC
    /// </summary>
    [NopResourceDisplayName("Plugins.Misc.EliteAuctions.StartDateTimeUtc")]
    public DateTime? StartDateTimeUtc { get; set; }

    /// <summary>
    /// Gets or sets the end date and time in UTC
    /// </summary>
    [NopResourceDisplayName("Plugins.Misc.EliteAuctions.EndDateTimeUtc")]
    public DateTime? EndDateTimeUtc { get; set; }

    /// <summary>
    /// Gets or sets the seconds to extend the EndDateTimeUtc when a bid is placed withing those seconds. Preventing bid snipping.
    /// </summary>
    [NopResourceDisplayName("Plugins.Misc.EliteAuctions.ExtensionTriggerSeconds")]
    public int ExtensionTriggerSeconds { get; set; }

    /// <summary>
    /// Gets or sets the primary store currency code
    /// </summary>
    public string PrimaryStoreCurrencyCode { get; set; }
}
