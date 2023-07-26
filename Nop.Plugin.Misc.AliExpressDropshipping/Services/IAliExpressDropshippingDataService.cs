using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Plugin.Misc.AliExpress.Dropshipping.AliexpressApi;
using Nop.Plugin.Misc.AliExpress.Dropshipping.Domain;
using Nop.Plugin.Misc.AliExpress.Dropshipping.Dtos;

namespace Nop.Plugin.Misc.AliExpress.Dropshipping.Services
{
    public interface IAliExpressDropshippingDataService
    {
        Task<bool> Add(AliExpressDropshippingData data);
        Task<bool> AddFromDto(GetAliexpressAuthorizationDataDto data);
        Task<AliExpressDropshippingData> GetAliexpressAuthorizationData();
    }
}
