using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Application.Mapping.Dto.Nfe
{
    public class ProductsDto
    {
        //public Guid? Id { get; set; }
        public double BcIcms { get; set; }

        public string Cfop { get; set; }

        public string Code { get; set; }

        public string Cst { get; set; }

        public string Description { get; set; }

        public string IcmsAliquot { get; set; }

        public double IcmsValue { get; set; }
        public double TotalValue { get; set; }

        public string IpiAliquot { get; set; }

        public double IpiValue { get; set; }

        public string NcmSh { get; set; }

        public string Quantity { get; set; }

        public string Unit { get; set; }

        public double UnitValue { get; set; }
    }
}
