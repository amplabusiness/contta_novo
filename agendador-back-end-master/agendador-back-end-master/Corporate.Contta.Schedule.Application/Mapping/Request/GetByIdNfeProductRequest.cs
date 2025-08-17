using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class GetByIdNfeProductRequest:IRequest<Response.Response>
    {
        public Guid NfeId { get; set; }
        public Guid Id { get; set; }
        public Guid EmpresaId { get; set; }       
        public string Operacao { get; set; }
    }
}
