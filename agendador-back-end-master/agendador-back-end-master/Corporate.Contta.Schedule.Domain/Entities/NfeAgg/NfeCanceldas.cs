using Corporate.Contta.Schedule.Domain.Entities.Base;
using Corporate.Contta.Schedule.Domain.Enum;
using System;

namespace Corporate.Contta.Schedule.Domain.Entities.NfeAgg
{
    public class NfeCanceldas: Entity
    {
        public string Cnpj { get; set; }
        public string CodBarra { get; set; }

        public DateTime DhEvento { get; set; }

        public string TpEvento { get; set; }

        public string DescEvento { get; set; }

        public string NProt { get; set; }

        public string Justificativa { get; set; }

        public string RefNfe { get; set; }

        public string XEvento { get; set; }

        public DateTime DhRegEvento { get; set; }

        public Guid? CompanyInformation { get; set; }

        public Guid? EmpresaEmitId { get; set; }

        public bool IntegradaEmpresa { get; set; }

        public ETipoNota ETipoNota { get; set; }

        public string ModeloTipo { get; set; }
        public string ModeloNota { get; set; }
        public string CnpjEmitente  { get; set; }
    }
}
