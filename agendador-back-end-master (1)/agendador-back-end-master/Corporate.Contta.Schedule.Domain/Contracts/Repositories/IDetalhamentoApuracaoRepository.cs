using Corporate.Contta.Schedule.Domain.Entities.DashboardAgg.Apuracoes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Domain.Contracts.Repositories
{
    public interface IDetalhamentoApuracaoRepository
    {
        Task<DetalhamentoApuracao> GetDetalhamentoApuracao(Guid companyId, DateTime dhEmiss);
        Task<DetalhamentoImposto> GetDetalhamentoImposto(Guid companyId, DateTime dhEmiss);

        Task<AgrupamentoDetalhamentoApuracao> GetAgrupamentoDetalhamentoApuracao(List<Guid> ids,string tipo,string grupo);
      
    }
}
