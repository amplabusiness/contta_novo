using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class AgBlocoEGetRequest: IRequest<Response.Response>
    {
        public Guid CompanyInformationId { get; set; }
        public DateTime DateInition { get; set; }
    }
}
