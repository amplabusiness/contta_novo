using MediatR;
using System;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class GetAllNfeRequest : IRequest<Response.Response>
    {
        public Guid Documento { get; set; }

        public int Operation { get; set; }        
    }
}
