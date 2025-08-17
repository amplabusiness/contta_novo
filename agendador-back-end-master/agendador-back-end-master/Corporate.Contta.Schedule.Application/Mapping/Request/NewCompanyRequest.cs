using Corporate.Contta.Schedule.Domain.Entities;
using Corporate.Contta.Schedule.Domain.Entities.CompanyInformationAgg;
using MediatR;
using System;
using System.Collections.Generic;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class NewCompanyRequest : IRequest<Response.Response>
    {

        public string Name { get; set; }

        public string NameFantasy { get; set; }

        public string StateRegistration { get; set; }

        public string Alias { get; set; }

        public string Cnpj { get; set; }

        public string Type { get; set; }

        public DateTime DateFounded { get; set; }

        public DateTime? Founded { get; set; }

        public string Regime { get; set; }

        public string Size { get; set; }

        public decimal? Capital { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Fax { get; set; }

        public string FederalEntity { get; set; }

        public bool Active { get; set; }

        public string StateWriting { get; set; }

        public string Sugrama { get; set; }

        public string MunicipalOffice { get; set; }

        public List<Anexo> Anexo { get; set; }

        public string UserId { get; set; }

        public Registration Registration { get; set; }

        public Address Address { get; set; }

        public LegalNature LegalNature { get; set; }

        public PrimaryActivity PrimaryActivity { get; set; }

        public List<SecondaryActivities> SecondaryActivities { get; set; }

        public List<Membership> Membership { get; set; }

        public Files Files { get; set; }

        public Sintegra Sintegra { get; set; }

        public SimplesNacional SimplesNacional { get; set; }

        public bool RegimeBox { get; set; }

        public bool RegimeCompetence { get; set; }
    }
}
