using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Nop.Plugin.Misc.AliExpress.Dropshipping.Models
{
    public class RootJsonResponseModel
    {
        public string draw { get; set; }
        public string recordsFiltered { get; set; }
        public string recordsTotal { get; set; }
        public Object CustomProperties { get; set; }
        public IEnumerable<Products> Data { get; set; }
    }
}
