using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class UpdateExigibilidadeRequest : IRequest<Response.Response>
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
    }
}
