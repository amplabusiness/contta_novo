using Corporate.Contta.Schedule.Domain.Entities.Base;
using Corporate.Contta.Schedule.Domain.Entities.NfeAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Domain.Entities.NfeModAgg
{
    public class RetornoNfeMod57 : Entity
    {
        public long TotalDeItens { get; set; }

        public int TotalDePaginas { get; set; }

        public int PaginaAtual { get; set; }

        public string NumeroNotaFiscal { get; set; }
        public string Cnpj { get; set; }
        public string TipoFrete { get; set; }
        public string Uf { get; set; }
        public string Municipio { get; set; }
        public string TipoCte { get; set; }
        public double ValorDesconto { get; set; }
        public double ValorIcms { get; set; }
        public double ValorTotalServico { get; set; }
        public double ValorTotal { get; set; } 
    }
}
