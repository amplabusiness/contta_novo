using System;
using System.Collections.Generic;

namespace ConttaComsumidor.Models.CompanyInformationAgg
{
    public class Sintegra
    {
            public DateTime? LastUpdate { get; set; }

            public List<Registrations> Registrations { get; set; }
    }
}
