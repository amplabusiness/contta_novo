using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Application.Mapping
{
    public class GetConfigurationFhUserRequest : IRequest<Response.Response>
    {
        public Guid? Id { get; set; }
    }
}
