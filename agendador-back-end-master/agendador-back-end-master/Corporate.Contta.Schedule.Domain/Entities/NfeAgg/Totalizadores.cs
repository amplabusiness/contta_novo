using Corporate.Contta.Schedule.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Domain.Entities.NfeAgg
{
    public class TotalNfe : Entity
    {
        public float BaseCAlIcms { get; set; }
        public double VtTotalNfe { get; set; }
        public double VlAproxTributos { get; set; }

        
    }
}
