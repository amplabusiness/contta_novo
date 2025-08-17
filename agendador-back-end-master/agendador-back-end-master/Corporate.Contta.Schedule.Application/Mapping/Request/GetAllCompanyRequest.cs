using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class GetAllCompanyRequest:IRequest<Response.Response>
    {
        public string UserId { get; set; }
    }
}
