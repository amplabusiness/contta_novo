using Corporate.Contta.Schedule.Domain.Entities.UserAgg;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class ChangeLinkWithTheCompanyRequest:IRequest<Response.Response>
    {
        public Guid? Id { get; set; }
        public List<UserCompany> UserCompanies { get; set; }
    }
}
