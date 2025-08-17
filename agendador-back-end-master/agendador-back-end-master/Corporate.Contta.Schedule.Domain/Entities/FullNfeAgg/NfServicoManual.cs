using MongoDB.Bson;
using System;

namespace Corporate.Contta.Schedule.Domain.Entities.FullNfeAgg
{
    public class NfServicoManual
    {
        public Guid Id { get; set; }
        public Guid CompanyInformation { get; set; }
        public Guid? EmpresaDestId { get; set; }
        public string CodBarras { get; set; }
        public DateTime DataEmissão { get; set; }
        public Taker Taker { get; set; }
        public Activity Activity { get; set; }
        public FederalRetentions FederalRetentions { get; set; }
        public Demonstrative Demonstrative { get; set; }
        public TaxCalculation TaxCalculation { get; set; }
    }
}
