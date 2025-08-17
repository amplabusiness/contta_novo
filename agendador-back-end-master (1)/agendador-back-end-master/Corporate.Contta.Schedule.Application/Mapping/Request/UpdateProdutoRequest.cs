using Corporate.Contta.Schedule.Domain.Entities.NfeAgg;
using Corporate.Contta.Schedule.Domain.Entities.Product;
using MediatR;
using System;
using System.Collections.Generic;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class UpdateProdutoRequest: NewProductRequest, IRequest<Response.Response>
    {
        public string RazaoSocial { get; set; }
        public string Fantasia { get; set; }
        public string ModeloNfe { get; set; }
        public string Cnpj { get; set; }
        public Guid Id { get; set; }
        public Guid EmpresaId { get; set; }
        public bool Depara { get; set; }
        public string CodProFornecedor { get; set; }
        public string CodProCliente { get; set; }
        public string Marca { get; set; }
        public bool Entrada { get; set; }
        public List<Produtos> ListProdutos { get; set; }     
        public List<ProdutosFornecedor> ListProdutosFornect { get; set; }     

    }
}
