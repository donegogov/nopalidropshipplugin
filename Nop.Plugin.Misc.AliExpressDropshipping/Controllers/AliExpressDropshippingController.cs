using System.Net;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using System.Linq;
using Nop.Core.Infrastructure;
using Nop.Core;
using Nop.Plugin.Misc.AliExpress.Dropshipping.Models;
using System.Net.Http;
using System.Text.Json.Nodes;
using Newtonsoft.Json;
using Nop.Plugin.Misc.AliExpress.Dropshipping.Dtos;
using Nop.Plugin.Misc.AliExpress.Dropshipping.Services;

namespace Nop.Plugin.Misc.AliExpress.Dropshipping.Controllers
{
    [AutoValidateAntiforgeryToken]
    [AuthorizeAdmin]
    [Area(AreaNames.Admin)]
    public class AliExpressDropshippingController : BasePluginController
    {
        #region Fields

        private readonly IPermissionService _permissionService;
        private static readonly HttpClient _client = new HttpClient();
        private readonly IAliExpressDropshippingDataService _aliExpressDropshippingDataService;

        #endregion

        #region Ctor 
        public AliExpressDropshippingController(IPermissionService permissionService, IAliExpressDropshippingDataService aliExpressDropshippingDataService)
        {
            _permissionService = permissionService;
            _aliExpressDropshippingDataService = aliExpressDropshippingDataService;
        }

        #endregion

        #region Methods

        public virtual async Task<IActionResult> Configure()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManagePlugins))
                return AccessDeniedView();

            var getUrlServerParameters = new GetUrlFromServerParameters();
            var webHelper = EngineContext.Current.Resolve<IWebHelper>();
            getUrlServerParameters.StoreUrl = webHelper.GetStoreLocation();

            using HttpResponseMessage response = await _client.GetAsync("https://alidropship.azurewebsites.net/api/Token/ali-dropship-url?storeurl=" + getUrlServerParameters.StoreUrl);
            if(response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<GetAuthorizeUrlDto>(jsonResponse);
                getUrlServerParameters.Url = json.AuthorizeURL;
                getUrlServerParameters.IsError = false;
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                getUrlServerParameters.ErrorMessage = jsonResponse;
                getUrlServerParameters.IsError = true;
                getUrlServerParameters.Url = "";
            }
            return View("~/Plugins/Misc.AliExpress.Dropshipping/Views/Configure.cshtml", getUrlServerParameters);
        }

        public virtual async Task<IActionResult> AliexpressAuthorizationData()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManagePlugins))
                return AccessDeniedView();

            var aliData = await _aliExpressDropshippingDataService.GetAliexpressAuthorizationData();
            return View("~/Plugins/Misc.AliExpress.Dropshipping/Views/AppSettings.cshtml", aliData);
        }

        #endregion
    }
}
