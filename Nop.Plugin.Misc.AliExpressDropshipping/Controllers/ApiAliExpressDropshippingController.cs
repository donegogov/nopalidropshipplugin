using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Misc.AliExpress.Dropshipping.Dtos;
using Nop.Plugin.Misc.AliExpress.Dropshipping.Services;

namespace Nop.Plugin.Misc.AliExpress.Dropshipping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiAliExpressDropshippingController : Controller
    {
        private readonly IAliExpressDropshippingDataService _aliExpressDropshippingDataService;
        public ApiAliExpressDropshippingController(IAliExpressDropshippingDataService aliExpressDropshippingDataService) 
        {
            _aliExpressDropshippingDataService = aliExpressDropshippingDataService;
        }

        [Route("aliexpress-authorization-data")]
        [HttpPost]
        public async Task<IActionResult> GetAliexpressAuthorizationDataAsync([FromBody] GetAliexpressAuthorizationDataDto getAliexpressAuthorizationDataDto)
        {
            var isTrue = await _aliExpressDropshippingDataService.AddFromDto(getAliexpressAuthorizationDataDto);

            if(isTrue)
                return Ok();

            return BadRequest("Error during inserting or the item already exist");
        }
    }
}
