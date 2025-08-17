using Corporate.Contta.Schedule.Application.Mapping.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class GetByIdProductRequest: IRequest<Response.Response>
    {
        public Guid? Id { get; set; }
        public Guid EmpresaId { get; set; }
        public Guid NfeId { get; set; }
        public string Operacao { get; set; }
        public string Modelo { get; set; }
    }
}
