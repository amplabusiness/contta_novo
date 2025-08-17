using System;

namespace ConttaComsumidor.Models.CompanyInformationAgg
{
    public class SimplesNacional
    {
        public DateTime? LastUpdate { get; set; }

        public bool? SimplesOptant { get; set; }

        public string SimplesIncluded { get; set; }

        public string SimplesExcluded { get; set; }

        public bool? SimeiOptant { get; set; }

        public int? SimpleNumber { get; set; }

        public string ResponsibleName { get; set; }
        public string ResponsibleEmail { get; set; }
        public string ResponsibleTelefone { get; set; }
    }
}
