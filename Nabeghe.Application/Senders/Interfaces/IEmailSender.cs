using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabeghe.Application.Senders.Interfaces
{
    public interface IEmailSender
    {
	    Task<bool> SendEmail(string email, string subject, string body, bool isHtml = true);
	}
}
