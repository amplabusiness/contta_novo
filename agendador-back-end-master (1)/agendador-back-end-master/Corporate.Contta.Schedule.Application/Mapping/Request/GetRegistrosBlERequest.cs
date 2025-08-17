using MediatR;
using System;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class GetRegistrosBlERequest : IRequest<Response.Response>
    {
        public Guid EmpresaId { get; set; }

        public DateTime Data { get; set; }
    }
}
