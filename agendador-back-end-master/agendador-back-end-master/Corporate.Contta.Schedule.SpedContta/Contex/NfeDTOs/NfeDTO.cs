using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.SpedContta.Contex.NfeDTOs
{
    public class NfeDTO
    {
        [BsonElement("Produtos")]
        public List<ProdutosDTO> Produtos { get; set; }
        [BsonElement("Impostos")]
        public List<ImportosDTO> Impostos { get; set; }

        public Guid Id { get; set; }
        public int Nnfe { get; set; }
        public int Serie { get; set; }
        public string Modelo { get; set; }
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
        public double VlAproxTributos { get; set; }
        public bool Integrada { get; set; }
        public Guid EmpresaEmetId { get; set; }
        public Guid EmpresaDesId { get; set; }
        public int TipoEmicao { get; set; }
        public string CodParticipante { get; set; }
        public string CSOSN { get; set; }
        public int CodSituacao { get; set; }
        public int TipoPagamento { get; set; }
        public decimal ValorDesconto { get; set; }
        public decimal ValorAbatSuf { get; set; }
        public int TipoFrete { get; set; }
        public decimal ValorTotalCofinsSt { get; set; }
        public decimal ValorTotalPisSt { get; set; }
        public string CodInfoComplementar { get; set; }
        public string DescriComplementar { get; set; }
        public bool Ativo { get; set; }

        public bool GeradoTbSimples { get; set; }
    }
}
