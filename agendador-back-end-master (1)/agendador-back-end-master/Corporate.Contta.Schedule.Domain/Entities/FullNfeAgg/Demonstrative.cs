using Corporate.Contta.Schedule.Domain.Entities.Base;
using System;

namespace Corporate.Contta.Schedule.Domain.Entities.FullNfeAgg
{
    public class Demonstrative : Entity
    {
        public Demonstrative()
        {
            this.Id = Guid.NewGuid();
        }
        public string ProvidedServiceCity { get; set; }

        public string TaxedServiceCity { get; set; }

        public double ServicesValue { get; set; }

        public double UnconditionalDiscount { get; set; }

        public double RetainedIssqn { get; set; }

        public double LiquidValue { get; set; }

        public double FederalRetentions { get; set; }
    }
}
