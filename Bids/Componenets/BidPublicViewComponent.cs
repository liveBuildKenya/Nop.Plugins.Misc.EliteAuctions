using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nop.Web.Framework.Components;
using Nop.Web.Framework.Infrastructure;
using Nop.Web.Models.Catalog;

namespace Nop.Plugin.Misc.EliteAuctions.Bids.Componenets
{
    public class BidPublicViewComponent : NopViewComponent
    {
        private readonly ILogger<BidPublicViewComponent> logger;

        public BidPublicViewComponent(ILogger<BidPublicViewComponent> logger)
        {
            this.logger = logger;
        }

        public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
        {
            if (!widgetZone.Equals(PublicWidgetZones.ProductDetailsAddInfo))
                return Content(string.Empty);

            var addToCartModel = additionalData as ProductDetailsModel.AddToCartModel;

            return View("~/Plugins/Misc.EliteAuctions/Views/Public/_Bid.cshtml", addToCartModel);
        }
    }
}
