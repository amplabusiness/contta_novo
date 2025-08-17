using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Application.Mapping.Dto.Nfe
{
    public class DemonstrativeDto
    {
        public string ProvidedServiceCity { get; set; }

        public string TaxedServiceCity { get; set; }

        public double ServicesValue { get; set; }

        public double UnconditionalDiscount { get; set; }

        public double RetainedIssqn { get; set; }

        public double LiquidValue { get; set; }

        public double FederalRetentions { get; set; }
    }
}
