
using Corporate.Contta.Schedule.Domain.Entities.Base;
using Corporate.Contta.Schedule.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Application.Mapping.Dto.Nfe
{
    public class ReclassificacaoFornecDto : Entity
    {
        public string RazaoSocial { get; set; }
        public string Fantasia { get; set; }
        public string ModeloNfe { get; set; }
        public string Cnpj { get; set; }

        public List<ProdutosFornecedor> ListaProdutos { get; set; }
    }
}
