using Corporate.Contta.Schedule.Domain.Entities.ImpostoAgg;
using MediatR;
using System.Collections.Generic;

namespace Corporate.Contta.Schedule.Application.Mapping.Request.Impostos
{
    public class NewImpostoImuneRequest : IRequest<Response.Response>
    {
        public List<ImpostoImune> ListImpostoImune { get; set; }
    }
}
