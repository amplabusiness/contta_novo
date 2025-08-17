using Newtonsoft.Json;

namespace Corporate.Contta.Schedule.Infra.Models.CompanyInformation
{
    public class SecondaryActivitiesModel
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
