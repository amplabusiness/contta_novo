using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class GetFileProductRequest: IRequest<Response.Response>
    {
        public Guid CompanyInformation { get; set; }
    }
}
