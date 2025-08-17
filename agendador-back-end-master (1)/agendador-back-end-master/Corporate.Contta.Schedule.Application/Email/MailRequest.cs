using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Application.Email
{
    public class MailRequest
    {
        public List<string> To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public BodyTypeEnum BodyType { get; set; }
        public List<Attachment> Attachments { get; set; }

        public enum BodyTypeEnum
        {
            Html,
            Plain_Text
        }

        public class Attachment
        {
            public string FileName { get; set; }
            public byte[] FileContent { get; set; }
        }

        public MailRequest()
        {
            To = new List<string>();
            Attachments = new List<Attachment>();
        }
    }
}
