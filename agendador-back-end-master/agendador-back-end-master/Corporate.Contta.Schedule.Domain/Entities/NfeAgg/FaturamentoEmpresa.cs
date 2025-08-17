using Corporate.Contta.Schedule.Domain.Entities.Base;
using System;
using System.Collections.Generic;

namespace Corporate.Contta.Schedule.Domain.Entities.NfeAgg
{
    public class FaturamentoEmpresa : Entity
    {
        public Guid? CompanyInformation { get; set; }
    
        public bool FaturamentoFechado { get; set; }

        public double TotalAnual { get; set; }

        public List<Faturamento> Faturamentos { get; set; }
        public Faturamento Faturamento { get; set; }
    }
}
