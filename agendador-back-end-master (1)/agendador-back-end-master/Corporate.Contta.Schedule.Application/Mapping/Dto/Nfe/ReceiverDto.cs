using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Application.Mapping.Dto.Nfe
{
    public class ReceiverDto
    {
        //public Guid? Id { get; set; }
        public string Address { get; set; }

        public string Cep { get; set; }

        public string City { get; set; }

        public string CnpjCpf { get; set; }
    

        public DateTime? EmissionDate { get; set; }

        public string InOutDate { get; set; }   

        public string Name { get; set; }

        public string Neighborhood { get; set; }

        public string PhoneFax { get; set; }

        public string State { get; set; }

        public string StateSubscription { get; set; }
    }
}
