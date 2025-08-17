using System;
using System.Collections.Generic;

namespace Corporate.Contta.Schedule.Domain.Entities.DashboardAgg
{
    public class HomeCompany
    {
        public bool Status { get; set; }
        public string RazaoSocial { get; set; }
        public double FaturamentoMes { get; set; }
        public string Aliquota { get; set; }
        public DateTime PrVisaoSimples { get; set; }
        public DateTime DataFechaSimples { get; set; }
        public DateTime ValiCertificado { get; set; }
        public string Declaracao { get; set; }
        public string Das { get; set; }
        public string Extrato { get; set; }
        public string Difal { get; set; }
    }
}
