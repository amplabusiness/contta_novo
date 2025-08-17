using MediatR;
using System;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class ApuracaoRequest : IRequest<Response.Response>
    {
        public string Token { get; set; }

        public DateTime DataEmissao { get; set; }

        public Guid CompanyId { get; set; }

        public bool Mod57 { get; set; }
        public bool Mod55 { get; set; }
        public bool Servico { get; set; }
        public bool Service { get; set; }

    }
}
