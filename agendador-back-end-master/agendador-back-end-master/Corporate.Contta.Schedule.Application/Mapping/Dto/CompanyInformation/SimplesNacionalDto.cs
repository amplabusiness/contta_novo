using System;

namespace Corporate.Contta.Schedule.Domain.Entities
{
    public class SimplesNacionalDto
    {
        public DateTime? LastUpdate { get; set; }

        public bool? SimplesOptant { get; set; }

        public string SimplesIncluded { get; set; }

        public string SimplesExcluded { get; set; }

        public bool? SimeiOptant { get; set; }

        public string ResponsibleName { get; set; }
        public string ResponsibleEmail { get; set; }
        public string ResponsibleTelefone { get; set; }
    }
}
