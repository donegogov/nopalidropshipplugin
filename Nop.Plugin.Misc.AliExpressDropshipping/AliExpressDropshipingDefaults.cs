using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Presentation;
using Humanizer;
using Org.BouncyCastle.Bcpg;
using static LinqToDB.Reflection.Methods.LinqToDB.Insert;

namespace Nop.Plugin.Misc.AliExpress.Dropshipping
{
    public static class AliExpressDropshipingDefaults
    {
        public static string AppKey { get; } = "33615924";
        public static string AppSecret { get; } = "5cb2e1ecd4a10a670d7c90bf759ee0cf";
        public static string AliApiURL { get; } = "https://api-sg.aliexpress.com";
        public static string ServerUrl { get; } = "https://alidropship.azurewebsites.net/api/";
        public static string ServerFeedNameApi { get; } = "DropShippingApi/feed-name";
        public static string ServerCategoryApi { get; } = "DropShippingApi/category";
        /// <summary>
        /// Gets a name of the route to the import contacts callback
        /// </summary>
        public static string AppSettingsRoute => "Plugin.Misc.AliExpress.Dropshipping.AppSettings";
        /// <summary>
        /// Gets a name of the route to the import contacts callback
        /// </summary>
        public static string ConfigureRoute => "Plugin.Misc.AliExpress.Dropshipping.Configure";
        /// <summary>
        /// Gets a name of the route to the import contacts callback
        /// </summary>
        public static string ListProductRoute => "Plugin.Misc.AliExpress.Dropshipping.List";
        /// <summary>
        /// Gets a name of the route to the import contacts callback
        /// </summary>
        public static string CreateProductRoute => "Plugin.Misc.AliExpress.Dropshipping.Create";
        /// <summary>
        /// Gets a name of the route to the import contacts callback
        /// </summary>
        public static string EditProductRoute => "Plugin.Misc.AliExpress.Dropshipping.Edit";
        public static List<string> TargetLanguage { get; } = new List<string> { "EN", "RU", "PT", "ES", "FR", "ID", "IT", "TH", "JA", "AR", "VI", "TR", "DE", "HE", "KO", "NL", "PL", "MX", "CL", "IN" };
        public static List<string> Currency { get; } = new List<string> { "USD", "GBP", "CAD", "EUR", "UAH", "MXN", "TRY", "RUB", "BRL", "AUD", "INR", "JPY", "IDR", "SEK", "KRW" };
        public static List<string> Sort { get; } = new List<string> { "priceAsc", "priceDesc", "volumeAsc", "volumeDesc", "discountAsc", "discountDesc", "DSRratingAsc", "DSRratingDesc" };
    }
}