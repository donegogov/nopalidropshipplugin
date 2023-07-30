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
using DocumentFormat.OpenXml.EMMA;
using Nop.Core.Domain.Directory;
using System.Dynamic;
using StackExchange.Profiling.Internal;

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

        public IActionResult Index()
        {
            return RedirectToAction("List");
        }

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

            //set search id
            aliProductSearchModel.SearchTargetLanguageId = "ali-target-language";
            aliProductSearchModel.SearchFeedNameId = "ali-feed-name";
            aliProductSearchModel.SearchCategoryId = "ali-category";
            aliProductSearchModel.SearchCurrenciesId = "ali-currency";
            aliProductSearchModel.SearchCountriesAvailableId = "ali-country";
            aliProductSearchModel.SearchSortId = "ali-sort";
            aliProductSearchModel.Draw = "1";

            return View("~/Plugins/Misc.AliExpress.Dropshipping/Views/List.cshtml", aliProductSearchModel);
        }

        public async Task<IActionResult> ProductList(AliProductListSearchModel aliProductListSearchModel)
        {
            var aliexpressAuthorizationData = await _aliExpressDropshippingDataService.GetAliexpressAuthorizationData();
            GetAliexpressProductsDto getAliexpressProductsDto = new GetAliexpressProductsDto();
            getAliexpressProductsDto.AccessToken = aliexpressAuthorizationData.AccessToken;
            var webHelper = EngineContext.Current.Resolve<IWebHelper>();
            getAliexpressProductsDto.StoreUrl = webHelper.GetStoreLocation();
            getAliexpressProductsDto.TargetCurrency = !string.IsNullOrEmpty(aliProductListSearchModel.SearchCurrenciesId) ? aliProductListSearchModel.SearchCurrenciesId : "USD";
            getAliexpressProductsDto.PageNumber = !string.IsNullOrEmpty(aliProductListSearchModel.Start.ToString()) ? aliProductListSearchModel.Start.ToString() : "0";
            getAliexpressProductsDto.PageSize = !string.IsNullOrEmpty(aliProductListSearchModel.Length.ToString()) ? aliProductListSearchModel.Length.ToString() : "15";
            getAliexpressProductsDto.CategoryId = !string.IsNullOrEmpty(aliProductListSearchModel.SearchCategoryId) ? aliProductListSearchModel.SearchCategoryId : "6";
            getAliexpressProductsDto.FeedName = !string.IsNullOrEmpty(aliProductListSearchModel.SearchFeedNameId) ? aliProductListSearchModel.SearchFeedNameId : "DS bestseller";
            getAliexpressProductsDto.Country = !string.IsNullOrEmpty(aliProductListSearchModel.SearchCountriesAvailableId) ? aliProductListSearchModel.SearchCountriesAvailableId : "US";
            getAliexpressProductsDto.TargetLanguage = !string.IsNullOrEmpty(aliProductListSearchModel.SearchTargetLanguageId) ? aliProductListSearchModel.SearchTargetLanguageId : "USD";
            getAliexpressProductsDto.Sort = !string.IsNullOrEmpty(aliProductListSearchModel.SearchSortId) ? aliProductListSearchModel.SearchSortId : "priceAsc";
            //content to verify on server
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(getAliexpressProductsDto), Encoding.UTF8, "application/json");

            //get feed name
            HttpResponseMessage response = await _client.PostAsync(AliExpressDropshipingDefaults.ServerUrl + AliExpressDropshipingDefaults.ServerGetProductsApi, httpContent);
            var productList = await response.Content.ReadAsStringAsync();

            RootJson result = JsonConvert.DeserializeObject<RootJson>(productList);
            RootJsonResponseModel rootJsonResponseModel = new RootJsonResponseModel();
            rootJsonResponseModel.draw = "1";
            rootJsonResponseModel.recordsFiltered = result.Result.CurrentRecordCount;
            rootJsonResponseModel.recordsTotal = result.Result.TotalRecordCount;
            rootJsonResponseModel.Data = result.Result.Products;

            return Json(rootJsonResponseModel);
        }

        #endregion
    }
}
