using MediatR;
using System;
using System.Collections.Generic;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class GetAllNfeTRequest : IRequest<Response.Response>
    {
        public Guid Documento { get; set; }

        public string Operation { get; set; }

        public DateTime Data { get; set; }

        public int Pagina { get; set; }

        public int QtdPorPagina { get; set; }

        public bool Apuracao { get; set; }

        public List<Guid> ListNfe { get; set; }
    }
}
