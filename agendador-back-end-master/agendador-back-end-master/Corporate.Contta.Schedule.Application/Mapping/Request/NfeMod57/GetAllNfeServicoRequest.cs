using MediatR;
using System;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class GetAllNfeServicoRequest : IRequest<Response.Response>
    {
        public Guid Documento { get; set; }

        public string Operation { get; set; }

        public DateTime Data { get; set; }

        public int Pagina { get; set; }

        public int QtdPorPagina { get; set; }
    }
}
