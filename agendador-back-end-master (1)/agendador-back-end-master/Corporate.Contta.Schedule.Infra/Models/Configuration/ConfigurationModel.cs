using Corporate.Contta.Schedule.Infra.Repositories.Base;
using Newtonsoft.Json;
using System;

namespace Corporate.Contta.Schedule.Infra.Models
{
    public class ConfigurationUserModel : HttpBaseResult
    {     
        [JsonProperty("dashbordTutorial")]
        public bool DashbordTutorial { get; set; }

        [JsonProperty("substituicaoTutorial")]
        public bool SubstituicaoTutorial { get; set; }

        [JsonProperty("pisConfinsTutorial")]
        public bool PisConfinsTutorial { get; set; }

        [JsonProperty("clickedDownLoadButton")]
        public bool ClickedDownLoadButton { get; set; }

        [JsonProperty("clickedChangeCompanyButton")]
        public bool ClickedChangeCompanyButton { get; set; }

        [JsonProperty("icmSInsento")]
        public bool IcmSInsento { get; set; }

        [JsonProperty("icmSImune")]
        public bool IcmSImune { get; set; }

        [JsonProperty("pISCofinsIsento")]
        public bool PISCofinsIsento { get; set; }

        [JsonProperty("pISCofinsImune")]
        public bool PISCofinsImune { get; set; }

        [JsonProperty("userId")]
        public Guid UserId { get; set; }
    }
}
