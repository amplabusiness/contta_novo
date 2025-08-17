using Corporate.Contta.Schedule.Domain.Entities.CompanyInformationAgg;
using System;
using System.Collections.Generic;

namespace Corporate.Contta.Schedule.Domain.Entities
{
    public class CompanyInformationDto
    {
        public CompanyInformationDto()
        {
            Membership = new List<Membership>();
            SecondaryActivities = new List<SecondaryActivities>();

        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public string StateRegistration { get; set; }

        public string Cnpj { get; set; }

        public string Type { get; set; }

        public DateTime? Founded { get; set; }

        public string Size { get; set; }

        public decimal? Capital { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string FederalEntity { get; set; }

        public bool? Active { get; set; }

        public List<Anexo> Anexo { get; set; }

        public string UserId { get; set; }

        public Registration Registration { get; set; }

        public AddressDto Address { get; set; }

        public LegalNature LegalNature { get; set; }

        public PrimaryActivity PrimaryActivity { get; set; }

        public List<SecondaryActivities> SecondaryActivities { get; set; }

        public List<Membership> Membership { get; set; }

        public FilesDto Files { get; set; }

        public Sintegra Sintegra { get; set; }

        public SimplesNacional SimplesNacional { get; set; }

        public string IntegradoEstoque { get; set; }

        public bool RegimeBox { get; set; }

        public bool RegimeCompetence { get; set; }

        public bool Transportadora { get; set; }
        public bool Servico { get; set; }

        public bool EmmpresaCad { get; set; }
        public bool EmpresaCadastrada { get; set; }
    }
}
