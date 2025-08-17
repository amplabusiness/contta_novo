using MediatR;
using System;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class GetBasCalculoRequest : IRequest<Response.Response>
    {
        public Guid Id { get; set; }

        public DateTime DateClose { get; set; }
    }
}
