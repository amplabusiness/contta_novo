using Corporate.Contta.Schedule.Domain.Entities.Base;
using System;
using System.Collections.Generic;

namespace Corporate.Contta.Schedule.Domain.Entities.NfeAgg
{
    public class Faturamento : Entity
    {   
        public DateTime DataFaturamento { get; set; }
        public int Ano { get; set; }
        public double ValorFaturamento { get; set; }      
    }
}
