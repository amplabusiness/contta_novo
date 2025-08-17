using MediatR;
using System;
using System.Collections.Generic;

using System.Text;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class GetAllByIdCompanyRequest: IRequest<Response.Response>
    {
        public Guid Id { get; set; }
     
    }
}
