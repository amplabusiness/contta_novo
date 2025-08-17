using Corporate.Contta.Schedule.Domain.Entities.Base;
using System;

namespace Corporate.Contta.Schedule.Domain.Entities.FullNfeAgg
{
    public class TaxCalculation : Entity
    {
        public TaxCalculation()
        {
            this.Id = Guid.NewGuid();
        }
        public string Uf { get; set; }

        public string City { get; set; }

        public string UnconditionalDiscount { get; set; }

        public string InvoiceValue { get; set; }

        public string Deductions { get; set; }

        public string CalculationBasis { get; set; }

        public string Aliquot { get; set; }

        public string TaxValues { get; set; }
    }
}
