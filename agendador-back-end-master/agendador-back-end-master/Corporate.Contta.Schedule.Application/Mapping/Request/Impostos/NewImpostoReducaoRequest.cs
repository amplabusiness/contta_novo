using Corporate.Contta.Schedule.Domain.Entities.ImpostoAgg;
using MediatR;
using System.Collections.Generic;

namespace Corporate.Contta.Schedule.Application.Mapping.Request.Impostos
{
    public class NewImpostoReducaoRequest : ImpostoReducao, IRequest<Response.Response>
    {
     
    }
}
