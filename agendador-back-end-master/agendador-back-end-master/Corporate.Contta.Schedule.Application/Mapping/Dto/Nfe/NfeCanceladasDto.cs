using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Application.Mapping.Dto.Nfe
{
    public class NfeCanceladasDto
    {
        public Guid? Id { get; set; }
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

        public int ETipoNota { get; set; }

        public string ModeloTipo { get; set; }
        public string ModeloNota { get; set; }
        public string CnpjEmitente { get; set; }
    }
}
