using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Nop.Web.Framework.Mvc.Routing;
using Nop.Web.Infrastructure;

namespace Nop.Plugin.Misc.EliteAuctions.Infrastructure;

public class RouteProvider : BaseRouteProvider, IRouteProvider
{
    public int Priority => 0;

    public void RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapControllerRoute(name: "Plugin.Misc.EliteAuctions.Save",
            pattern: "Admin/Auction/Save",
            defaults: new { controller = "Auction", action = "Save" });
    }
}
