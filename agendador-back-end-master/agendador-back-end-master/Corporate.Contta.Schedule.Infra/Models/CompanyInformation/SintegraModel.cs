using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Corporate.Contta.Schedule.Infra.Models.CompanyInformation
{
    public class SintegraModel
    {
        [JsonProperty("last_update")]
        public DateTime? LastUpdate { get; set; }

        [JsonProperty("registrations")]
        public List<RegistrationsModel> Registrations { get; set; }
    }
}
