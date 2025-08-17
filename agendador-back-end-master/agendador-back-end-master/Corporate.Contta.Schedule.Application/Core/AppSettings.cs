using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Application.Core
{
    public class AppSettings
    {
        public MailSettings EmailSettings { get; set; }
        public class MailSettings
        {
            public string FromMail { get; set; }
            public string DisplayName { get; set; }
            public string SmtpUser { get; set; }
            public string Password { get; set; }
            public string Host { get; set; }
            public int Port { get; set; }
            public string TargetName { get; set; }
        }
    }
}
