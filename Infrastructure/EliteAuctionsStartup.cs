using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using Nop.Plugin.Misc.EliteAuctions.Auctions.Services;
using Nop.Plugin.Misc.EliteAuctions.Bids.Services;

namespace Nop.Plugin.Misc.EliteAuctions.Infrastructure;

public class EliteAuctionsStartup : INopStartup
{
    public int Order => 99999;

    public void Configure(IApplicationBuilder application)
    {

    }

    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IAuctionService, AuctionService>();
        services.AddTransient<IAuctionStageService, AuctionStageService>();
        services.AddTransient<IAuctionStageHistoryService, AuctionStageHistoryService>();

        services.AddTransient<IBidService, BidService>();
    }
}
