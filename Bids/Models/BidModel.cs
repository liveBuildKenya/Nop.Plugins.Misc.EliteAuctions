using Nop.Web.Framework.Models;

namespace Nop.Plugin.Misc.EliteAuctions.Bids.Models
{
    public record BidModel : BaseNopEntityModel
    {
        public int ProductId { get; set; }

        public decimal CustomerEnteredPrice { get; set; }
    }
}
