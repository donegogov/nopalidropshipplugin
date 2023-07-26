using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Nop.Plugin.Misc.AliExpress.Dropshipping.ViewModel
{
    public class DataTableSearchModel
    {
        public string FeedNameId { get; set; }
        public string CategoryId { get; set; }
        public string CountryId { get; set; }
        public string TargetLanguageId { get; set; }
        public string CurrencyId { get; set; }
        public string SortId { get; set; }
        public JArray Category { get; set; }
        public string CategoryName { get; set; }
        public string AliCategoryId { get; set; }
    }
}
