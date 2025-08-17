using Corporate.Contta.Schedule.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Domain.Entities.EstoqueAgg
{
    public class Estoque : Entity
    {      
        public string CodProd { get; set; }

        public string Descricao { get; set; }

        public string Marca { get; set; }
        public string VlUnitario { get; set; }

        public string UniMedida { get; set; }

        public decimal Quantidade { get; set; }

        public string CodBarra { get; set; }

        public Guid? CompanyInformation { get; set; }

    }
}
