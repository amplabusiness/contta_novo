using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class AgBlocoERequest: IRequest<Response.Response>
    {
        public Guid CompanyInformationId { get; set; }
        public E100Request E100 { get; set; }
        public E110Request E110 { get; set; }
        public List<E111Request> E111 { get; set; }
        public E113Request E113 { get; set; }
        public E115Request E115 { get; set; }
        public E116Request E116 { get; set; }
    }
    public class E100Request
    {
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
    }
    public class E110Request
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
    public class E111Request
    {
        public string CodAjuste { get; set; }
        public string DescComplementar { get; set; }
        public double ValorAjuste { get; set; }
    }
    public class E113Request
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
    public class E115Request
    {
        public string CodInformacao { get; set; }
        public double ValorInformacao { get; set; }
        public string DescComplementar { get; set; }
    }
    public class E116Request
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
