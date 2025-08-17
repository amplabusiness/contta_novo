using Corporate.Contta.Schedule.Domain.Entities.Base;
using System;
using System.Collections.Generic;

namespace Corporate.Contta.Schedule.Domain.Entities.BlocoE
{
    public class AjusteNfe : Entity
    {
        public DateTime DhEmiss { get; set; }
        public Guid CompanyInformation { get; set; }
        public string CodAjuste { get; set; }
        public string DescricaoAjuste { get; set; }
        public string Tipo { get; set; }
        public string TipoNota { get; set; }
        public string Totalizador { get; set; }
        public double TotalNfe { get; set; }
        public double Aliquota { get; set; }
        public double TotalCalculo { get; set; }
        public double VlTotalNfe { get; set; }

        public List<string> Ncms { get; set; }
        public List<string> Cfops { get; set; }
        public List<string> Csts { get; set; }
    }

    public class NCMAjuste
    {
        public string NCM { get; set; }
    }


    public class CFOPjuste
    {
        public string CFOP { get; set; }
    }

    public class CSTjuste
    {
        public string CST { get; set; }
    }

}
