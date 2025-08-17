using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Application.Mapping.Dto.Nfe
{
    public class TaxesDto
    {
        //public Guid? Id { get; set; }
        public float BcIcms { get; set; }

        public float BcIcmsSt { get; set; }

        public double Discount { get; set; }

        public double IcmsStValue { get; set; }

        public double IcmsValue { get; set; }

        public double IpiValue { get; set; }

        public double Other { get; set; }

        public double TotalInvoiceValue { get; set; }

        public double TotalProductsValue { get; set; }
    }
}
