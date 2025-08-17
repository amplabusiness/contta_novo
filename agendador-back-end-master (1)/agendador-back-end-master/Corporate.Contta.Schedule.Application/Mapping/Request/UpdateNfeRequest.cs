using Corporate.Contta.Schedule.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class UpdateCompanyRequest : IRequest<Response.Response>
    {
        public Guid Id { get; set; }

        public SimplesNacional SimplesNacional { get; set; }

        public bool RegimeBox { get; set; }

        public bool RegimeCompetence { get; set; }

        public int Tipo { get; set; }

    }
}
