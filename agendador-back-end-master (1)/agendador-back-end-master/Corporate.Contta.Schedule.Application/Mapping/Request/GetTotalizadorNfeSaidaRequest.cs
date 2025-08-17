using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class GetTotalizadorNfeSaidaRequest: IRequest<Response.Response>
    {
        public Guid EmpresaId { get; set; }
        public DateTime Data { get; set; }
    }
}
