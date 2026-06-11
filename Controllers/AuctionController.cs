using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Misc.EliteAuctions.Auctions.Models;
using Nop.Plugin.Misc.EliteAuctions.MarketPlace;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Misc.EliteAuctions.Controllers;

[Area(AreaNames.ADMIN)]
[AuthorizeAdmin]
public class AuctionController : BasePluginController
{
    private readonly IMarketPlaceFactory _marketPlaceFactory;

    public AuctionController(IMarketPlaceFactory marketPlaceFactory)
    {
        _marketPlaceFactory = marketPlaceFactory;
    }

    [HttpPost]
    public async Task<IActionResult> ConfigureOptions(AuctionModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!model.ActivateAuction)
        {
            ModelState.AddModelError("Auction-Disabled", "Auction not enabled for this product");
            return BadRequest(ModelState);
        }

        await _marketPlaceFactory.PrepareProductAuction(model);

        return Ok();
    }
}
