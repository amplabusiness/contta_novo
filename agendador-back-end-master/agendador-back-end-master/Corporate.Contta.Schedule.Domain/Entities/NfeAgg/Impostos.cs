using Corporate.Contta.Schedule.Domain.Entities.Base;
using System;

namespace Corporate.Contta.Schedule.Domain.Entities.NfeAgg
{
    public class Impostos : Entity
    {      
        public double Icms { get; set; }
        public string SituTributaria { get; set; }
        public double SituTriPermCredito { get; set; }
        public float AliCredIcms { get; set; }
        public double VlCredIcms { get; set; }
        public double SitSemPrerCred { get; set; }
        public double SitInseIcmsRecBruta { get; set; }
        public float BaseClacuDes { get; set; }
        public string AliqDest { get; set; }
        public string AliqInter { get; set; }
        public string PerProvParth { get; set; }
        public double VlParthDes { get; set; }
        public double VlParthRes { get; set; }
        public double PerIcmsFcpRemet { get; set; }
        public double VlIcmsFcpDest { get; set; }
        public double SitPerCrediCmCobrIcmsSt { get; set; }
        public double SitPerCrediSmCobrIcmsSt { get; set; }
        public double TriIsenIcmsCmCobrIcmsSt { get; set; }

        public string ModBcIcms { get; set; }
        public float ModOCIcms { get; set; }
        public double BCIcms { get; set; }
        public double AliqIcms { get; set; }
        public double VlIcms { get; set; }
        public string Cest { get; set; }
        public float ModBcICmsSt { get; set; }
        public float PerMarVlAdIcms { get; set; }
        public double PerRedBcIcmsSt { get; set; }
        public double VlBcIcmsSt { get; set; }
        public float AliqIcmsSt { get; set; }
        public string SitTriImune { get; set; }
        public double VlIcmsSt { get; set; }
        public double SitTriNTribu { get; set; }
        public double SitTriIcmsCbAntSt { get; set; }
        public double SitTriOuts { get; set; }
        public Guid ProdutoId { get; set; }
        public Guid NfeId { get; set; }
        public Guid CompanyInformation { get; set; }
        public double Ipi { get; set; }
        public double AliquotaIpi { get; set; }
        public string Origem { get; set; }

        public string Csosn { get; set; }

        public string CSelo { get; set; }
        public string CEnq { get; set; }
        public string CST { get; set; }
        public string CstPis { get; set; }
        public string CstCofins { get; set; }
        public string CvBC { get; set; }
        public string PCOFINS { get; set; }
        public string VCOFINS { get; set; }

        public string VBC { get; set; }
        public string VPIS { get; set; }

    }
}
