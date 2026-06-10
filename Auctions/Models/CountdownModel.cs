using Nop.Web.Framework.Models;

namespace Nop.Plugin.Misc.EliteAuctions.Auctions.Models;

public record CountdownModel : BaseNopEntityModel
{
    public bool IsOnAuction { get; set; }
    public DateTime? EndDateUtc { get; set; }
}
