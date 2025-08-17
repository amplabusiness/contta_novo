using Corporate.Contta.Schedule.Domain.Entities.NfeAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Domain.Entities.FullNfeAgg
{
    public class RetornoNfeCancelada
    {
        public NFE NFe { get; set; }
        public NfeCanceldas NfeCancelada { get; set; }
    }
}
