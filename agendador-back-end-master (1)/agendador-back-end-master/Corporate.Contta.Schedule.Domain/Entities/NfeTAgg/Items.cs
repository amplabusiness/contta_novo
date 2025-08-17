using Corporate.Contta.Schedule.Domain.Entities.Base;

namespace Corporate.Contta.Schedule.Domain.Entities.NfeTAgg
{
    public class Items : Entity
    {
        public bool ProdutoBaySimples { get; set; }
        public string ItemCode { get; set; }

        public string AdditionalDescription { get; set; }

        public float ItemQuantity { get; set; }

        public string MeasureCode { get; set; }

        public double TotalItemValue { get; set; }

        public double DiscountValue { get; set; }

        public string CstIcms { get; set; }

        public double Cfop { get; set; }

        public string OperationNatureCode { get; set; }

        public double IcmsCalculationBasis { get; set; }

        public double IcmsAliquot { get; set; }

        public double IcmsValue { get; set; }

        public double IcmsStCalculationBasis { get; set; }

        public string IcmsStAliquot { get; set; }

        public double IcmsStValue { get; set; }

        public string CstIpi { get; set; }

        public double IpiAliquot { get; set; }

        public double IpiValue { get; set; }

        public string CstPis { get; set; }

        public string CofinsValue { get; set; }
    }
}
