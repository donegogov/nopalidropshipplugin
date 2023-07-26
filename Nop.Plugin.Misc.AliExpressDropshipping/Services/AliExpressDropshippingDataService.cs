using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Data;
using Nop.Plugin.Misc.AliExpress.Dropshipping.AliexpressApi;
using Nop.Plugin.Misc.AliExpress.Dropshipping.Domain;
using Nop.Plugin.Misc.AliExpress.Dropshipping.Dtos;

namespace Nop.Plugin.Misc.AliExpress.Dropshipping.Services
{
    internal class AliExpressDropshippingDataService : IAliExpressDropshippingDataService
    {
        private readonly IRepository<AliExpressDropshippingData> _aliExpressDropshippingDataRepository;
        public AliExpressDropshippingDataService(IRepository<AliExpressDropshippingData> aliExpressDropshippingDataRepository)
        {
            _aliExpressDropshippingDataRepository = aliExpressDropshippingDataRepository;
        }
        public async Task<bool> Add(AliExpressDropshippingData data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            /*var aliData = await _aliExpressDropshippingDataRepository.Table.FirstOrDefaultAsync(x => x.AccountEmail == data.AccountEmail);
            if(aliData == null)
            {*/
            var aliexpressAuthorizationData = await _aliExpressDropshippingDataRepository.Table.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            data.Id = aliexpressAuthorizationData.Id + 1;
            await _aliExpressDropshippingDataRepository.InsertAsync(data);
            return true;
            /*}
            return false;*/
        }

        public async Task<bool> AddFromDto(GetAliexpressAuthorizationDataDto data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            /*var aliData = await _aliExpressDropshippingDataRepository.Table.FirstOrDefaultAsync(x => x.AccountEmail == data.Account);
            if (aliData == null)
            {*/
            var aliexpressAuthorizationData = await _aliExpressDropshippingDataRepository.Table.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            AliExpressDropshippingData aliExpressDropshippingData = new AliExpressDropshippingData();
            aliExpressDropshippingData.Id = aliexpressAuthorizationData.Id + 1;
            aliExpressDropshippingData.AccessToken = data.Access_Token;
            aliExpressDropshippingData.RefreshToken = data.Refresh_Token;
            aliExpressDropshippingData.AccessTokenExpireTime = data.Expire_Time;
            aliExpressDropshippingData.AccessTokenValidTime = data.Expires_In;
            aliExpressDropshippingData.RefreshTokenExpireTime = data.Refresh_Token_Valid_Time;
            aliExpressDropshippingData.RefreshTokenValidTime = data.Refresh_Expires_In;
            aliExpressDropshippingData.AccountEmail = data.Account;

            await _aliExpressDropshippingDataRepository.InsertAsync(aliExpressDropshippingData);
            return true;
            /*}
            return false;*/
        }
        public async Task<AliExpressDropshippingData> GetAliexpressAuthorizationData()
        {
            var aliexpressAuthorizationData = await _aliExpressDropshippingDataRepository.Table.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            aliexpressAuthorizationData.AccessTokenExpireTime = DateTimeOffset.FromUnixTimeMilliseconds(Convert.ToInt64(aliexpressAuthorizationData.AccessTokenExpireTime)).ToString();
            aliexpressAuthorizationData.AccessTokenValidTime = TimeSpan.FromSeconds(Convert.ToInt64(aliexpressAuthorizationData.AccessTokenValidTime)).ToString();
            aliexpressAuthorizationData.RefreshTokenExpireTime = DateTimeOffset.FromUnixTimeMilliseconds(Convert.ToInt64(aliexpressAuthorizationData.RefreshTokenExpireTime)).ToString();
            aliexpressAuthorizationData.RefreshTokenValidTime = TimeSpan.FromSeconds(Convert.ToInt64(aliexpressAuthorizationData.RefreshTokenValidTime)).ToString();
            if (aliexpressAuthorizationData == null)
            {
                return null;
            }
            return aliexpressAuthorizationData;
        }

        

    }
}
