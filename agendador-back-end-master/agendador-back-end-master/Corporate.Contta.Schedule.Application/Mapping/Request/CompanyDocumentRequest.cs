using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class CompanyDocumentRequest: IRequest<Response.Response>
    {
        public string Document { get; set; }
    }
}
