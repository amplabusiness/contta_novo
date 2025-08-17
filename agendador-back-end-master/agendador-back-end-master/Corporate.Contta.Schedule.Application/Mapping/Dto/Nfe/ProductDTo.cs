using System;
using System.ComponentModel.DataAnnotations;

namespace Corporate.Contta.Schedule.Application.Mapping.Dto.Nfe
{
    public class ProductDto
    {
        [Key]
        public Guid Id { get; set; }
        public string Lei { get; set; }
        public bool Auditado { get; set; }
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
        public string Tributos { get; set; }
        public double VlAproxTributos { get; set; }
        public Guid NfeId { get; set; }
        public Guid EmpresaEmitId { get; set; }
        public int Csons { get; set; }
        public Guid? CompanyInformation { get; set; }

        public bool IntegradaEmpresa { get; set; }

        public bool NcmMono { get; set; }
        public bool IcmsSt { get; set; }
        public bool Benefico { get; set; }
        public bool Modificado { get; set; }
        public bool RoboMode { get; set; }
        public bool Validado { get; set; }
    }
}
