using EmaloteContta.Models.Speed.Base;
using System.Collections.Generic;

namespace ConttaComsumidor.Models.CompanyInformationAgg
{
    public class CompanyInformation : Entity
    {
        public CompanyInformation()
        {
            Membership = new List<Membership>();
            SecondaryActivities = new List<SecondaryActivities>();
            this.Active = true;
        }

        public string BaseDTO { get; set; }
        public bool RegimeCompetence { get; set; }
        public string Name { get; set; }
        public bool RegimeBox { get; set; }

        public string NameFantasy { get; set; }

        public string StateRegistration { get; set; }
        public string Alias { get; set; }

        public string Cnpj { get; set; }

        public string Type { get; set; }

        public string Founded { get; set; }

        public string Regime { get; set; }

        public string Size { get; set; }

        public decimal? Capital { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string FederalEntity { get; set; }

        public string Anexo { get; set; }

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

        public string UserId { get; set; }

        public string MunicipalRegistration { get; set; }     

        public void DisabledCompany()
        {
            this.Active = false;
        }
    }
}
