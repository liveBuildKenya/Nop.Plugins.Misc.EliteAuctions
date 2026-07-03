using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Misc.EliteAuctions.Auctions.Models;
using Nop.Plugin.Misc.EliteAuctions.Auctions.Services;
using Nop.Services.Catalog;
using Nop.Web.Framework.Components;
using Nop.Web.Framework.Infrastructure;
using Nop.Web.Models.Catalog;

namespace Nop.Plugin.Misc.EliteAuctions.Bids.Componenets;

public class CountdownPublicViewComponent : NopViewComponent
{
    private readonly IProductService _productService;
    private readonly IAuctionService _auctionService;

    public CountdownPublicViewComponent(IProductService productService,
        IAuctionService auctionService)
    {
        _productService = productService;
        _auctionService = auctionService;
    }

    public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
    {
        var isAllowedZone = widgetZone == PublicWidgetZones.ProductBoxAddinfoBefore ||
                            widgetZone == PublicWidgetZones.ProductPriceTop;

        if (!isAllowedZone)
            return Content(string.Empty);

        int productId = 0;

        if (additionalData is ProductOverviewModel overview)
        {
            productId = overview.Id;
        }
        else if (additionalData is ProductDetailsModel details)
        {
            productId = details.Id;
        }

        if (productId == 0)
            return Content(string.Empty);

        var product = await _productService.GetProductByIdAsync(productId);
        var auction = await _auctionService.GetAuctionByProductId(productId);

        if (!product.CustomerEntersPrice || auction == null)
            return Content(string.Empty);

        var countdownModel = new CountdownModel
        {
            IsOnAuction = true,
            EndDateUtc = auction.EndDateTimeUtc
        };

        return View("~/Plugins/Misc.EliteAuctions/Views/Public/_ProductCountdown.cshtml", countdownModel);
    }
}
