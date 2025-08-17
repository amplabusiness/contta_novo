using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Infra.Models.ExternalTable
{
    public class ExternalTableModel
    {

        [JsonProperty("item")]    
        public string Item { get; set; }

        [JsonProperty("subItem")]
        public string SubItem { get; set; }

        [JsonProperty("descricao")]
        public string Descricao { get; set; }

        [JsonProperty("Cest")]
        public string CEST { get; set; }

        [JsonProperty("ncm")]
        public string NCM { get; set; }

        [JsonProperty("Mva1")]
        public int MVA1 { get; set; }

        [JsonProperty("Nva2")]
        public int MVA2 { get; set; }

        [JsonProperty("nva3")]
        public int MVA3 { get; set; }

        [JsonProperty("nva4")]
        public int MVA4 { get; set; }

        [JsonProperty("dataInicial")]
        public DateTime DataInicial { get; set; }

        [JsonProperty("dataFinal")]
        public DateTime DataFinal { get; set; }
    }
}
