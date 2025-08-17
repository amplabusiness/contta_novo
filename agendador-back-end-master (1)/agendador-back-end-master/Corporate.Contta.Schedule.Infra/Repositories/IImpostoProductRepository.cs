using Corporate.Contta.Schedule.Domain.Entities.ImpostoAgg;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Infra.Repositories
{
    public interface IImpostoProductRepository
    {
        Task<IEnumerable<ImpostoRedCestaBasica>> GetAllImpostoCestaBasica(Guid empresaId);
        Task<List<ImpostoAntecipacao>> GetAllImpostosAntecipacao(Guid empresaId);
        Task<ImpostoExigibilidadeSus> GetAllImpostosExi(Guid empresaId);
        Task<List<ImpostoImune>> GetAllImpostosimune(Guid empresaId);
        Task<List<ImpostoInsento>> GetAllImpostosInsento(Guid empresaId);
        Task<ImpostoReducao> GetAllImpostosReducao(Guid empresaId);
        Task InsertAnt(List<ImpostoAntecipacao> listEntity);
        Task InsertExigibilidadeSus(ImpostoExigibilidadeSus entity);
        Task InsertImun(List<ImpostoImune> listEntity);
        Task InsertInsent(List<ImpostoInsento> listEntity);
        Task InsertRedCestaBasica(List<ImpostoRedCestaBasica> listEntity);
        Task InsertReducao(ImpostoReducao entity);
    }
}