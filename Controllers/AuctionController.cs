using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Misc.EliteAuctions.Auctions.Domain;
using Nop.Plugin.Misc.EliteAuctions.Auctions.Models;
using Nop.Plugin.Misc.EliteAuctions.Auctions.Services;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Misc.EliteAuctions.Controllers;

[Area(AreaNames.ADMIN)]
[AuthorizeAdmin]
public class AuctionController : BasePluginController
{
    private readonly IAuctionService _auctionService;
    private readonly IAuctionStageService _auctionStageService;
    private readonly IAuctionStageHistoryService _auctionStageHistoryService;

    public AuctionController(IAuctionService auctionService,
        IAuctionStageService auctionStageService,
        IAuctionStageHistoryService auctionStageHistoryService)
    {
        _auctionService = auctionService;
        _auctionStageService = auctionStageService;
        _auctionStageHistoryService = auctionStageHistoryService;
    }

    [HttpPost]
    public async Task<IActionResult> Save(AuctionModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!model.EnableAuction)
        {
            ModelState.AddModelError("Auction-Disabled", "Auction not enabled for this product");
            return BadRequest(ModelState);
        }

        var auction = await _auctionService.GetAuctionByProductId(model.ProductId);

        if (auction == null)
        {
            auction = new Auction
            {
                ProductId = model.ProductId,
                IsProxyBiddingEnabled = model.EnableProxyBidding,
                WinningCustomerId = null
            };

            await _auctionService.InsertAuction(auction);
        }

        var auctionStageHistory = await _auctionStageHistoryService.GetCurrentAuctionStageByAuctionId(auction.Id);
        if (auctionStageHistory == null)
        {
            var auctionStage = _auctionStageService.GetBeginingAuctionStage();

            await _auctionStageHistoryService.InsertAuctionStageHistory(new AuctionStageHistory
            {
                AuctionId = auction.Id,
                AuctionStageId = auctionStage.Id
            });
        }

        return Ok();
    }
}
