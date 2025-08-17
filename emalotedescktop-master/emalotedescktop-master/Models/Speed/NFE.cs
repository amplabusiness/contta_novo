using System;
using System.Collections.Generic;
using System.Text;
using EmaloteContta.Models.NotaFiscalConsumidorFinalMod65;
using EmaloteContta.Models.Speed.Base;

namespace EmaloteContta.Models.Speed
{
    public class NFE: Entity
    {
        public NFE()
        {
            Produtos = new List<Produtos>();
            Impostos = new List<Impostos>();
            Ativo = true;
        }
        public List<Produtos> Produtos { get; set; }
        public List<Impostos> Impostos { get; set; }
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
        public double VtIcms  { get; set; }
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
        public bool Ativo { get; set; }

    }
}
