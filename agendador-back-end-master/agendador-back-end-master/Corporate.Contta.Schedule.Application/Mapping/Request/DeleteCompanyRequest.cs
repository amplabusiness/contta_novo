using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class DeleteCompanyRequest:IRequest<Response.Response>
    {
        public Guid? Id { get; set; }
        public Guid? UserId { get; set; }
    }
}
