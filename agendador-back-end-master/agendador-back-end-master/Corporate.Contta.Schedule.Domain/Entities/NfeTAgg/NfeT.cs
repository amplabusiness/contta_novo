using Corporate.Contta.Schedule.Domain.Entities.Base;
using System;
using System.Collections.Generic;

namespace Corporate.Contta.Schedule.Domain.Entities.NfeTAgg
{
    public class NfeT : Entity
    {
        public double Cfop { get; set; }

        public Guid Issuer { get; set; }

        public int DocumentSituationCode { get; set; }

        public int Series { get; set; }

        public int DocumentNumber { get; set; }

        public string NfeKey { get; set; }

        public DateTime? EmissionDate { get; set; }

        public DateTime? InOrOutDate { get; set; }

        public double TotalDocumentValue { get; set; }

        public string PaymentType { get; set; }

        public double DiscountValue { get; set; }

        public double InsuranceValue { get; set; }

        public double OtherExpendituresValue { get; set; }

        public double IcmsCalculationBasis { get; set; }

        public double IcmsValue { get; set; }

        public double IcmsStCalculationBasis { get; set; }

        public double RetainedIcms { get; set; }

        public double IpiValue { get; set; }

        public string CofinsValue { get; set; }

        public string RetainedPis { get; set; }

        public string RetainedCofins { get; set; }

        public string Cnpj { get; set; }
        public Guid? IdNfe { get; set; }
        public string UrlDanfe  { get; set; }

        public IList<Items> Items { get; set; }

        public Analytical Analytical { get; set; }

        public bool Carta { get; set; }
        public string DescricaoCarta { get; set; }
    }
}
