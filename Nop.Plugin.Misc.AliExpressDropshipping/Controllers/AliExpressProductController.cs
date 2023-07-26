using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Nop.Core.Infrastructure;
using Nop.Core;
using Nop.Plugin.Misc.AliExpress.Dropshipping.Dtos;
using Nop.Plugin.Misc.AliExpress.Dropshipping.Models;
using Nop.Plugin.Misc.AliExpress.Dropshipping.Services;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using Nop.Plugin.Misc.AliExpress.Dropshipping.AliexpressApi;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using AliexpressOpenPlatformAPI.Dtos;
using Nop.Plugin.Misc.AliExpress.Dropshipping.ViewModel;
using Newtonsoft.Json.Linq;
using Nop.Services.Directory;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Nop.Plugin.Misc.AliExpress.Dropshipping.Controllers
{
    [AutoValidateAntiforgeryToken]
    [AuthorizeAdmin]
    [Area(AreaNames.Admin)]
    public class AliExpressProductController : BasePluginController
    {
        #region Fields

        private readonly IPermissionService _permissionService;
        private readonly ICountryService _countryService;
        private static readonly HttpClient _client = new HttpClient();
        private readonly IAliExpressDropshippingDataService _aliExpressDropshippingDataService;
        
        #endregion

        #region Ctor 

        public AliExpressProductController(IPermissionService permissionService,
            IAliExpressDropshippingDataService aliExpressDropshippingDataService,
            ICountryService countryService)
        {
            _permissionService = permissionService;
            _aliExpressDropshippingDataService = aliExpressDropshippingDataService;
            _countryService = countryService;
        }

        #endregion

        #region Methods

        public async Task<IActionResult> List()
        {
            var aliexpressAuthorizationData = await _aliExpressDropshippingDataService.GetAliexpressAuthorizationData();
            ServerApiDataDto serverApiDataDto = new ServerApiDataDto();
            serverApiDataDto.AccessToken = aliexpressAuthorizationData.AccessToken;
            var webHelper = EngineContext.Current.Resolve<IWebHelper>();
            serverApiDataDto.StoreUrl = webHelper.GetStoreLocation();
            //content to verify on server
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(serverApiDataDto), Encoding.UTF8, "application/json");

            //get feed name
            HttpResponseMessage response = await _client.PostAsync(AliExpressDropshipingDefaults.ServerUrl + AliExpressDropshipingDefaults.ServerFeedNameApi, httpContent);
            var modelFeedName = await response.Content.ReadAsStringAsync();

            //get categories in json [{}]
            response = await _client.PostAsync(AliExpressDropshipingDefaults.ServerUrl + AliExpressDropshipingDefaults.ServerCategoryApi, httpContent);
            var modelCategory = await response.Content.ReadAsStringAsync();

            //prepare model
            AliProductSearchModel aliProductSearchModel = new AliProductSearchModel();
            aliProductSearchModel.JArrayFeedName = JArray.Parse(modelFeedName);
            aliProductSearchModel.JArrayCategory = JArray.Parse(modelCategory);

            var countries = await _countryService.GetAllCountriesAsync();
            var countriesAvailable = new List<String>();
            foreach (var country in countries)
            {
                countriesAvailable.Add(country.TwoLetterIsoCode);
            }
            aliProductSearchModel.CountriesAvailable = countriesAvailable;

            return View("~/Plugins/Misc.AliExpress.Dropshipping/Views/AliExpressProduct/List.cshtml", aliProductSearchModel);
        }

        #endregion
    }
}
