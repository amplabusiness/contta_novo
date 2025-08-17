using Corporate.Contta.Schedule.Domain.Entities.ImpostoAgg;
using MediatR;

namespace Corporate.Contta.Schedule.Application.Mapping.Request.Impostos
{
    public class NewImpostoExigibilidadeRequest : ImpostoExigibilidadeSus,  IRequest<Response.Response>
    {
     
    }
}
