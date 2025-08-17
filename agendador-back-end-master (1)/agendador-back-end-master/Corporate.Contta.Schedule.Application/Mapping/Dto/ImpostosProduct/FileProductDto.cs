using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Application.Mapping.Dto.ImpostosProduct
{
    [Serializable]
    public class FileProductDto
    {
        public int CodProduto { get; set; }
        public string DescProduto { get; set; }
        public string NCM { get; set; }
        public string CodigoBarra { get; set; }
        public double VlUnitario { get; set; }
        public double Qtd { get; set; }
    }
}
