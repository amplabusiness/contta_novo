using Corporate.Contta.Schedule.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Domain.Entities.BlocoE
{
    public class AgBlocoE: Entity
    {
        public Guid CompanyInformationId { get; set; }
        public E100 E100 { get; set; }
        public E110 E110 { get; set; }
        public List<E111> E111 { get; set; }
        public E113 E113 { get; set; }
        public E115 E115 { get; set; }
        public E116 E116 { get; set; }
    }
    public class E100
    {
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
    }
    public class E110
    {
        public double ValorDebitoImpostos { get; set; }
        public double ValorAjustesDebitoDocFiscal { get; set; }
        public double ValorAjustesDebito { get; set; }
        public double ValorEstornosCreditos { get; set; }
        public double ValorCreditoImpostos { get; set; }
        public DateTime DataFinal { get; set; }
        public double ValorAjustesCreditoDocFiscal { get; set; }
        public double ValorAjustesCredito { get; set; }
        public double ValorEstornosDebitos { get; set; }
        public double SaldoCredorAnterior { get; set; }
        public double ValorSaldoDevedor { get; set; }
        public double ValorDeducoes { get; set; }
        public double ValorIcmsRecolher { get; set; }
        public double ValorSaldoCredorIcms { get; set; }
        public double ExtraApuracao { get; set; }
    }
    public class E111
    {
        public string CodAjuste { get; set; }
        public string DescComplementar { get; set; }
        public double ValorAjuste { get; set; }
    }
    public class E113
    {
        public string CodParticipante { get; set; }
        public string CodModeloDocumento { get; set; }
        public string Serie { get; set; }
        public string SubSerie { get; set; }
        public int NumeroDocumento { get; set; }
        public DateTime DataEmissao { get; set; }
        public string CodItem { get; set; }
        public double ValorAjusteItem { get; set; }
        public long Chave { get; set; }
    }
    public class E115
    {
        public string CodInformacao { get; set; }
        public double ValorInformacao { get; set; }
        public string DescComplementar { get; set; }
    }
    public  class E116
    {
        public string CodIcms { get; set; }
        public double ValorIcms { get; set; }
        public string DataVencimentoIcms { get; set; }
        public string CodReceita { get; set; }
        public string NumeroProcesso { get; set; }
        public int OrigemProcesso { get; set; }
        public string DescProcesso { get; set; }
        public string DescComplementar { get; set; }
        public string MesReferencia { get; set; }
    }
}
