using Microsoft.AspNetCore.Mvc;
using Nop.Core.Domain.Catalog;
using Nop.Plugin.Misc.EliteAuctions.Auctions.Models;
using Nop.Services.Catalog;
using Nop.Web.Framework.Components;
using Nop.Web.Framework.Infrastructure;
using Nop.Web.Models.Catalog;

namespace Nop.Plugin.Misc.EliteAuctions.Components;

public class CountdownPublicViewComponent : NopViewComponent
{
    private readonly IProductService _productService;

    public CountdownPublicViewComponent(IProductService productService)
    {
        _productService = productService;
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

        var productModel = await _productService.GetProductByIdAsync(productId);

        if (!productModel.CustomerEntersPrice)
            return Content(string.Empty);

        var countdownModel = new CountdownModel
        {
            IsOnAuction = true,
            EndDateUtc = productModel.AvailableEndDateTimeUtc
        };

        return View("~/Plugins/Misc.EliteAuctions/Views/Public/_ProductCountdown.cshtml", countdownModel);
    }
}
