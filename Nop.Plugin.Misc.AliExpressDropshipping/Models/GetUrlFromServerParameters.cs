using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.AliExpress.Dropshipping.Models
{
    public class GetUrlFromServerParameters
    {
        public string StoreUrl { get; set; }
        public string Url { get; set; }
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
    }
}
