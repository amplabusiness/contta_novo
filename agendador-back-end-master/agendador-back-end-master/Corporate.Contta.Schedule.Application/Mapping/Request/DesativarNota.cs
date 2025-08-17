using MediatR;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class DesativarNota: IRequest<Response.Response>
    {
        public Guid Id { get; set; }
    }
}
