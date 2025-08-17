using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Application.Email
{
    public interface IEmailService
    {
        bool SendMail(MailRequest mailRequest);
        Task<bool> SendMailAsync(MailRequest mailRequest);
    }
}
