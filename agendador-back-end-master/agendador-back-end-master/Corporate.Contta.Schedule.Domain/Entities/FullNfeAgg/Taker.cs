using Corporate.Contta.Schedule.Domain.Entities.Base;
using System;

namespace Corporate.Contta.Schedule.Domain.Entities.FullNfeAgg
{
    public class Taker : Entity
    {
        public Taker()
        {
            this.Id = Guid.NewGuid();
        }
        public string Cnpj_Cpf { get; set; }

        public string Name { get; set; }

        public string CitySubscription { get; set; }

        public string ZipCode { get; set; }

        public string Address { get; set; }

        public string Neighborhood { get; set; }

        public string City { get; set; }

        public string State { get; set; }
    }
}
