using MediatR;
using System;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class UpdateNfeAjusteRequest : IRequest<Response.Response>
    {
        public Guid ajusteId { get; set; }
        public double TotalNfe { get; set; }
        public double Aliquota { get; set; }
        public double TotalCalculo { get; set; }
    }
}
