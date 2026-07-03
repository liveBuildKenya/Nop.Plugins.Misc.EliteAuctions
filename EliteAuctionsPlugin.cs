using Nop.Plugin.Misc.EliteAuctions.Auctions.Componenets;
using Nop.Plugin.Misc.EliteAuctions.Bids.Componenets;
using Nop.Plugin.Misc.EliteAuctions.Installation;
using Nop.Services.Cms;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Misc.EliteAuctions;

public class EliteAuctionsPlugin : BasePlugin, IWidgetPlugin
{
    private readonly IEliteAuctionsInstallationService _eliteAuctionsInstallationService;

    public EliteAuctionsPlugin(IEliteAuctionsInstallationService eliteAuctionsInstallationService)
    {
        _eliteAuctionsInstallationService = eliteAuctionsInstallationService;
    }
    public override async Task InstallAsync()
    {
        await _eliteAuctionsInstallationService.InstallSystemAuctionStages();

        await _eliteAuctionsInstallationService.InstallLocaleResources();

        await _eliteAuctionsInstallationService.InstallScheduledTasks();

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
            PublicWidgetZones.ProductPriceTop,
            PublicWidgetZones.ProductDetailsAddInfo
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

        if (widgetZone.Equals(PublicWidgetZones.ProductDetailsAddInfo))
            return typeof(BidPublicViewComponent);

        return null;
    }
}
