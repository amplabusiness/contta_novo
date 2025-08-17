using Corporate.Contta.Schedule.Domain.Entities.Base;
using System;

namespace Corporate.Contta.Schedule.Domain.Entities.Configuration
{
    public class ConfigurationUser : Entity
    {
        public bool DashboardTutorial { get; set; }
        public bool SubstituicaoTutorial { get; set; }
        public bool PisConfinsTutorial { get; set; }
        public bool ClickedDownLoadButton { get; set; }
        public bool ClickedChangeCompanyButton { get; set; }
        public bool IcmSInsento { get; set; }
        public bool IcmSImune { get; set; }
        public bool PISCofinsIsento { get; set; }
        public bool PISCofinsImune { get; set; }
        public bool ProductTb { get; set; }
        public Guid? UserId { get; set; }        
    }
}
