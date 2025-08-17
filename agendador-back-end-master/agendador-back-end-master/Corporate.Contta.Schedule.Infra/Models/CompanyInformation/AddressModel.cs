using Newtonsoft.Json;

namespace Corporate.Contta.Schedule.Infra.Models.CompanyInformation
{
    public class AddressModel
    {
        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("details")]
        public string Details { get; set; }

        [JsonProperty("zip")]
        public string Zip { get; set; }

        [JsonProperty("neighborhood")]
        public string Neighborhood { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("city_ibge")]
        public string CityIbge { get; set; }

        [JsonProperty("state_ibge")]
        public string StateIbge { get; set; }
    }
}
