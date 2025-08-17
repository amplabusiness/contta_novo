using MediatR;
using System;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class GetAllByProductNcmRequest : IRequest<Response.Response>
    {
        public Guid Id { get; set; }

        public bool Monofasico { get; set; }

    }
}
