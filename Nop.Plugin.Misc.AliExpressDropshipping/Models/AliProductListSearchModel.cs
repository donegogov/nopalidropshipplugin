using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.AliExpress.Dropshipping.Models
{
    public class AliProductListSearchModel : BaseSearchModel
    {
        public string SearchTargetLanguageId { get; set; }
        public string SearchCategoryId { get; set; }
        public string SearchSortId { get; set; }
        public string SearchCountriesAvailableId { get; set; }
        public string SearchCurrenciesId { get; set; }
        public string SearchFeedNameId { get; set; }
    }
}
