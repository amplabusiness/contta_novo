using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmaloteContta.Infra.Base;
using EmaloteContta.Models.Respositories;
using EmaloteContta.Models.Speed;
using Microsoft.Practices.Unity;

namespace EmaloteContta.Infra.Repositorios
{
    public class NotaFiscalLocalRepositorio : BaseRepository<NFE>, INotaFiscalRepositorio
    {
        private static MongoDBContext<NFE> _dbContext = new MongoDBContext<NFE>();
        private IEmpresaEmitenteRepositorio _emitenteRepositorio;
        public NotaFiscalLocalRepositorio() : base(_dbContext) { _emitenteRepositorio = Nucleo.Container.Resolve<IEmpresaEmitenteRepositorio>(); }

        public override Task<NFE> Add(NFE entity)
        {
            if (!entity.Id.HasValue)
                entity.Id = Guid.NewGuid();
            entity.Impostos.ForEach(c => { c.NfeId = entity.Id.Value; });
            entity.Produtos.ForEach(c => { c.NfeId = entity.Id.Value; c.EmpresaEmitId = entity.EmpresaEmetId; });

            return base.Add(entity);
        }

        public async Task<bool> IntegrarNotaPelaChave(string chave)
        {
            var update = Builders<NFE>.Update.Set(c => c.Integrada, true);
            var updateResult = await _dbContext.GetColection.UpdateOneAsync(c => c.CodBarra == chave, update);
            return updateResult.ModifiedCount > 0;
        }

        public async Task<bool> IntegrarNotaPeloId(Guid nfeId)
        {
            var update = Builders<NFE>.Update.Set(c => c.Integrada, true);
            var updateResult = await _dbContext.GetColection.UpdateOneAsync(c => c.Id == nfeId, update);
            return updateResult.ModifiedCount > 0;
        }

        public async  Task<bool> NotaJaFoiGravada(string chave)
        {
            var result = _dbContext.GetColection.Find(c => c.CodBarra.Equals(chave));
            return result.Any();
        }

        public async Task<List<NFE>> ObterTodasAsNotas(string cnpj)
        {
            var empresa = await _emitenteRepositorio.ObterPorCnpj(cnpj);
            if (empresa == null)
                throw new Exception("Empresa nao encontrada.");

            var result = await _dbContext.GetColection.FindAsync(c => c.EmpresaEmetId == empresa.Id);
            return result.ToList();
        }

        public async Task<List<NFE>> ObterTodasAsNotasNaoProcessadas(string cnpj)
        {
            var empresa = await _emitenteRepositorio.ObterPorCnpj(cnpj);
            if (empresa == null)
                throw new Exception("Empresa nao encontrada.");

            var result = await _dbContext.GetColection.FindAsync(c => c.EmpresaEmetId == empresa.Id && c.Integrada == false);
            return result.ToList();
        }

        public async Task<List<NFE>> ObterTodasAsNotasProcessadas(string cnpj)
        {
            var empresa = await _emitenteRepositorio.ObterPorCnpj(cnpj);
            if (empresa == null)
                throw new Exception("Empresa nao encontrada.");

            var result = await _dbContext.GetColection.FindAsync(c => c.EmpresaEmetId == empresa.Id && c.Integrada == true);
            return result.ToList();
        }
    }
}
