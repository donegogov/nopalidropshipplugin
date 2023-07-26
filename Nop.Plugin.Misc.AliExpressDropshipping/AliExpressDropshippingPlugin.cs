using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Nop.Core;
using Nop.Core.Configuration;
using Nop.Services.Common;
using Nop.Services.Plugins;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Menu;

namespace Nop.Plugin.Misc.AliExpress.Dropshipping
{
    /// <summary>
    /// Represents the Web API frontend plugin
    /// </summary>
    public class AliExpressDropshippingPlugin : BasePlugin, IAdminMenuPlugin, IMiscPlugin
    {
        #region Fields

        private readonly IPermissionService _permissionService;
        private readonly IWebHelper _webHelper;

        #endregion

        #region Ctor

        public AliExpressDropshippingPlugin(IPermissionService permissionService,
            IWebHelper webHelper)
        {
            _permissionService = permissionService;
            _webHelper = webHelper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a configuration page URL
        /// </summary>
        public override string GetConfigurationPageUrl()
        {
            return $"{_webHelper.GetStoreLocation()}Admin/AliExpressDropshipping/Configure";
        }

        /// <summary>
        /// Install the plugin
        /// </summary>
        /// <returns>A task that represents the asynchronous operation</returns>
        public override async Task InstallAsync()
        {            
            await base.InstallAsync();
        }

        public async Task ManageSiteMapAsync(SiteMapNode rootNode)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManagePlugins))
                return;

            var config = rootNode.ChildNodes.FirstOrDefault(node => node.SystemName.Equals("Configuration"));
            if (config == null)
                return;

            var plugins = config.ChildNodes.FirstOrDefault(node => node.SystemName.Equals("Local plugins"));

            if (plugins == null)
                return;

            var index = config.ChildNodes.IndexOf(plugins);

            if (index < 0)
                return;

            config.ChildNodes.Insert(index, new SiteMapNode
            {
                SystemName = "nopCommerce.AliExpress.Dropshipping.plugin",
                Title = "AliExpress Dropshipping",
                IconClass = "far fa-dot-circle",
                Visible = true,
                ChildNodes = new List<SiteMapNode>
                {
                    new()
                    {
                        SystemName = PluginDescriptor.SystemName,
                        Title = "Configure",
                        ControllerName = "AliExpressDropshipping",
                        ActionName = "Configure",
                        Visible = true,
                        RouteValues = new RouteValueDictionary() { { "area", AreaNames.Admin } }
                    },
                    new()
                    {
                        SystemName = PluginDescriptor.SystemName,
                        Title = "App Settings",
                        ControllerName = "AliExpressDropshipping",
                        ActionName = "AliexpressAuthorizationData",
                        Visible = true,
                        RouteValues = new RouteValueDictionary() { { "area", AreaNames.Admin } }
                    },
                    new()
                    {
                        SystemName = PluginDescriptor.SystemName,
                        Title = "Product",
                        Visible = true,
                        IconClass = "far fa-dot-circle",
                        ChildNodes = new List<SiteMapNode>
                        {
                            new()
                            {
                                SystemName = PluginDescriptor.SystemName,
                                Title = "List",
                                ControllerName = "AliExpressProduct",
                                ActionName = "List",
                                Visible = true,
                                RouteValues = new RouteValueDictionary() { { "area", AreaNames.Admin } }
                            },
                            new()
                            {
                                SystemName = PluginDescriptor.SystemName,
                                Title = "Create",
                                ControllerName = "AliExpressProduct",
                                ActionName = "Create",
                                Visible = true,
                                RouteValues = new RouteValueDictionary() { { "area", AreaNames.Admin } }
                            }
                        }
                    }
                }
            });
        }

        /// <summary>
        /// Uninstall the plugin
        /// </summary>
        /// <returns>A task that represents the asynchronous operation</returns>
        public override async Task UninstallAsync()
        {
            await base.UninstallAsync();
        }

        #endregion
    }
}
