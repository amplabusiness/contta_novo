using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Domain.Entities.NfeAgg
{
    public class LigroFiscalRodape
    {

        public double TotalCfopTrib { get; set; }
        public double TotalCfopNaoTrib { get; set; }
        public double Cfop { get; set; }
        public double TotalBase { get; set; }
        public double TotalLig { get; set; }
        public double Outros { get; set; }
    }
}
