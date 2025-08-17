using MediatR;
using System;

namespace Corporate.Contta.Schedule.Application.Mapping.Request.Impostos
{
    public class GetAllProdutosImpostosRequest : IRequest<Response.Response>
    {
        public Guid EmpresaId { get; set; }
        public bool AntecipacapTri { get; set; }
        public bool Exigibilidade { get; set; }
        public bool Imune { get; set; }
        public bool Insento { get; set; }
        public bool CestaBasica { get; set; }
        public bool Reducao { get; set; }
    }
}
