using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Domain.Entities.Product
{
    public class FileProduct
    {
        public string CodProduto { get; set; }
        public string DescProduto { get; set; }
        public string NCM { get; set; }
        public string CodigoBarra { get; set; }
        public double VlUnitario { get; set; }
        public double Qtd { get; set; }
        public string Unidade { get; set; }
    }
}
