using Newtonsoft.Json;
using RestSharp.Serializers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace PortalMoedas.Extensions
{
    internal class ERPJsonSerializer : ISerializer
    {
        protected JsonSerializer Serializer { get; set; }

        /// <summary>
        /// Default serializer
        /// </summary>
        public ERPJsonSerializer()
        {
            ContentType = "application/json";

            Serializer = JsonSerializer.Create();

            Serializer.NullValueHandling = NullValueHandling.Ignore;
        }

        /// <summary>
        /// Default serializer with overload for allowing custom Json.NET settings
        /// </summary>
        public ERPJsonSerializer(JsonSerializer serializer)
        {
            ContentType = "application/json";
            Serializer = serializer;
        }

        /// <summary>
        /// Unused for JSON Serialization
        /// </summary>
        public virtual string DateFormat { get; set; }
        /// <summary>
        /// Unused for JSON Serialization
        /// </summary>
        public virtual string RootElement { get; set; }
        /// <summary>
        /// Unused for JSON Serialization
        /// </summary>
        public virtual string Namespace { get; set; }
        /// <summary>
        /// Content type for serialized content
        /// </summary>
        public virtual string ContentType { get; set; }

        /// <summary>
        /// Serialize the object as JSON
        /// </summary>
        /// <param name="obj">Object to serialize</param>
        /// <returns>JSON as String</returns>
        public virtual string Serialize(object obj)
        {
            using (var stringWriter = new StringWriter())
            using (var jsonTextWriter = new JsonTextWriter(stringWriter))
            {
#if DEBUG
                jsonTextWriter.Formatting = Formatting.Indented;
#endif

                Serializer.Serialize(jsonTextWriter, obj);

                return stringWriter.ToString();
            }
        }

    }
}
