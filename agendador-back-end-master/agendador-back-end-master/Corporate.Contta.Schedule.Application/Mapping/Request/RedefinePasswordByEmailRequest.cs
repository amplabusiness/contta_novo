using MediatR;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class RedefinePasswordByEmailRequest : IRequest<Response.Response>
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }

    }
}
