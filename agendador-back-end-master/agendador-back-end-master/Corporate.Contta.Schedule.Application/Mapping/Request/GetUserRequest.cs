using MediatR;
using System;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class GetUserRequest : IRequest<Response.Response>
    {
        public Guid Id { get; set; }
    }
}
