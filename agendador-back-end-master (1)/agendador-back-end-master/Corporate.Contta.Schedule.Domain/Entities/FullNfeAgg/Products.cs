using Corporate.Contta.Schedule.Domain.Entities.Base;
using System;

namespace Corporate.Contta.Schedule.Domain.Entities.FullNfeAgg
{
    public class Products : Entity
    {
        public Products()
        {
            this.Id = Guid.NewGuid();
        }
        public double BcIcms { get; set; }

        public string Cfop { get; set; }

        public string Code { get; set; }

        public string Cst { get; set; }

        public string Description { get; set; }

        public string IcmsAliquot { get; set; }

        public double IcmsValue { get; set; }

        public string IpiAliquot { get; set; }

        public double IpiValue { get; set; }

        public string NcmSh { get; set; }

        public float Quantity { get; set; }

        public string Unit { get; set; }

        public double UnitValue { get; set; }

        public double TotalValue { get; set; }
    
    }
}
