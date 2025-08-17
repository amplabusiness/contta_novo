using Corporate.Contta.Schedule.Domain.Entities.ImpostoAgg;
using MediatR;
using System.Collections.Generic;

namespace Corporate.Contta.Schedule.Application.Mapping.Request.Impostos
{
    public class NewImpostoRedCestabasicaRequest : IRequest<Response.Response>
    {
        public List<ImpostoRedCestaBasica> ListImpostoRedCestBasica { get; set; }
    }
}
