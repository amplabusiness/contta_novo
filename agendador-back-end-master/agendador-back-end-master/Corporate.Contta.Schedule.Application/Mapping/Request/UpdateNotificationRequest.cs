using MediatR;
using System;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class UpdateNotificationRequest : NewNotificationRequest, IRequest<Response.Response>
    {
     
    }
}
