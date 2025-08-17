using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class GetByIdNfeRequest: IRequest<Response.Response>
    {
        public Guid NfeId { get; set; }
    }
}
