using Corporate.Contta.Schedule.Domain.Entities.NfeAgg;
using Corporate.Contta.Schedule.Domain.Entities.Product;
using MediatR;
using System.Collections.Generic;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class UpdateProdutoFornecRequest: IRequest<Response.Response>
    {
        public string RazaoSocial { get; set; }
        public string Fantasia { get; set; }
        public string ModeloNfe { get; set; }
        public string Cnpj { get; set; }  
        public List<ProdutosFornecedor> listaProdutos { get; set; }     
    }
}
