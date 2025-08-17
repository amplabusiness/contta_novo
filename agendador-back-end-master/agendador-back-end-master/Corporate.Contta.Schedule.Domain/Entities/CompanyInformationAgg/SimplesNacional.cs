using System;

namespace Corporate.Contta.Schedule.Domain.Entities
{
    public class SimplesNacional
    {
        public DateTime? LastUpdate { get; set; }

        public bool? SimplesOptant { get; set; }

        public string SimplesIncluded { get; set; }

        public string SimplesExcluded { get; set; }
        public string QualificationResp { get; set; }

        public bool? SimeiOptant { get; set; }

        public int? SimpleCode { get; set; }
        public int? SimpleNumber  { get; set; }


        public string ResponsibleCpf { get; set; }
        public string ResponsibleName { get; set; }
        public string ResponsibleEmail { get; set; }
        public string ResponsibleTelefone { get; set; }
    }
}
