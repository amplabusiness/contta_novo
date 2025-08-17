using Corporate.Contta.Schedule.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Domain.Entities.FullNfeAgg
{
    public class NfVendaManual : Entity
    {      
        public Guid CompanyInformation { get; set; }
        public Taxes Taxes { get; set; }
        public List<Products> Products { get; set; }
        public Receiver Receiver { get; set; }
    }
}