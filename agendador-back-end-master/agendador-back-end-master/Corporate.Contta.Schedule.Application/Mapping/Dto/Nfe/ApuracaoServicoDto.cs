using Corporate.Contta.Schedule.Domain.Entities.NfeAgg;
using System;
using System.Collections.Generic;

namespace Corporate.Contta.Schedule.Application.Mapping.Dto.Nfe
{
    public class ApuracaoServicoDto
    {
        public ApuracaoPrestadorSaida ApuracaoSaida { get; set; }
        public ApuracaoTomadorEntrada ApuracaoEntrada { get; set; }

    }
}
