using Corporate.Contta.Schedule.Domain.Entities.Base;
using System;

namespace Corporate.Contta.Schedule.Domain.Entities.FullNfeAgg
{
    public class Taxes : Entity
    {
        public Taxes()
        {
            this.Id = Guid.NewGuid();
        }
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
