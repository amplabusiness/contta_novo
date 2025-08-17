using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public  class FilterNfeCanceladaRequest : IRequest<Response.Response>
    {
        public Guid CompanyId { get; set; }
        public string TipoNfe { get; set; }   
        public string Cnpj { get; set; }
        public DateTime? DhEmiss { get; set; }      
    }
}
