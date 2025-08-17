using Corporate.Contta.Schedule.Application.Mapping.Dto.Nfe;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class NewNfVendaManualRequest: IRequest<Response.Response>
    {
        public Guid CompanyInformation { get; set; }
        public TaxesDto Taxes { get; set; }
        public List<ProductsDto> Products { get; set; }
        public ReceiverDto Receiver { get; set; }
    }
}
