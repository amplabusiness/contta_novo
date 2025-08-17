using Newtonsoft.Json;

namespace Corporate.Contta.Schedule.Infra.Models.CompanyInformation
{
    public class FilesModel
    {
        [JsonProperty("registration")]
        public string Registration { get; set; }
    }
}
