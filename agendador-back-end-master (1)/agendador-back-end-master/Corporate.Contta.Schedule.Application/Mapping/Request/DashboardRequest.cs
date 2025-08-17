using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class DashboardRequest: IRequest<Response.Response>
    {
        public Guid Id { get; set; }

        public DateTime RequestDate { get; set; }

        public Guid? EmpresaId { get; set; }
    }
}
