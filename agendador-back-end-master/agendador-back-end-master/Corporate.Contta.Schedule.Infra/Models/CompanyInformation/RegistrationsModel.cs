using Newtonsoft.Json;

namespace Corporate.Contta.Schedule.Infra.Models.CompanyInformation
{
    public class RegistrationsModel
    {
        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("enabled")]
        public bool? Enabled { get; set; }
    }
}
