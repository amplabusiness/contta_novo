using Corporate.Contta.Schedule.Domain.Entities.ImpostoAgg;
using MediatR;
using System.Collections.Generic;

namespace Corporate.Contta.Schedule.Application.Mapping.Request.Impostos
{
    public class NewImpostoAntecipacaoRequest : IRequest<Response.Response>
    {
        public List<ImpostoAntecipacao> ListImpostoAntecipacao { get; set; }
    }
}
