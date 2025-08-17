using MediatR;
using System;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class HomeRequest : IRequest<Response.Response>
    {
        public Guid UserId { get; set; }

        public string Token { get; set; }

        public DateTime dhEmi { get; set; }

        public string EmpresaId { get; set; }

    }
}
