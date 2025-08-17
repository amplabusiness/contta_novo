using System;
using System.Collections.Generic;

namespace Corporate.Contta.Schedule.Domain.Entities
{
    public class Sintegra
    {
        public DateTime? LastUpdate { get; set; }
        public string home_state_registration { get; set; }

        public List<Registrations> Registrations { get; set; }
    }
}
