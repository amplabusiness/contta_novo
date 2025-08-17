using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using EmaloteContta.Models.Speed.Base;

namespace EmaloteContta.Models.Speed
{
    public class Produtos: Entity
    {
        public Produtos()
        {
            this.Id = Guid.NewGuid();
        }   

        public string CodProduto { get; set; }
        public string DescProduto { get; set; }
        public string NcmProd { get; set; }
        public string UnidMedida { get; set; }
        public float Quantidade { get; set; }
        public double VlUnitario { get; set; }
        public string UniMedTributado { get; set; }
        public float QtdTributaria { get; set; }
        public double VlUnitTributado { get; set; }
        public double VlProduto { get; set; }
        public double VlTlFrete { get; set; }
        public double VlTlSeguro { get; set; }
        public double VlTlDesconto { get; set; }
        public double OutrasDespesas { get; set; }
        public double Cfop { get; set; }    
        public string Ean { get; set; }
        public string PedCompra { get; set; }
        public string NItemPedido { get; set; }
        public int Origem { get; set; }
        public double Tributos { get; set; }
        public double VlAproxTributos { get; set; }
        public Guid NfeId { get; set; }
        public Guid EmpresaEmitId { get; set; }
        public int Csons { get; set; }
    }
}
