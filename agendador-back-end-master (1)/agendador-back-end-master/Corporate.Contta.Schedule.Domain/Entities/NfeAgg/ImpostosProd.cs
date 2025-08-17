using System;

namespace Corporate.Contta.Schedule.Domain.Entities.NfeAgg
{
    public class ImpostosProd
    {
        public Guid ProdutoId { get; set; }
        public string CodProduto { get; set; }
        public bool IcmsSt { get; set; }
        public bool NcmMono { get; set; } 
        public bool Isento { get; set; } 
        public bool Imune { get; set; } 
        public bool Beneficios { get; set; } 
        public bool IsencaoReducao { get; set; } 
        public bool Modificado { get; set; }    
    }
}
