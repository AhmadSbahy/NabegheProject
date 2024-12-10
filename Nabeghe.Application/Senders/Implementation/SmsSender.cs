using Kavenegar;
using Kavenegar.Models;
using Kavenegar.Models.Enums;
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

        public SendResult SendSms(string mobile, string token,string token2,string token3,string token10,string token20, string template, VerifyLookupType type)
        {
	        return kavenegarApi.VerifyLookup(mobile, token, token2, token3,token10, token20,template,type);
        }
    }
}
