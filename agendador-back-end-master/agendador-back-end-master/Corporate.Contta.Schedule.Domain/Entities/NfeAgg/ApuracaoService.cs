using System;
using System.Collections.Generic;

namespace Corporate.Contta.Schedule.Domain.Entities.NfeAgg
{
    public class ApuracaoService
    {
        public ApuracaoService()
        {
            ApuracaoSaida = new ApuracaoPrestadorSaida();

            ApuracaoEntrada = new ApuracaoTomadorEntrada();            
        }

        public ApuracaoPrestadorSaida ApuracaoSaida { get; set; }
        public ApuracaoTomadorEntrada ApuracaoEntrada { get; set; }      
    }

    public class ApuracaoPrestadorSaida
    {
        public string CnpjEmitente { get; set; }
        public int MunicipioIncidencia { get; set; }
        public string CodigoMunicipio { get; set; }
        public double ValorTotalIss { get; set; }
        public double ValorTotalDeducoes { get; set; }
        public double ValorTotalServicos { get; set; }

        public double ValorToatlInss { get; set; }

    }

    public class ApuracaoTomadorEntrada
    {
        public string CnpjEmitente { get; set; }
        public int MunicipioIncidencia { get; set; }
        public string CodigoMunicipio { get; set; }
        public double ValorTotalIss { get; set; }
        public double ValorTotalDeducoes { get; set; }
        public double ValorTotalServicos { get; set; }

        public double ValorToatlInss { get; set; }
    } 
}

