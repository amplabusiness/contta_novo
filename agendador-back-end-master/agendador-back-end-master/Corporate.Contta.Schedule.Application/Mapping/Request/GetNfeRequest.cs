using Corporate.Contta.Schedule.Application.Mapping.Response.GetInformationByMasterId;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class GetCompanySummaryInformationRequest:IRequest<Response.Response>
    {
        public Guid? Id { get; set; }
    }
}
