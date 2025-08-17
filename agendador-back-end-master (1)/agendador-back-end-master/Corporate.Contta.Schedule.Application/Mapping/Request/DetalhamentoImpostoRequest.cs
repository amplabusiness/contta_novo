using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class DetalhamentoImpostoRequest : IRequest<Response.Response>
    {
        public Guid CompanyId { get; set; }
        public DateTime DhEmiss { get; set; }
    }
}
