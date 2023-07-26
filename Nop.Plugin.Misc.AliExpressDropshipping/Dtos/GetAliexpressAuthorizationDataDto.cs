using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.AliExpress.Dropshipping.Dtos
{
    public class GetAliexpressAuthorizationDataDto
    {
        public string Refresh_Token_Valid_Time { get; set; }
        public string Havana_Id { get; set; }
        public string Expire_Time { get; set; }
        public string Locale { get; set; }
        public string User_Nick { get; set; }
        public string Access_Token { get; set; }
        public string Refresh_Token { get; set; }
        public string Uuser_Id { get; set; }
        public string Account_Platform { get; set; }
        public string Refresh_Expires_In { get; set; }
        public string Expires_In { get; set; }
        public string Sp { get; set; }
        public string Seller_Id { get; set; }
        public string Account { get; set; }
        public string Code { get; set; }
        public string Request_Id { get; set; }
    }
}
