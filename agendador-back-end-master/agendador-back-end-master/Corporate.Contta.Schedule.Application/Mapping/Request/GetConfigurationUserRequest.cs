using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Application.Mapping
{
    public class GetConfigurationUserRequest : IRequest<Response.Response>
    {
        public Guid UserId { get; set; }

        public string Token { get; set; }

    }
}
