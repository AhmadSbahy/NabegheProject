using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kavenegar.Models;
using Kavenegar.Models.Enums;

namespace Nabeghe.Application.Senders.Interfaces
{
    public interface ISmsSender
    {
	    SendResult SendSms(string mobile, string token, string token2, string token3, string token10, string token20, string template, VerifyLookupType type);
    }
}
