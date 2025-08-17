using Corporate.Contta.Schedule.Domain.Entities.ConfigurationFhAgg;
using Corporate.Contta.Schedule.Domain.Entities.NotificationAgg;
using MediatR;
using System;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class NewConfigurationFhRequest : IRequest<Response.Response>
    {       

        public Guid Id { get; set; }
        public Guid CompanyInformation { get; set; }
        public FechamentoSimples FechamentoSimples { get; set; }
        public FechamentoLivroEntrada FechamentoLivroEntrada { get; set; }
        public FechamentoLivroCaixa FechamentoLivroCaixa { get; set; }
        public FechamentoLivroSaida FechamentoLivroSaida { get; set; }

    }
}
