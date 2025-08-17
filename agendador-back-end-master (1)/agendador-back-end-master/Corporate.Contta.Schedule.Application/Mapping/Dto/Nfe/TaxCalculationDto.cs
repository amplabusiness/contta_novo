using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Application.Mapping.Dto.Nfe
{
    public class TaxCalculationDto
    {
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
