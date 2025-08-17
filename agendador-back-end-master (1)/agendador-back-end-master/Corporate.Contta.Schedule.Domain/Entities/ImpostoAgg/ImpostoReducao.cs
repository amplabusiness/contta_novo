using Corporate.Contta.Schedule.Domain.Entities.Base;
using System;

namespace Corporate.Contta.Schedule.Domain.Entities.ImpostoAgg
{
    public class ImpostoReducao : Entity
    {
        public double TotalVendas { get; set; }
        public double TotalDeducao { get; set; }
        public double Porcentagem { get; set; }
        public double BaseCalculoDedu { get; set; }

        public string Status { get; set; }

        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }

        public Guid? CompanyInformation { get; set; }
    }
}
