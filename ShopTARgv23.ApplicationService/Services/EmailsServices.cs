using ShopTARgv23.Core.ServiceInterface;

using MimeKit;
using MailKit.Net.Smtp;

using ShopTARgv23.Core.Dto;
using Microsoft.Extensions.Configuration;



namespace ShopTARgv23.ApplicationService.Services
{
    public class EmailsServices : IEmailsServices
    {

        private readonly IConfiguration _config;

        public EmailsServices(IConfiguration config) { _config = config; }

		public void SendEmail(EmailDto dto)
		{
			var email = new MimeMessage();
			email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUserName").Value));
			email.To.Add(MailboxAddress.Parse(dto.To));
			email.Subject = dto.Subject;
			email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
			{
				Text = dto.Body
			};
			using var smtp = new SmtpClient();
			smtp.Connect(_config.GetSection("EmailHost").Value, 587, MailKit.Security.SecureSocketOptions.StartTls);
			smtp.Authenticate(_config.GetSection("EmailUserName").Value, _config.GetSection("EmailPassword").Value);
			smtp.Send(email);
			smtp.Disconnect(true);
		}
	}
}
