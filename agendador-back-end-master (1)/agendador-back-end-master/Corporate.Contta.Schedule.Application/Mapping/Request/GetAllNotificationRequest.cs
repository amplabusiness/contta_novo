using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Application.Mapping
{
    public class GetAllNotificationRequest : IRequest<Response.Response>
    {
        public Guid EmpresaId { get; set; }

        public string Token { get; set; }

    }
}
