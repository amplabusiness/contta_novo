using System.Collections.Generic;

namespace Corporate.Contta.Schedule.Domain.Entities
{
    public class Membership
    {
        public string Name { get; set; }
        public string CpfSocio { get; set; }

        public Role Role { get; set; }
    }
}
