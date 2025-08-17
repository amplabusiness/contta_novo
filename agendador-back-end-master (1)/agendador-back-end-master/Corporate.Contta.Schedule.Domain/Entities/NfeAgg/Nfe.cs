using Corporate.Contta.Schedule.Domain.Entities.Base;
using Corporate.Contta.Schedule.Domain.Enum;
using System;
using System.Collections.Generic;

namespace Corporate.Contta.Schedule.Domain.Entities.NfeAgg
{
    public class NFE : Entity
    {
        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public int Nnfe { get; set; }
        public int Serie { get; set; }
        public string Modelo { get; set; }
        public string CMunEnv { get; set; }
        public string XMunEnv { get; set; }
        public string UFEnv { get; set; }
        public string UFInicial { get; set; }
        public string UFFinal { get; set; }
        public string CMunIni { get; set; }
        public string XMunFim { get; set; }
        public string UFIni { get; set; }
        public string CMunFim { get; set; }
        public string CodBarra { get; set; }
        public DateTime? DhEmi { get; set; }
        public DateTime? DhSaida { get; set; }
        public int TipNfe { get; set; }
        public string FormPag { get; set; }
        public string TipAten { get; set; }
        public string NatOperacao { get; set; }
        public bool? NfConFInal { get; set; }
        public string DesOperacao { get; set; }
        public double VlTotalPro { get; set; }
        public double VPrest { get; set; }
        public double VlTotalFrete { get; set; }
        public double VlTotalSeguro { get; set; }
        public double VlTotalDesc { get; set; }
        public double VlOutDes { get; set; }
        public float BaseCAlIcms { get; set; }
        public double VtIcms { get; set; }
        public float BaseCalIcmsSt { get; set; }
        public double VtIcmsSt { get; set; }
        public double VlIpi { get; set; }
        public double VlPis { get; set; }
        public double VlCofins { get; set; }
        public double VtTotalNfe { get; set; }
        public double TotalFrete { get; set; }
        public double TotalSeguro { get; set; }
        public double TotalDesconto { get; set; }
        public double TotalProdutos { get; set; }
        public double VlAproxTributos { get; set; }
        public bool Integrada { get; set; }
        public Guid EmpresaEmetId { get; set; }
        public Guid? EmpresaDesId { get; set; }
        public bool Ativo { get; set; }

        public string NFRef { get; set; }

        public Guid? CompanyInformation { get; set; }

        public bool IntegradaEmpresa { get; set; }

        public ETipoNota ETipoNota { get; set; }

        public int FinNFe { get; set; }

        public string ModeloTipo { get; set; }
        public string Status { get; set; }

        public string TPag { get; set; }
        public string VPag { get; set; }

        public bool TranMercadoria { get; set; }

        public bool CompraFornecedor { get; set; }
        public string CNPJEmitente { get; set; }

        public bool GeradoTbSimples { get; set; }

        public string Danfe { get; set; }

        public bool Carta { get; set; }
        public string DescricaoCarta { get; set; }

        public bool Difal { get; set; }
        public string CnpjFornec { get; set; }
    }
}

