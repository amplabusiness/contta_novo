using MediatR;
using System;

namespace Corporate.Contta.Schedule.Application.Mapping
{
    public class GetCriticasNovasRequest : IRequest<Response.Response>
    {
        public Guid CompanyId { get; set; }

        public bool CriticasNovas { get; set; }
        public bool CriticasAntigas { get; set; }

    }
}
