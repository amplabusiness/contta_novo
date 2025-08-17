using Corporate.Contta.Schedule.Domain.Entities.Base;
using Corporate.Contta.Schedule.Domain.Entities.NfeAgg;
using Corporate.Contta.Schedule.Domain.Entities.ServicoEntityAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Domain.Entities.NfeModAgg
{
    public class RetornoNfeServico : Entity
    {
        public string CnpjCpf { get; set; }
        public string RazaoSocial { get; set; }
        public double Cofins { get; set; }
        public double Inss { get; set; }
        public double Ir { get; set; }
        public double Csll { get; set; }
        public double Desconto { get; set; }
        public double ValorIss { get; set; }
        public double ValorPis { get; set; }
        public double ValorCofins { get; set; }
        public double ValorIr { get; set; }
        public double ValorCsll { get; set; }
        public double ValorDeducao { get; set; }
        public double TotalNFe { get; set; }
        public double Aliquota { get; set; }    
    }

}

