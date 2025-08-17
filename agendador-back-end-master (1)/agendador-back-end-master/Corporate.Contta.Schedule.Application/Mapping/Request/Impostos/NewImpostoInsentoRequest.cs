using Corporate.Contta.Schedule.Domain.Entities.ImpostoAgg;
using MediatR;
using System.Collections.Generic;

namespace Corporate.Contta.Schedule.Application.Mapping.Request.Impostos
{
    public class NewImpostoInsentoRequest : IRequest<Response.Response>
    {
        public List<ImpostoInsento> ListImpostoInsento { get; set; }
    }
}
