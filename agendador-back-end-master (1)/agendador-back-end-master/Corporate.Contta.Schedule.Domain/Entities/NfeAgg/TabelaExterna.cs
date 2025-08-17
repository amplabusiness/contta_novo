using Corporate.Contta.Schedule.Domain.Entities.Base;
using System;

namespace Corporate.Contta.Schedule.Domain.Entities.NfeAgg
{
    public class TabelaExterna
    {
        public Guid Id { get; set; }
        public int Faixa { get; set; }
        public string ValorInicial { get; set; }
        public string ValorFinal { get; set; }
        public decimal Aliquota { get; set; }
        public decimal Deduzir { get; set; }

        public decimal IRPJ { get; set; }
        public decimal CSLL { get; set; }
        public decimal Cofins { get; set; }
        public decimal PISPasep { get; set; }
        public decimal CPP { get; set; }
        public decimal ICMS { get; set; }
    }
}
