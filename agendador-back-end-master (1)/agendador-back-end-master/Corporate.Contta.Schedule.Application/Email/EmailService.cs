using Corporate.Contta.Schedule.Application.Core;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Application.Email
{
    public class EmailService: IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        private readonly AppSettings _appSettings;
        public EmailService(ILogger<EmailService> logger, IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _appSettings = appSettings.Value;
        }

        public bool SendMail(MailRequest mailRequest)
        {
            try
            {
                var mailMessage = ConfigureMailSender(mailRequest);
                using var smtp = new SmtpClient();               
                smtp.Connect(_appSettings.EmailSettings.Host, _appSettings.EmailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_appSettings.EmailSettings.SmtpUser, _appSettings.EmailSettings.Password);
                smtp.Send(mailMessage);
                smtp.Disconnect(true);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email");
            }
            return false;
        }

        public async Task<bool> SendMailAsync(MailRequest mailRequest)
        {
            try
            {
                var mailMessage = ConfigureMailSender(mailRequest);
                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(_appSettings.EmailSettings.Host, _appSettings.EmailSettings.Port, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_appSettings.EmailSettings.SmtpUser, _appSettings.EmailSettings.Password);
                await smtp.SendAsync(mailMessage);
                await smtp.DisconnectAsync(true);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email async");
            }
            return false;
        }

        private MimeMessage ConfigureMailSender(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_appSettings.EmailSettings.FromMail));
            foreach (string mail in mailRequest.To)
                email.To.Add(MailboxAddress.Parse(mail));

            email.Subject = mailRequest.Subject;
            BodyBuilder bodyBuilder;
            if (mailRequest.BodyType == MailRequest.BodyTypeEnum.Plain_Text)
                bodyBuilder = new BodyBuilder { TextBody = mailRequest.Body };
            else
                bodyBuilder = new BodyBuilder { HtmlBody = mailRequest.Body };

            if (mailRequest.Attachments != null && mailRequest.Attachments.Any())
            {
                byte[] fileBytes;
                foreach (var attachment in mailRequest.Attachments)
                {
                    using (var ms = new MemoryStream(attachment.FileContent))
                    {
                        fileBytes = ms.ToArray();
                    }
                    bodyBuilder.Attachments.Add(attachment.FileName, fileBytes);
                }
            }
            email.Body = bodyBuilder.ToMessageBody();
            return email;
        }
    }
}
