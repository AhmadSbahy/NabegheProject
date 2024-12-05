using Newtonsoft.Json;

namespace Nabeghe.Domain.DTOs.NovinoPay
{
    public class NovinoGetPaymentUrlResponseDto
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public NovinoGetPaymentUrlRequestData Data { get; set; }

        [JsonProperty("errors")]
        public object Errors { get; set; }
    }

    public class NovinoGetPaymentUrlRequestData
    {
        [JsonProperty("wage")]
        public int Wage { get; set; }

        [JsonProperty("WagePayer")]
        public string wage_payer { get; set; }

        [JsonProperty("Authority")]
        public string authority { get; set; }

        [JsonProperty("PaymentUrl")]
        public string payment_url { get; set; }
    }
}
