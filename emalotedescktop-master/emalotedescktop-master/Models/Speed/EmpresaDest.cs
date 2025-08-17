using System;
using System.Collections.Generic;
using System.Text;

namespace EmaloteContta.Models.Speed
{
    public class EmpresaDest: Empresa
    {
        public string InscEstSubTribu { get; set; }
        public bool? Estrangeiro { get; set; }
        public string CPF { get; set; }
    }
}
