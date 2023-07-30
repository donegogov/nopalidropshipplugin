using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Nop.Plugin.Misc.AliExpress.Dropshipping.Models
{
    public class RootJson
    {
        [JsonProperty("res_code")]
        public string ResponseCode { get; set; }
        [JsonProperty("result")]
        public Result Result { get; set; }
    }
}
