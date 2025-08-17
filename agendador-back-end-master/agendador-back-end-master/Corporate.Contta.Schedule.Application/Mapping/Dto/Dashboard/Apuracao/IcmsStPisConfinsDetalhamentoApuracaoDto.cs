using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Application.Mapping.Dto.Dashboard.Apuracao
{
    public class IcmsStPisConfinsDetalhamentoApuracaoDto
    {
        public string NomeProduto { get; set; }
        public int NumeroNfe { get; set; }
        public string CodigoProduto { get; set; }
        public string NCM { get; set; }
        public double CFOP { get; set; }
        public double ValorNfe { get; set; }
        public double ValorProd { get; set; }
        public string Lei { get; set; }
        public string Danfe { get; set; }

    }
}
