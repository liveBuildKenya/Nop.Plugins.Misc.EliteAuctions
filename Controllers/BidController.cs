using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Misc.EliteAuctions.Bids.Models;
using Nop.Plugin.Misc.EliteAuctions.MarketPlace;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Misc.EliteAuctions.Controllers;

[Authorize]
public class BidController : BasePluginController
{
    private readonly IMarketPlaceFactory _marketPlaceFactory;

    public BidController(IMarketPlaceFactory marketPlaceFactory)

    {
        _marketPlaceFactory = marketPlaceFactory;
    }

    [HttpPost]
    public async Task<IActionResult> PlaceBid(BidModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _marketPlaceFactory.PlaceBid(model);

        return Ok();
    }
}
