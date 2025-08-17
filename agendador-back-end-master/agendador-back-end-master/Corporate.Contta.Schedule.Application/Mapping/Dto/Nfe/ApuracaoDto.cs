using Corporate.Contta.Schedule.Domain.Entities.NfeAgg;
using System;
using System.Collections.Generic;

namespace Corporate.Contta.Schedule.Application.Mapping.Dto.Nfe
{
    public class ApuracaoDto
    {
        public List<ApuracaoSaida> ApuracaoSaida { get; set; }
        public List<ApuracaoEntrada> ApuracaoEntrada { get; set; }

      

    }
}
