using System;
using System.Collections.Generic;

namespace Corporate.Contta.Schedule.Domain.Entities.NfeAgg
{
    public class ApuracaoCte
    {
        public ApuracaoCte()
        {
            ApuracaoSaida = new ApuracaoSaidaCte();

            ApuracaoEntrada = new ApuracaoEntradaCte();            
        }

        public ApuracaoSaidaCte ApuracaoSaida { get; set; }
        public ApuracaoEntradaCte ApuracaoEntrada { get; set; }      
    }

    public class ApuracaoSaidaCte
    {
        public string NumNfe { get; set; }
        public string Cnpj { get; set; }
        public string TipoFrete { get; set; }
        public string Estado { get; set; }
        public string TipoCte { get; set; }
        public double VlDesconto { get; set; }
        public double VlIcms { get; set; }
        public double VlTotalServico { get; set; }
        public double TotalNfe { get; set; }      

    }

    public class ApuracaoEntradaCte
    {
        public string NumNfe { get; set; }
        public string Cnpj { get; set; }
        public string TipoFrete { get; set; }
        public string Estado { get; set; }
        public string TipoCte { get; set; }
        public double VlDesconto { get; set; }
        public double VlIcms { get; set; }
        public double VlTotalServico { get; set; }
        public double TotalNfe { get; set; }
    } 
}

