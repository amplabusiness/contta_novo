using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public  class FilterNfeRequest : IRequest<Response.Response>
    {
        public Guid CompanyId { get; set; }
        public string TipoNfe { get; set; }
        public string DescProduto { get; set; }
        public string Cnpj { get; set; }
        public DateTime? DhEmiss { get; set; }
        public string Uf { get; set; }
        public string NomeCli { get; set; }
    }
}
