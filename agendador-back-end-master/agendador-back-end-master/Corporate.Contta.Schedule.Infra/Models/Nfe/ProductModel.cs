using Corporate.Contta.Schedule.Infra.Models.CompanyInformation;
using Newtonsoft.Json;
using System;

namespace Corporate.Contta.Schedule.Infra.Models.Nfe
{
    public class ProductModel
    {
        [JsonProperty("name")]
        public string CodProduto { get; set; }

        [JsonProperty("name")]
        public string DescProduto { get; set; }

        [JsonProperty("name")]
        public int NcmProd { get; set; }

        [JsonProperty("name")]
        public string UnidMedida { get; set; }

        [JsonProperty("name")]
        public double Quantidade { get; set; }

        [JsonProperty("name")]
        public double VlUnitario { get; set; }

        [JsonProperty("name")]
        public string UniMedTributado { get; set; }

        [JsonProperty("name")]
        public double QtdTributaria { get; set; }

        [JsonProperty("name")]
        public double VlUnitTributado { get; set; }

        [JsonProperty("name")]
        public double VlProduto { get; set; }

        [JsonProperty("name")]
        public double VlTlFrete { get; set; }

        [JsonProperty("name")]
        public double VlTlSeguro { get; set; }

        [JsonProperty("name")]
        public double VlTlDesconto { get; set; }

        [JsonProperty("name")]
        public double OutrasDespesas { get; set; }

        [JsonProperty("name")]
        public int Cfop { get; set; }

        [JsonProperty("name")]
        public int Ean { get; set; }

        [JsonProperty("name")]
        public decimal PedCompra { get; set; }

        [JsonProperty("name")]
        public string NItemPedido { get; set; }

        [JsonProperty("name")]
        public int Origem { get; set; }

        [JsonProperty("name")]
        public int Tributos { get; set; }

        [JsonProperty("name")]
        public int VlAproxTributos { get; set; }

        [JsonProperty("name")]
        public Guid NfeId { get; set; }

        [JsonProperty("name")]
        public CompanyInformationModel EmpresaEmitId { get; set; }
    }
}
