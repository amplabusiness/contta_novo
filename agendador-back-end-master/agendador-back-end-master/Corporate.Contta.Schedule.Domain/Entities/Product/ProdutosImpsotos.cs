using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Domain.Entities.Product
{
   public class ProdutosImpsotos
    {
        public Guid ProdutoId { get; set; }
        public bool Modificado { get; set; }
        public bool NcmMono { get; set; }
        public bool IcmsSt { get; set; }
        public Guid EmpresaId { get; set; }
        public DateTime DataOperacao { get; set; }
    }
}
