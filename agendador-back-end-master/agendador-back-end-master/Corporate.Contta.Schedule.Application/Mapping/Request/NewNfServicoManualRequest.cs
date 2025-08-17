using Corporate.Contta.Schedule.Application.Mapping.Dto.Nfe;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class NewNfServicoManualRequest : IRequest<Response.Response>
    {
        public Guid CompanyInformation { get; set; }
        public TakerDto Taker { get; set; }
        public ActivityDto Activity { get; set; }
        public FederalRetentionsDto FederalRetentions { get; set; }
        public DemonstrativeDto Demonstrative { get; set; }
        public TaxCalculationDto TaxCalculation { get; set; }
        public bool NotaServeTransporte { get; set; }
    }
}
