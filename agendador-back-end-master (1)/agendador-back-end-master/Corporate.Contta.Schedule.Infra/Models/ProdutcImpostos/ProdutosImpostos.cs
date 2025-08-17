using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Infra.Models.ProdutcImpostos
{
    public class ProdutosImpostos
    {        
        public Guid ProdutoId { get; set; }
        public bool Modificado { get; set; }
        public bool NcmMono { get; set; }
        public bool IcmsSt { get; set; }

    }
}
