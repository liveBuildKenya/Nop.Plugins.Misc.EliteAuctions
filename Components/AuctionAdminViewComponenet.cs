using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Misc.EliteAuctions.Auctions.Models;
using Nop.Plugin.Misc.EliteAuctions.Auctions.Services;
using Nop.Web.Areas.Admin.Models.Catalog;
using Nop.Web.Framework.Components;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Misc.EliteAuctions.Components;

/// <summary>
/// Represents the view component for the auction administration interface.
/// </summary>
public class AuctionAdminViewComponent : NopViewComponent
{
    private readonly IAuctionService _auctionService;

    public AuctionAdminViewComponent(IAuctionService auctionService)
    {
        _auctionService = auctionService;
    }

    public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
    {
        if (!widgetZone.Equals(AdminWidgetZones.ProductDetailsBlock))
            return Content(string.Empty);

        var product = additionalData as ProductModel;

        if (product == null)
            return Content(string.Empty);

        var routeData = ViewContext.RouteData;
        var action = routeData.Values["action"]?.ToString();

        if (!string.Equals(action, "Edit", StringComparison.OrdinalIgnoreCase))
            return Content(string.Empty);

        var auction = await _auctionService.GetAuctionByProductId(product.Id);

        var auctionModel = new AuctionModel
        {
            ProductId = product.Id,
            EnableProxyBidding = auction?.IsProxyBiddingEnabled ?? false,
            EnableAuction = auction != null,
        };


        return View("~/Plugins/Misc.EliteAuctions/Views/Admin/_AuctionFields.cshtml", auctionModel);
    }
}
