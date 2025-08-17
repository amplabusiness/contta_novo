using Corporate.Contta.Schedule.Domain.Entities.Base;
using Corporate.Contta.Schedule.Domain.Enum;
using System;

namespace Corporate.Contta.Schedule.Domain.Entities.ServicoEntityAgg
{
    public class ServicoEntity : Entity
    {
        public Guid EmpresaEmetId { get; set; }

        public string Numero { get; set; }

        public DateTime DataEmissao { get; set; }

        public string BaseCalculo { get; set; }

        public double Aliquota { get; set; }

        public double ValorIss { get; set; }

        public string Serie { get; set; }

        public string Tipo { get; set; }

        public string Status { get; set; }

        public string CodigoVerificacao { get; set; }

        public double ValorServicos { get; set; }

        public double ValorDeducoes { get; set; }

        public double ValorPis { get; set; }

        public double ValorCofins { get; set; }

        public double ValorInss { get; set; }

        public double ValorIr { get; set; }

        public double ValorCsll { get; set; }

        public double DescontoIncondicionado { get; set; }

        public string CodigoTributacaoMunicipio { get; set; }

        public string Discriminacao { get; set; }

        public string CodigoMunicipio { get; set; }

        public int MunicipioIncidencia { get; set; }

        public Guid CompanyInformation { get; set; }

        public string ModeloNota { get; set; }
        public string CnpjEmitente { get; set; }

        public Guid? EmpresaDesId { get; set; }

        public ETipoNota ETipoNota { get; set; }
    }
}
