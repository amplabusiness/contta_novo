using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class DetalhamentoApuracaoIcmsPisNfeRequest : IRequest<Response.Response>
    {
        public List<Guid> Ids { get; set; }
        public string Tipo { get; set; }
        public string Grupo { get; set; }
    }
}
