using MediatR;
using System;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class GetnfeRequest:IRequest<Response.Response>
    {
        public Guid? Id { get; set; }
    }
}
