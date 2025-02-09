using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using Nabeghe.Application.Senders.Interfaces;

namespace Nabeghe.Application.Senders.Implementation
{
	public class EmailSender : IEmailSender
	{
		private readonly IConfiguration _configuration;

		public EmailSender(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public bool SendEmail(string email, string subject, string body, bool isHtml = true)
		{
			try
			{
				MimeMessage message = new MimeMessage();
				message.From.Add(new MailboxAddress(_configuration.GetSection("EmailSettings:DisplayName").Value,
					_configuration.GetSection("EmailSettings:FromEmail").Value));
				message.To.Add(new MailboxAddress(email, email));
				message.Subject = subject;
				var builder = new BodyBuilder();


				if (isHtml)
				{
					builder.HtmlBody = body;
				}
				else
				{
					builder.TextBody = body;
				}

				message.Body = builder.ToMessageBody();
				SmtpClient client = new SmtpClient();
				client.Connect(_configuration.GetSection("EmailSettings:DomainName").Value, 25, SecureSocketOptions.None);
				client.Authenticate(_configuration.GetSection("EmailSettings:UserName").Value,
					_configuration.GetSection("EmailSettings:Password").Value);
				client.Send(message);
				client.Disconnect(true);
				return true;
			}
			catch (Exception e)
			{
				return false;
			}
		}
	}
}
