using Corporate.Contta.Schedule.Domain.Entities.CompanyInformationAgg;
using Corporate.Contta.Schedule.Infra.Repositories.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Corporate.Contta.Schedule.Infra.Models.CompanyInformation
{
    public class CompanyInformationModel : HttpBaseResult
    {
        public CompanyInformationModel()
        {
            Membership = new List<MembershipModel>();
            SecondaryActivities = new List<SecondaryActivitiesModel>();
        }

        [JsonProperty("name")]
        public string Name { get; set; }

        public string StateRegistration { get; set; }

        [JsonProperty("alias")]
        public string Alias { get; set; }

        [JsonProperty("tax_id")]
        public string Cnpj { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("founded")]
        public string Founded { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("capital")]
        public decimal? Capital { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("anexo")]
        public List<Anexo> Anexo { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("federal_entity")]
        public string FederalEntity { get; set; }
        
        public Guid UserId { get; set; }

        [JsonProperty("registration")]
        public RegistrationModel Registration { get; set; }

        [JsonProperty("address")]
        public AddressModel Address { get; set; }

        [JsonProperty("legal_nature")]
        public LegalNatureModel LegalNature { get; set; }

        [JsonProperty("primary_activity")]
        public PrimaryActivityModel PrimaryActivity { get; set; }

        [JsonProperty("secondary_activities")]
        public List<SecondaryActivitiesModel> SecondaryActivities { get; set; }

        [JsonProperty("membership")]
        public List<MembershipModel> Membership { get; set; }

        [JsonProperty("files")]
        public FilesModel Files { get; set; }

        [JsonProperty("sintegra")]
        public SintegraModel Sintegra { get; set; }

        [JsonProperty("simples_nacional")]
        public SimplesNacionalMode SimplesNacional { get; set; }

        [JsonProperty("regimeBox")]
        public bool RegimeBox { get; set; }

        [JsonProperty("integradoEstoque")]
        public string IntegradoEstoque { get; set; }

        [JsonProperty("regimeCompetence")]
        public bool RegimeCompetence { get; set; }

        [JsonProperty("transportadora")]
        public bool Transportadora { get; set; }
        [JsonProperty("servico")]
        public bool Servico { get; set; }
    }
}
