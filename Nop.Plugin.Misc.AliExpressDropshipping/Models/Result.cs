using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Nop.Plugin.Misc.AliExpress.Dropshipping.Models
{
    public class Result
    {
        [JsonProperty("current_record_count")]
        public string CurrentRecordCount { get; set; }
        [JsonProperty("total_record_count")]
        public string TotalRecordCount { get; set; }
        [JsonProperty("products")]
        public IEnumerable<Products> Products { get; set; }
    }
}
