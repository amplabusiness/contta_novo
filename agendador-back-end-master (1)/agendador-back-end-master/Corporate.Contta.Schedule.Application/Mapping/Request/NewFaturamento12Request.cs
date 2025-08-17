using Corporate.Contta.Schedule.Domain.Entities.NfeAgg;
using MediatR;
using System;
using System.Collections.Generic;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class NewFaturamento12Request : IRequest<Response.Response>
    {
        public Guid CompanyInformation { get; set; }
        public bool FaturamentoFechado { get; set; }

        public List<Faturamento> Faturamentos { get; set; }

        public bool FechamentoEmp { get; set; }
    }
}
