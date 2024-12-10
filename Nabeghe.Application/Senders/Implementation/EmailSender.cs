using Microsoft.Extensions.Configuration;
using MimeKit;
using Nabeghe.Application.Senders.Interfaces;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
namespace Nabeghe.Application.Senders.Implementation
{
	public class EmailSender : IEmailSender
	{
		private readonly IConfiguration _configuration;

		public EmailSender(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public async Task<bool> SendEmail(string email, string subject, string body, bool isHtml = true)
		{
			MimeMessage message = new MimeMessage();
			message.From.Add(new MailboxAddress(_configuration.GetSection("EmailSettings:DisplayName").Value, _configuration.GetSection("EmailSettings:FromEmail").Value));
			message.To.Add(new MailboxAddress(email, email));
			message.Subject = subject;
			var builder = new BodyBuilder();

			builder.HtmlBody = body;
			message.Body = builder.ToMessageBody();
			SmtpClient client = new SmtpClient();
			await client.ConnectAsync(_configuration.GetSection("EmailSettings:DomainName").Value, 465, true);
			await client.AuthenticateAsync(_configuration.GetSection("EmailSettings:UserName").Value, _configuration.GetSection("EmailSettings:Password").Value);
			await client.SendAsync(message);
			await client.DisconnectAsync(true);
			return true;
		}
	}
}
