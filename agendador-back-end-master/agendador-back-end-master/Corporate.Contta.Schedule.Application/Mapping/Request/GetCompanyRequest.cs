using MediatR;
using System;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class GetCompanyRequest:IRequest<Response.Response>
    {
        public Guid Id { get; set; }
        public string Document { get; set; }

        public string Token { get; set; }


    }
}
