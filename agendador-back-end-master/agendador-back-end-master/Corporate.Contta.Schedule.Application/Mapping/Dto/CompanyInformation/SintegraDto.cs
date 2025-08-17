using System;
using System.Collections.Generic;

namespace Corporate.Contta.Schedule.Domain.Entities
{
    public class SintegraDto
    {
        public DateTime? LastUpdate { get; set; }

        public List<Registrations> Registrations { get; set; }
    }
}
