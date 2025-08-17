using MediatR;
using System;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class GetAllNfeMod57Request : IRequest<Response.Response>
    {
        public Guid Documento { get; set; }

        public string Operation { get; set; }

        public DateTime Data { get; set; }
   
    }
}
