using MediatR;
using System;

namespace Corporate.Contta.Schedule.Application.Mapping.Request
{
    public class GetAllByProductIdCompanyRequest: IRequest<Response.Response>
    {
        public Guid EmpresaId { get; set; }
        public bool IcmsSt { get; set; }
        public bool Beneficio { get; set; }
        public bool Insento { get; set; }
        public bool Imune { get; set; }
        public bool InsencaoCesta { get; set; }
        public bool Insencao { get; set; }
        public bool Monofasico { get; set; }
        public DateTime DateEmiss { get; set; }
        public bool DeparaProd { get; set; }
        public bool Alterado { get; set; }
    }
}
