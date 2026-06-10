using Nop.Plugin.Misc.EliteAuctions.Auctions.Domain;
using Nop.Plugin.Misc.EliteAuctions.Auctions.Services;
using Nop.Plugin.Misc.EliteAuctions.Components;
using Nop.Services.Cms;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Misc.EliteAuctions;

public class EliteAuctionsPlugin : BasePlugin, IWidgetPlugin
{
    private readonly ILocalizationService _localizationService;
    private readonly IAuctionStageService _auctionStageService;

    public EliteAuctionsPlugin(ILocalizationService localizationService,
        IAuctionStageService auctionStageService)
    {
        _localizationService = localizationService;
        _auctionStageService = auctionStageService;
    }
    public override async Task InstallAsync()
    {
        await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
        {
            ["Plugins.Misc.EliteAuctions.EnableProxyBidding"] = "Enable Proxy Bidding",
            ["Plugins.Misc.EliteAuctions.Settings"] = "Auction Settings",

        });
        var preparationAuctionStage = new AuctionStage { DisplayOrder = 1, Name = "Preperation" };
        await _auctionStageService.InsertAuctionStage(preparationAuctionStage);

        var openingAuctionStage = new AuctionStage { DisplayOrder = 2, Name = "Opening" };
        await _auctionStageService.InsertAuctionStage(openingAuctionStage);
        
        var biddingAuctionStage = new AuctionStage { DisplayOrder = 3, Name = "Bidding" };
        await _auctionStageService.InsertAuctionStage(biddingAuctionStage);

        var closingAuctionStage = new AuctionStage { DisplayOrder = 4, Name = "Closing" };
        await _auctionStageService.InsertAuctionStage(closingAuctionStage);

        var settlemtnAuctionStage = new AuctionStage { DisplayOrder = 5, Name = "Settlement" };
        await _auctionStageService.InsertAuctionStage(settlemtnAuctionStage);


        await base.InstallAsync();
    }
    public bool HideInWidgetList => false;

    /// <summary>
    /// Gets widget zones where this widget should be rendered
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the widget zones
    /// </returns>
    public Task<IList<string>> GetWidgetZonesAsync()
    {
        return Task.FromResult<IList<string>>(new List<string>
        {
            AdminWidgetZones.ProductDetailsBlock,
            PublicWidgetZones.ProductBoxAddinfoBefore,
            PublicWidgetZones.ProductPriceTop
        });
    }

    public Type GetWidgetViewComponent(string widgetZone)
    {
        if (widgetZone.Equals(AdminWidgetZones.ProductDetailsBlock))
            return typeof(AuctionAdminViewComponent);

        if (widgetZone.Equals(PublicWidgetZones.ProductBoxAddinfoBefore))
            return typeof(CountdownPublicViewComponent);

        if (widgetZone.Equals(PublicWidgetZones.ProductPriceTop))
            return typeof(CountdownPublicViewComponent);

        return null;
    }
}
