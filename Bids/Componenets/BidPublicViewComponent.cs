using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nop.Plugin.Misc.EliteAuctions.Auctions.Services;
using Nop.Services.Catalog;
using Nop.Services.Helpers;
using Nop.Web.Framework.Components;
using Nop.Web.Framework.Infrastructure;
using Nop.Web.Models.Catalog;

namespace Nop.Plugin.Misc.EliteAuctions.Bids.Componenets
{
    public class BidPublicViewComponent : NopViewComponent
    {
        private readonly ILogger<BidPublicViewComponent> _logger;
        private readonly IProductService _productService;
        private readonly IAuctionService _auctionService;
        private readonly IDateTimeHelper _dateTimeHelper;

        public BidPublicViewComponent(ILogger<BidPublicViewComponent> logger,
            IProductService productService,
            IAuctionService auctionService,
            IDateTimeHelper dateTimeHelper)
        {
            _logger = logger;
            _productService = productService;
            _auctionService = auctionService;
            _dateTimeHelper = dateTimeHelper;
        }

        public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
        {
            if (!widgetZone.Equals(PublicWidgetZones.ProductDetailsAddInfo))
                return Content(string.Empty);

            var addToCartModel = additionalData as ProductDetailsModel.AddToCartModel;

            var product = await _productService.GetProductByIdAsync(addToCartModel.ProductId);

            var auction = await _auctionService.GetAuctionByProductId(product.Id);

            if (auction == null)
                return Content(string.Empty);

            if (auction.EndDateTimeUtc <= DateTime.Now)
                return Content(string.Empty);

            addToCartModel.CustomerEnteredPrice = auction.CurrentBidPrice;

            return View("~/Plugins/Misc.EliteAuctions/Views/Public/_Bid.cshtml", addToCartModel);
        }
    }
}
