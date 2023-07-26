using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Nop.Plugin.Misc.AliExpress.Dropshipping;
using Nop.Web.Framework.Mvc.Routing;

namespace Nop.Plugin.Misc.Sendinblue.Infrastructure
{
    /// <summary>
    /// Represents plugin route provider
    /// </summary>
    public class RouteProvider : IRouteProvider
    {
        /// <summary>
        /// Register routes
        /// </summary>
        /// <param name="endpointRouteBuilder">Route builder</param>
        public void RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapControllerRoute(AliExpressDropshipingDefaults.ConfigureRoute, "Admin/AliExpressDropshipping/Configure",
                new { controller = "AliExpressDropshipping", action = "Configure" });

            endpointRouteBuilder.MapControllerRoute(AliExpressDropshipingDefaults.AppSettingsRoute, "Admin/AliExpressDropshipping/AliexpressAuthorizationData",
                new { controller = "AliExpressDropshipping", action = "AliexpressAuthorizationData" });

            endpointRouteBuilder.MapControllerRoute(AliExpressDropshipingDefaults.ListProductRoute, "Admin/AliExpressProduct/List",
                new { controller = "AliExpressProduct", action = "List" });

            endpointRouteBuilder.MapControllerRoute(AliExpressDropshipingDefaults.EditProductRoute, "Admin/AliExpressProduct/Edit",
                new { controller = "AliExpressProduct", action = "Edit" });

            endpointRouteBuilder.MapControllerRoute(AliExpressDropshipingDefaults.CreateProductRoute, "Admin/AliExpressProduct/Create",
                new { controller = "AliExpressProduct", action = "Create" });
        }

        /// <summary>
        /// Gets a priority of route provider
        /// </summary>
        public int Priority => 10;
    }
}