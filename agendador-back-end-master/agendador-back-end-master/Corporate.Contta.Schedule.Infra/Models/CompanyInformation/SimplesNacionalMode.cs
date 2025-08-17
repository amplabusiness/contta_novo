using Newtonsoft.Json;
using System;

namespace Corporate.Contta.Schedule.Infra.Models.CompanyInformation
{
    public class SimplesNacionalMode
    {
        [JsonProperty("last_update")]
        public DateTime? LastUpdate { get; set; }

        [JsonProperty("simples_optant")]
        public bool? SimplesOptant { get; set; }

        [JsonProperty("simples_included")]
        public string SimplesIncluded { get; set; }

        [JsonProperty("simples_excluded")]
        public string SimplesExcluded { get; set; }

        [JsonProperty("simei_optant")]
        public bool? SimeiOptant { get; set; }

        public int? SimpleNumber { get; set; }
        public string ResponsibleName { get; set; }
        public string ResponsibleEmail { get; set; }
        public string ResponsibleTelefone { get; set; }
    }
}
