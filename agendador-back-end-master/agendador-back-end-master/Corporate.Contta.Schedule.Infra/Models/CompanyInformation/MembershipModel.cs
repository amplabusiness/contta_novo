using Newtonsoft.Json;
using System.Collections.Generic;

namespace Corporate.Contta.Schedule.Infra.Models.CompanyInformation
{
    public class MembershipModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("tax_id")]
        public string CpfSocio { get; set; }

        [JsonProperty("role")]
        public RoleModel Role { get; set; }
    }
}
