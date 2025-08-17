using MediatR;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class RedefinePasswordRequest:IRequest<Response.Response>
    {
        public Guid? Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }

    }
}
