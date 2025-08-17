using MediatR;
using System;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class UpdateConfigurationFhRequest : NewConfigurationFhRequest, IRequest<Response.Response>
    {       

    }
}
