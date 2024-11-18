using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabeghe.Application.Statics
{
    public class KavenegarStatics
    {
        [JsonProperty("ApiKey")]
        public static string ApiKey { get; set; }
        [JsonProperty("Sender")]
        public static string Sender { get; set; }
    }
}
