using Newtonsoft.Json;

namespace Corporate.Contta.Schedule.Infra.Models.CompanyInformation
{
    public class RegistrationModel
    {
        [JsonProperty("ATIVA")]
        public string Ativa { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("status_date")]
        public string StatusDate { get; set; }

        [JsonProperty("status_reason")]
        public string StatusReason { get; set; }

        [JsonProperty("special_status")]
        public string SpecialStatus { get; set; }

        [JsonProperty("special_status_date")]
        public string SpecialStatusDate { get; set; }
    }
}
