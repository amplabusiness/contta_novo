using System;
using System.Collections.Generic;
using System.Text;

namespace EmaloteContta.Data
{
    public class XmlCliente
    {     
        public Guid IdCompany { get; set; }
        public string CNPJ { get; set; }
        public string Url { get; set; }
        public string TokenAz { get; set; }
    }
}
