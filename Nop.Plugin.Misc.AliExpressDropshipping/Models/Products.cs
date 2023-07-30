using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nop.Plugin.Misc.AliExpress.Dropshipping.Models
{
    public class Products
    {
        [JsonProperty("product_id")]
        public string? Id { get; set; }
        [JsonProperty("product_main_image_url")]
        public string? MainPicture { get; set; }
        [JsonProperty("product_title")]
        public string? Title { get; set; }
        [JsonProperty("target_original_price")]
        public string? TargetOriginalPrice { get; set; }
        [JsonProperty("original_price")]
        public string? OriginalPrice { get; set; }
        [JsonProperty("discount")]
        public string? Discount { get; set; }
        [JsonProperty("target_sale_price")]
        public string? TargetSalesPrice { get; set; }
        [JsonProperty("sale_price")]
        public string? SalesPrice { get; set; }
        [JsonProperty("product_detail_url")]
        public string? ShopUrl { get; set; }
        [JsonProperty("lastest_volume")]
        public string? LastestVolume { get; set; }
    }
}
