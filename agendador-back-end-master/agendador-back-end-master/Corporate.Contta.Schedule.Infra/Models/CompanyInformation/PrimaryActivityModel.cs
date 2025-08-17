using Newtonsoft.Json;

namespace Corporate.Contta.Schedule.Infra.Models.CompanyInformation
{
    public class PrimaryActivityModel
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
