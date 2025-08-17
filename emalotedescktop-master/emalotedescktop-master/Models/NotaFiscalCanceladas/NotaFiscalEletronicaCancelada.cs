using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace EmaloteContta.Models
{
    [XmlRoot(ElementName = "detEvento", Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public class DetEvento
    {
        [XmlElement(ElementName = "versao", Namespace = "http://www.portalfiscal.inf.br/nfe")]
        public string Versao { get; set; }

        [XmlElement(ElementName = "descEvento", Namespace = "http://www.portalfiscal.inf.br/nfe")]
        public string DescEvento { get; set; }

        [XmlElement(ElementName = "nProt", Namespace = "http://www.portalfiscal.inf.br/nfe")]
        public string NProt { get; set; }

        [XmlElement(ElementName = "xJust", Namespace = "http://www.portalfiscal.inf.br/nfe")]
        public string XJust { get; set; }
    }

    [XmlRoot(ElementName = "infEvento", Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public class InfEvento
    {
        [XmlElement(ElementName = "Id", Namespace = "http://www.portalfiscal.inf.br/nfe")]
        public string Id { get; set; }

        [XmlElement(ElementName = "cOrgao", Namespace = "http://www.portalfiscal.inf.br/nfe")]
        public string COrgao { get; set; }

        [XmlElement(ElementName = "tpAmb", Namespace = "http://www.portalfiscal.inf.br/nfe")]
        public string TpAmb { get; set; }

        [XmlElement(ElementName = "CNPJ", Namespace = "http://www.portalfiscal.inf.br/nfe")]
        public string CNPJ { get; set; }

        [XmlElement(ElementName = "chNFe", Namespace = "http://www.portalfiscal.inf.br/nfe")]
        public string ChNFe { get; set; }

        [XmlElement(ElementName = "dhEvento", Namespace = "http://www.portalfiscal.inf.br/nfe")]
        public string DhEvento { get; set; }

        [XmlElement(ElementName = "tpEvento", Namespace = "http://www.portalfiscal.inf.br/nfe")]
        public string TpEvento { get; set; }

        [XmlElement(ElementName = "nSeqEvento", Namespace = "http://www.portalfiscal.inf.br/nfe")]
        public string NSeqEvento { get; set; }

        [XmlElement(ElementName = "verEvento", Namespace = "http://www.portalfiscal.inf.br/nfe")]
        public string VerEvento { get; set; }

        [XmlElement(ElementName = "detEvento", Namespace = "http://www.portalfiscal.inf.br/nfe")]
        public DetEvento DetEvento { get; set; }

        [XmlElement(ElementName = "verAplic", Namespace = "http://www.portalfiscal.inf.br/nfe")]
        public string VerAplic { get; set; }

        [XmlElement(ElementName = "cStat", Namespace = "http://www.portalfiscal.inf.br/nfe")]
        public string CStat { get; set; }

        [XmlElement(ElementName = "xMotivo", Namespace = "http://www.portalfiscal.inf.br/nfe")]
        public string XMotivo { get; set; }

        [XmlElement(ElementName = "xEvento", Namespace = "http://www.portalfiscal.inf.br/nfe")]
        public string XEvento { get; set; }

        [XmlElement(ElementName = "dhRegEvento", Namespace = "http://www.portalfiscal.inf.br/nfe")]
        public DateTime DhRegEvento { get; set; }

        [XmlElement(ElementName = "nProt", Namespace = "http://www.portalfiscal.inf.br/nfe")]
        public string NProt { get; set; }
    }

    [XmlRoot(ElementName = "canonicalizationMethod", Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public class CanonicalizationMethod
    {
        [XmlElement(ElementName = "Algorithm", Namespace = "http://www.portalfiscal.inf.br/nfe")]
        public string Algorithm { get; set; }
    }

    [XmlRoot(ElementName = "signatureMethod", Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public class SignatureMethod
    {
        [XmlElement(ElementName = "Algorithm", Namespace = "http://www.portalfiscal.inf.br/nfe")]
        public string Algorithm { get; set; }
    }

    [XmlRoot(ElementName = "transform", Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public class Transform
    {
        [XmlElement(ElementName = "Algorithm", Namespace = "http://www.portalfiscal.inf.br/nfe")]
        public string Algorithm { get; set; }
    }

    [XmlRoot(ElementName = "transform", Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public class Transforms
    {
        [XmlElement(ElementName = "Transform", Namespace = "http://www.portalfiscal.inf.br/nfe")]
        public List<Transform> Transform { get; set; }
    }

    [XmlRoot(ElementName = "DigestMethod", Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public class DigestMethod
    {
        [XmlElement(ElementName = "Algorithm", Namespace = "http://www.portalfiscal.inf.br/nfe")]
        public string Algorithm { get; set; }
    }

    [XmlRoot(ElementName = "Reference", Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public class Reference
    {
        [XmlElement(ElementName = "URI", Namespace = "http://www.portalfiscal.inf.br/nfe")]
        public string URI { get; set; }

        [XmlElement(ElementName = "Transforms", Namespace = "http://www.portalfiscal.inf.br/nfe")]
        public Transforms Transforms { get; set; }

        [XmlElement(ElementName = "DigestMethod", Namespace = "http://www.portalfiscal.inf.br/nfe")]
        public DigestMethod DigestMethod { get; set; }

        [XmlElement(ElementName = "DigestValue", Namespace = "http://www.portalfiscal.inf.br/nfe")]
        public string DigestValue { get; set; }
    }

    [XmlRoot(ElementName = "SignedInfo", Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public class SignedInfo
    {
        [XmlElement(ElementName = "CanonicalizationMethod", Namespace = "http://www.portalfiscal.inf.br/nfe")]
        public CanonicalizationMethod CanonicalizationMethod { get; set; }

        [XmlElement(ElementName = "SignatureMethod", Namespace = "http://www.portalfiscal.inf.br/nfe")]
        public SignatureMethod SignatureMethod { get; set; }

        [XmlElement(ElementName = "Reference", Namespace = "http://www.portalfiscal.inf.br/nfe")]
        public Reference Reference { get; set; }
    }

    [XmlRoot(ElementName = "X509Data", Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public class X509Data
    {
        [XmlElement(ElementName = "X509Certificate", Namespace = "http://www.portalfiscal.inf.br/nfe")]
        public string X509Certificate { get; set; }
    }

    [XmlRoot(ElementName = "KeyInfo", Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public class KeyInfo
    {
        [XmlElement(ElementName = "X509Data", Namespace = "http://www.portalfiscal.inf.br/nfe")]
        public X509Data X509Data { get; set; }
    }

    [XmlRoot(ElementName = "Signature", Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public class Signature
    {
        [XmlElement(ElementName = "SignedInfo", Namespace = "http://www.portalfiscal.inf.br/nfe")]
        public SignedInfo SignedInfo { get; set; }

        [XmlElement(ElementName = "SignatureValue", Namespace = "http://www.portalfiscal.inf.br/nfe")]
        public string SignatureValue { get; set; }

        [XmlElement(ElementName = "KeyInfo", Namespace = "http://www.portalfiscal.inf.br/nfe")]
        public KeyInfo KeyInfo { get; set; }
    }

    [XmlRoot(ElementName = "Evento", Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public class Evento
    {
        [XmlElement(ElementName = "versao", Namespace = "http://www.portalfiscal.inf.br/nfe")]
        public string Versao { get; set; }

        [XmlElement(ElementName = "infEvento", Namespace = "http://www.portalfiscal.inf.br/nfe")]
        public InfEvento InfEvento { get; set; }

        [XmlElement(ElementName = "Signature", Namespace = "http://www.portalfiscal.inf.br/nfe")]
        public Signature Signature { get; set; }
    }

    [XmlRoot(ElementName = "retEvento", Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public class RetEvento
    {
        [XmlElement(ElementName = "versao", Namespace = "http://www.portalfiscal.inf.br/nfe")]
        public string Versao { get; set; }

        [XmlElement(ElementName = "infEvento", Namespace = "http://www.portalfiscal.inf.br/nfe")]
        public InfEvento InfEvento { get; set; }
    }

    [XmlRoot(ElementName = "procEventoNFe", Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public class ProcEventoNFe
    {
        [XmlElement(ElementName = "versao", Namespace = "http://www.portalfiscal.inf.br/nfe")]
        public string Versao { get; set; }

        [XmlElement(ElementName = "evento", Namespace = "http://www.portalfiscal.inf.br/nfe")]
        public Evento Evento { get; set; }

        [XmlElement(ElementName = "retEvento", Namespace = "http://www.portalfiscal.inf.br/nfe")]
        public RetEvento RetEvento { get; set; }

        public string FileInfo { get; set; }
        public string FileName { get; set; }

        public ETipoNota ETipoNota { get; set; }
    }
}
