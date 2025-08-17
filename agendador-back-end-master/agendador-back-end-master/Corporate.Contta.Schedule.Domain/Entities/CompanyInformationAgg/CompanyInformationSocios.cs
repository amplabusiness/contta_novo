using Corporate.Contta.Schedule.Domain.Entities.Base;
using Corporate.Contta.Schedule.Domain.Entities.CompanyInformationAgg;
using Corporate.Contta.Schedule.Domain.Entities.UserAgg;
using System;
using System.Collections.Generic;

namespace Corporate.Contta.Schedule.Domain.Entities
{
    public class CompanyInformationSocios : Entity
    {
        public CompanyInformationSocios()
        {
            Membership = new List<Membership>();
            Anexo = new List<Anexo>();
            SecondaryActivities = new List<SecondaryActivities>();
            this.Active = true;
        }

        public int? Tipo { get; set; }

        public string Name { get; set; }
        public string BaseDTO { get; set; }

        public string NameFantasy { get; set; }

        public string StateRegistration { get; set; }
        public string Alias { get; set; }

        public string Cnpj { get; set; }

        public string Type { get; set; }

        public DateTime? Founded { get; set; }

        public string Regime { get; set; }

        public string Size { get; set; }

        public decimal? Capital { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string FederalEntity { get; set; }

        public List<Anexo> Anexo { get; set; }

        public bool Active { get; set; }

        public Registration Registration { get; set; }

        public Address Address { get; set; }

        public LegalNature LegalNature { get; set; }

        public PrimaryActivity PrimaryActivity { get; set; }

        public List<SecondaryActivities> SecondaryActivities { get; set; }

        public List<Membership> Membership { get; set; }

        public Files Files { get; set; }

        public Sintegra Sintegra { get; set; }

        public SimplesNacional SimplesNacional { get; set; }

        public List<string> ListUserId { get; set; } = new List<string>();
        public string UserComunId { get; set; }

        public string MunicipalRegistration { get; set; }     

        public void DisabledCompany()
        {
            this.Active = false;
        }

        public bool RegimeBox { get; set; }

        public bool RegimeCompetence { get; set; }

        public string IntegradoEstoque { get; set; }

        public bool Transportadora { get; set; }
        public bool Servico { get; set; }

        public bool EmpresaCadastrada { get; set; }
    }
}
