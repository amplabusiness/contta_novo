using Corporate.Contta.Schedule.Domain.Entities.Base;

namespace Corporate.Contta.Schedule.Domain.Entities.FullNfeAgg
{
    public class FullNFE : Entity
    {
        public Taker Taker { get; set; }

        public Activity Activity { get; set; }

        public FederalRetentions FederalRetentions { get; set; }

        public Demonstrative Demonstrative { get; set; }

        public TaxCalculation TaxCalculation { get; set; }

        public Sale Sale { get; set; }
    }
}
