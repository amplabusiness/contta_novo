using System.Collections.Generic;

namespace RoboEconet.Models
{
    public class ICMSDto
    {
        public string BaseLegal { get; set; }
        public string Segmento { get; set; }
        public string NCM { get; set; }
        public string Descricao { get; set; }
        public decimal? MVAOriginal { get; set; }
        public decimal? MVAAjustada4 { get; set; }        
        public decimal? MVAAjustada7 { get; set; }        
        public decimal? MVAAjustada12 { get; set; }
        public decimal? AliquotaInterna { get; set; }
        public string CESTNCM { get; set; }
        public string CESTDescricao { get; set; }
        public string CEST { get; set; }
        public string CESTAnexo { get; set; }
        public string CESTSegmento { get; set; }
        public string CESTItem { get; set; }
        public string ConvenioProtocolo { get; set; }
        public string Signatarios { get; set; }
        public string Texto1 { get; set; }
        public string Texto2 { get; set; }
        public List<AliquotaIPI> AliquotasIPI { get; set; } = new List<AliquotaIPI>();
    }
}
