using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Corporate.Contta.Schedule.Domain.Enum;

namespace ConttaComsumidor.Models.NotaFiscalDeServico
{
	[XmlRoot(ElementName = "ValoresNfse", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
	public class ValoresNfse
	{
		[XmlElement(ElementName = "BaseCalculo", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public string BaseCalculo { get; set; }
		[XmlElement(ElementName = "Aliquota", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public string Aliquota { get; set; }
		[XmlElement(ElementName = "ValorIss", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public string ValorIss { get; set; }
	}

	[XmlRoot(ElementName = "IdentificacaoRps", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
	public class IdentificacaoRps
	{
		[XmlElement(ElementName = "Numero", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public string Numero { get; set; }
		[XmlElement(ElementName = "Serie", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public string Serie { get; set; }
		[XmlElement(ElementName = "Tipo", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public string Tipo { get; set; }
	}

	[XmlRoot(ElementName = "Rps", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
	public class Rps
	{
		[XmlElement(ElementName = "IdentificacaoRps", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public IdentificacaoRps IdentificacaoRps { get; set; }
		[XmlElement(ElementName = "DataEmissao", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public string DataEmissao { get; set; }
		[XmlElement(ElementName = "Status", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public string Status { get; set; }
	}

	[XmlRoot(ElementName = "Valores", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
	public class Valores
	{
		[XmlElement(ElementName = "ValorServicos", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public string ValorServicos { get; set; }
		[XmlElement(ElementName = "ValorDeducoes", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public string ValorDeducoes { get; set; }
		[XmlElement(ElementName = "ValorPis", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public string ValorPis { get; set; }
		[XmlElement(ElementName = "ValorCofins", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public string ValorCofins { get; set; }
		[XmlElement(ElementName = "ValorInss", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public string ValorInss { get; set; }
		[XmlElement(ElementName = "ValorIr", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public string ValorIr { get; set; }
		[XmlElement(ElementName = "ValorCsll", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public string ValorCsll { get; set; }
		[XmlElement(ElementName = "ValorIss", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public string ValorIss { get; set; }
		[XmlElement(ElementName = "Aliquota", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public string Aliquota { get; set; }
		[XmlElement(ElementName = "DescontoIncondicionado", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public string DescontoIncondicionado { get; set; }
	}

	[XmlRoot(ElementName = "Servico", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
	public class Servico
	{
		[XmlElement(ElementName = "Valores", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public Valores Valores { get; set; }
		[XmlElement(ElementName = "IssRetido", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public string IssRetido { get; set; }
		[XmlElement(ElementName = "CodigoTributacaoMunicipio", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public string CodigoTributacaoMunicipio { get; set; }
		[XmlElement(ElementName = "Discriminacao", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public string Discriminacao { get; set; }
		[XmlElement(ElementName = "CodigoMunicipio", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public string CodigoMunicipio { get; set; }
		[XmlElement(ElementName = "ExigibilidadeISS", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public string ExigibilidadeISS { get; set; }
		[XmlElement(ElementName = "MunicipioIncidencia", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public string MunicipioIncidencia { get; set; }
	}

	[XmlRoot(ElementName = "CpfCnpj", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
	public class CpfCnpj
	{
		[XmlElement(ElementName = "Cnpj", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public string Cnpj { get; set; }
	}

	[XmlRoot(ElementName = "Prestador", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
	public class Prestador
	{
		[XmlElement(ElementName = "CpfCnpj", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public CpfCnpj CpfCnpj { get; set; }
		[XmlElement(ElementName = "InscricaoMunicipal", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public string InscricaoMunicipal { get; set; }
	}

	[XmlRoot(ElementName = "IdentificacaoTomador", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
	public class IdentificacaoTomador
	{
		[XmlElement(ElementName = "CpfCnpj", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public CpfCnpj CpfCnpj { get; set; }
	}

	[XmlRoot(ElementName = "Endereco", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
	public class Endereco
	{
		[XmlElement(ElementName = "Endereco", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public string Enderecos { get; set; }
		[XmlElement(ElementName = "Numero", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public string Numero { get; set; }
		[XmlElement(ElementName = "Complemento", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public string Complemento { get; set; }
		[XmlElement(ElementName = "Bairro", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public string Bairro { get; set; }
		[XmlElement(ElementName = "CodigoMunicipio", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public string CodigoMunicipio { get; set; }
		[XmlElement(ElementName = "Uf", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public string Uf { get; set; }
		[XmlElement(ElementName = "Cep", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public string Cep { get; set; }
	}

	[XmlRoot(ElementName = "Tomador", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
	public class Tomador
	{
		[XmlElement(ElementName = "IdentificacaoTomador", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public IdentificacaoTomador IdentificacaoTomador { get; set; }
		[XmlElement(ElementName = "RazaoSocial", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public string RazaoSocial { get; set; }
		[XmlElement(ElementName = "Endereco", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public Endereco Endereco { get; set; }
	}

	[XmlRoot(ElementName = "InfDeclaracaoPrestacaoServico", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
	public class InfDeclaracaoPrestacaoServico
	{
		[XmlElement(ElementName = "Rps", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public Rps Rps { get; set; }
		[XmlElement(ElementName = "Competencia", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public string Competencia { get; set; }
		[XmlElement(ElementName = "Servico", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public Servico Servico { get; set; }
		[XmlElement(ElementName = "Prestador", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public Prestador Prestador { get; set; }
		[XmlElement(ElementName = "Tomador", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public Tomador Tomador { get; set; }
		[XmlElement(ElementName = "OptanteSimplesNacional", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public string OptanteSimplesNacional { get; set; }
	}

	[XmlRoot(ElementName = "DeclaracaoPrestacaoServico", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
	public class DeclaracaoPrestacaoServico
	{
		[XmlElement(ElementName = "InfDeclaracaoPrestacaoServico", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public InfDeclaracaoPrestacaoServico InfDeclaracaoPrestacaoServico { get; set; }
	}

	[XmlRoot(ElementName = "InfNfse", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
	public class InfNfse
	{
		[XmlElement(ElementName = "Numero", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public string Numero { get; set; }
		[XmlElement(ElementName = "CodigoVerificacao", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public string CodigoVerificacao { get; set; }
		[XmlElement(ElementName = "DataEmissao", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public string DataEmissao { get; set; }
		[XmlElement(ElementName = "ValoresNfse", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public ValoresNfse ValoresNfse { get; set; }
		[XmlElement(ElementName = "DeclaracaoPrestacaoServico", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public DeclaracaoPrestacaoServico DeclaracaoPrestacaoServico { get; set; }
	}

	[XmlRoot(ElementName = "Nfse", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
	public class Nfse
	{
		[XmlElement(ElementName = "InfNfse", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public InfNfse InfNfse { get; set; }
		[XmlAttribute(AttributeName = "versao")]
		public string Versao { get; set; }
        public ETipoNota ETipoNota { get; set; }
    }

	[XmlRoot(ElementName = "CompNfse", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
	public class CompNfse
	{
		[XmlElement(ElementName = "Nfse", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public Nfse Nfse { get; set; }
	}

	[XmlRoot(ElementName = "ListaNfse", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
	public class ListaNfse
	{
		[XmlElement(ElementName = "CompNfse", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public CompNfse CompNfse { get; set; }
	}

	[XmlRoot(ElementName = "MensagemRetorno", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
	public class MensagemRetorno
	{
		[XmlElement(ElementName = "Codigo", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public string Codigo { get; set; }
		[XmlElement(ElementName = "Mensagem", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public string Mensagem { get; set; }
	}

	[XmlRoot(ElementName = "ListaMensagemRetorno", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
	public class ListaMensagemRetorno
	{
		[XmlElement(ElementName = "MensagemRetorno", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public MensagemRetorno MensagemRetorno { get; set; }
	}

	[XmlRoot(ElementName = "GerarNfseResposta", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
	public class GerarNfseResposta
	{
		[XmlElement(ElementName = "ListaNfse", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public ListaNfse ListaNfse { get; set; }
		[XmlElement(ElementName = "ListaMensagemRetorno", Namespace = "http://nfse.goiania.go.gov.br/xsd/nfse_gyn_v02.xsd")]
		public ListaMensagemRetorno ListaMensagemRetorno { get; set; }
		[XmlAttribute(AttributeName = "xmlns")]
		public string Xmlns { get; set; }
		public string FileInfo { get; set; }
		public string FileName { get; set; }

		public ETipoNota ETipoNota { get; set; }

		public string ModeloNota { get; set; }
		public string CnpjEmitente { get; set; }
	}
}
