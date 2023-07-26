using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;

namespace Nop.Plugin.Misc.AliExpress.Dropshipping.ViewModel
{
    public class AliProductSearchModel
    {
        public JArray JArrayFeedName { get; set; }
        public JArray JArrayCategory { get; set; }
        public string Country { get; set; }
        public List<String> TargetLanguage { get; } = new List<String>
        {
            "EN", "RU", "PT", "ES", "FR", "ID", "IT", "TH", "JA", "AR", "VI", "TR", "DE", "HE", "KO", "NL", "PL", "MX", "CL", "IN"
        };
        public List<String> Currencies { get; } = new List<String>
        {
            "USD", "GBP", "CAD", "EUR", "UAH", "MXN", "TRY", "RUB", "BRL", "AUD", "INR", "JPY", "IDR", "SEK", "KRW"
        };
        public List<String> CountriesAvailable { get; set; } = new List<String>();
        public List<string> Sort { get; } = new List<string> 
        {
            "priceAsc", "priceDesc", "volumeAsc", "volumeDesc", "discountAsc", "discountDesc", "DSRratingAsc", "DSRratingDesc" 
        };
    }
}
