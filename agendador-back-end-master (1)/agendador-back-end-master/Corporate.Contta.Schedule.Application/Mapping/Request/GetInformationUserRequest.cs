using Corporate.Contta.Schedule.Application.Mapping.Result.GetUser;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class GetInformationUserRequest : IRequest<Response.Response>
    {
        public  Guid Id { get; set; }
        public string Token { get; set; }
        public bool UserComum { get; set; }

    }
}
