using DYNAMIC_BOX_WebAPI.Helper;
using DYNAMIC_BOX_WebAPI.OptionModel;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Net;
using System.Net.Mail;

namespace DYNAMIC_BOX_WebAPI.Services
{
    public class SmtpMailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        public SmtpMailService(IOptions<EmailSettings> options)
        {
            _emailSettings=options.Value;
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_emailSettings.Email);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();

            using var smpt = new MailKit.Net.Smtp.SmtpClient();
            smpt.Connect(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.StartTls);
            smpt.Authenticate(_emailSettings.Email, _emailSettings.Password);
            await smpt.SendAsync(email);
            smpt.Disconnect(true);
            
        }
    }
}
