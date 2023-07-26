using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core;

namespace Nop.Plugin.Misc.AliExpress.Dropshipping.Domain
{
    public class AliExpressDropshippingData : BaseEntity
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string AccessTokenExpireTime { get; set; }
        public string RefreshTokenExpireTime { get; set; }
        public string AccessTokenValidTime { get; set; }
        public string RefreshTokenValidTime { get; set; }
        public string UserId { get; set; }
        public string UserNick { get; set; }
        public string AccountEmail { get; set; }
    }
}
