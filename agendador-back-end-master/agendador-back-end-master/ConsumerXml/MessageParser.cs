using Newtonsoft.Json;
using System;
using System.IO;
using System.Xml.Serialization;

namespace ConsumerXml
{
    public static class MessageParser
    {
        public static NfeProc ParseNfeProc(string message)
        {
            if (string.IsNullOrWhiteSpace(message)) return null;

            // Try XML first if it looks like XML
            if (message.TrimStart().StartsWith("<"))
            {
                try
                {
                    var ser = new XmlSerializer(typeof(NfeProc));
                    using var sr = new StringReader(message);
                    return (NfeProc)ser.Deserialize(sr);
                }
                catch
                {
                    // ignore and try JSON fallback
                }
            }

            try
            {
                var settings = new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore,
                };
                return JsonConvert.DeserializeObject<NfeProc>(message, settings);
            }
            catch
            {
                return null;
            }
        }
    }
}
