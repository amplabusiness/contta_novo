using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Domain.Entities.NfeAgg
{
    public class Reclassificacao
    {
        public string RazaoSocial { get; set; }
        public string Fantasia { get; set; }
        public string ModeloNfe { get; set; }
        public string Cnpj { get; set; }

        public List<Produtos> ListaProdutos { get; set; }
    }
}
