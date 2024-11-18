using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kavenegar.Models;

namespace Nabeghe.Application.Senders.Interfaces
{
    public interface ISmsSender
    {
        SendResult SendSms(string mobile, string message);
    }
}
