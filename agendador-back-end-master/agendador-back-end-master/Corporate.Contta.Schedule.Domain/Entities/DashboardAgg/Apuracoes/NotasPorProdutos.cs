using Corporate.Contta.Schedule.Domain.Entities.NfeAgg;
using Corporate.Contta.Schedule.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Domain.Entities.DashboardAgg.Apuracoes
{
    public class NotasPorProdutos
    {
        public NFE Nfe{ get; set; }
        public Produtos Produtos { get; set; }
        public ProdutosFornecedor ProdutosFornec { get; set; }
    }
}
