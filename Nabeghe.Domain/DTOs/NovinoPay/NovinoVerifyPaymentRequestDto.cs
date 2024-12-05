using Newtonsoft.Json;

namespace Nabeghe.Domain.DTOs.NovinoPay
{
    public class NovinoVerifyPaymentRequestDto
    {
        [JsonProperty("merchant_id")]
        public string MerchantId { get; set; }

        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("authority")]
        public string Authority { get; set; }
    }
}
