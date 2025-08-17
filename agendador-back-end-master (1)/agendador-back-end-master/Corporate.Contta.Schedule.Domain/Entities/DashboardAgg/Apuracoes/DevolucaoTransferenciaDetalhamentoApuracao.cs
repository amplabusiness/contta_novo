using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Domain.Entities.DashboardAgg.Apuracoes
{
    public class DevolucaoTransferenciaDetalhamentoApuracao
    {
        public int NumeroNfe { get; set; }
        public double ValorTotalNfe { get; set; }
        public double ValorTotalProd { get; set; }
        public string NfeRef { get; set; }
        public DateTime? DataEmi { get; set; }
        public string ModeloTipo { get; set; }
        public string Danfe { get; set; }

    }
}
