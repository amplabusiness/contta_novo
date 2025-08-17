using MediatR;
using System;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class NewCriticasNovasRequest : IRequest<Response.Response>
    {
        public int St { get; set; }
        public int Ncm { get; set; }
        public int Cfop { get; set; }
        public int Cnae { get; set; }
        public int Estoque { get; set; }
        public DateTime DataCadastro { get; set; }
        public Guid CompanyInformation { get; set; }
    }
}
