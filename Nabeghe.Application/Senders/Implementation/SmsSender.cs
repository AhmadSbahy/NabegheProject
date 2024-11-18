using Kavenegar;
using Kavenegar.Models;
using Nabeghe.Application.Senders.Interfaces;
using Nabeghe.Application.Statics;

namespace Nabeghe.Application.Senders.Implementation
{
    public class SmsSender : ISmsSender
    {
        private readonly KavenegarApi kavenegarApi;

        public SmsSender()
        {
            string apiKey = KavenegarStatics.ApiKey;
            kavenegarApi = new KavenegarApi(apiKey);
        }

        public SendResult SendSms(string mobile, string message)
        {
            return kavenegarApi.Send(KavenegarStatics.Sender, mobile, message);
        }
    }
}
