using Corporate.Contta.Schedule.Domain.Entities.Base;
using System;

namespace Corporate.Contta.Schedule.Domain.Entities.FullNfeAgg
{
    public class FederalRetentions : Entity
    {
        public FederalRetentions()
        {
            this.Id = Guid.NewGuid();
        }
        public double Pis { get; set; }

        public double Confins { get; set; }

        public double Inss { get; set; }

        public double Ir { get; set; }

        public double Csll { get; set; }
    }
}
