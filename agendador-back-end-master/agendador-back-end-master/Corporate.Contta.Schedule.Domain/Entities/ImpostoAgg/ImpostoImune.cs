using Corporate.Contta.Schedule.Domain.Entities.Base;
using System;

namespace Corporate.Contta.Schedule.Domain.Entities.ImpostoAgg
{
    public class ImpostoImune : Entity
    {      
        public string NCM { get; set; }
        public string Descricao { get; set; }

        public string Status { get; set; }

        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }

        public Guid? CompanyInformation { get; set; }
    }
}
