using Corporate.Contta.Schedule.Domain.Entities.Product;
using Corporate.Contta.Schedule.Infra.Models.ProdutcImpostos;
using MediatR;
using System;
using System.Collections.Generic;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class UpdateProdIcmsMonoRequest : List<ProdutosImpsotos>, IRequest<Response.Response>
    {
    }
}
