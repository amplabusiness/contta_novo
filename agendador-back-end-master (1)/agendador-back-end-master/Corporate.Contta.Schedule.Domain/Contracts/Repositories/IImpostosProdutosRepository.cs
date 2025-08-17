using Corporate.Contta.Schedule.Domain.Entities.ImpostoAgg;
using Corporate.Contta.Schedule.Domain.Entities.NfeAgg;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Domain.Contracts.Repositories
{
    public interface IImpostosProdutosRepository
    {
        Task InsertAnt(List<ImpostoAntecipacao> impostosAntecipacao );
        Task InsertInsent(List<ImpostoInsento> impostosInsento );
        Task InsertImun(List<ImpostoImune> impostosImune );
        Task InsertRedCestaBasica(List<ImpostoRedCestaBasica> impostoRedCestBasica);
        Task InsertExigibilidadeSus(ImpostoExigibilidadeSus impostoExigibilidadeSus);
        Task InsertReducao(ImpostoReducao impostoReducao);

        Task UpdateExigibilidadeSus(Guid id, bool status);
        Task UpdateEncerramento(Guid id, bool status);

        Task<List<ImpostoExigibilidadeSus>> GetAllImpostosExi(Guid empresaId);
        Task<List<ImpostoReducao>> GetAllImpostosReducao(Guid empresaId);
        Task<List<ImpostoAntecipacao>> GetAllImpostosAntecipacao(Guid empresaId);
        Task<List<ImpostoInsento>> GetAllImpostosInsento(Guid empresaId);
        Task<List<ImpostoImune>> GetAllImpostosimune(Guid empresaId);
        Task<List<TbCFOP>> GetAllCfop();
        Task<List<TbCfopGeral>> GetAllCfopGeral();
        Task<List<TbCFOP>> NewCfop(TbCFOP tbCfopGeral);
        Task<bool> DeleteCfop(Guid cfopId);
        Task<List<ImpostoRedCestaBasica>> GetAllImpostoCestaBasica(Guid empresaId);
        Task<List<NFE>> GetNfeDifal(Guid company, DateTime periudo);

    }
}
