using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class GetCodProductRequest : IRequest<Response.Response>
    {
        public string CodProd { get; set; }
        public string DescProd { get; set; }

        public Guid Document { get; set; }
    }
}
