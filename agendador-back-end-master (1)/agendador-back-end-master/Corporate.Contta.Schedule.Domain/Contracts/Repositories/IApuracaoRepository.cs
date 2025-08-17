using Corporate.Contta.Schedule.Domain.Entities.DashboardAgg;
using Corporate.Contta.Schedule.Domain.Entities.NfeAgg;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Domain.Contracts.Repositories
{
    public interface IApuracaoRepository
    {
        Task<List<Apuracao>> GetAllApuracao(string companyIdUser, DateTime dataEmissao, Guid companyId);
        Task<ApuracaoCte> GetAllApuracaoCte(string companyIdUser, DateTime dataEmissao, Guid companyId);
        Task<ApuracaoService> GetAllApuracaoServico(string companyIdUser, DateTime dataEmissao, Guid companyId);
        Task<bool> NewHome(double aliquata, double fat12Messes, Guid companyId, DateTime dataEmissao);
        Task<List<Home>> GetHome(string Token, DateTime dataOperacao);
    }
}
