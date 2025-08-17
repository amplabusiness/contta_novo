using Corporate.Contta.Schedule.Domain.Entities.Base;
using System;

namespace Corporate.Contta.Schedule.Domain.Entities.ImpostoAgg
{
    public class Difal : Entity
    {
        public Guid CompanyId { get; set; }
        public string NomeCliente { get; set; }
        public string CnpjEmit { get; set; }
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
        public DateTime Data { get; set; }
        public string CnpjCpf { get; set; }
        public string NumeNfe { get; set; }
        public string Chave { get; set; }
        public double ValorOp { get; set; }
        public double ValorDifal { get; set; }
        public bool Ativo { get; set; }

    }
}
