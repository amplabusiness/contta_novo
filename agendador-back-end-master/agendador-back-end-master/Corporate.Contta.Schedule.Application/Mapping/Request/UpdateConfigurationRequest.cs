using MediatR;
using System;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class UpdateConfigurationRequest : NewConfigurationUserRequest , IRequest<Response.Response>
    {
     
    }
}
