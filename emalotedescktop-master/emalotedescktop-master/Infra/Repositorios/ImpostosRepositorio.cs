using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmaloteContta.Infra.Base;
using EmaloteContta.Models.Respositories;
using EmaloteContta.Models.Speed;
using MongoDB.Driver;
namespace EmaloteContta.Infra.Repositorios
{
    public class ImpostosRepositorio : BaseRepository<Impostos>, IImpostosRepositorio
    {
        private static MongoDBContext<Impostos> _dbContext = new MongoDBContext<Impostos>();

        public ImpostosRepositorio() : base(_dbContext) { }

        public override Task<Impostos> Add(Impostos entity)
        {
            if (!entity.Id.HasValue)
                entity.Id = Guid.NewGuid();
            return base.Add(entity);
        }

        public async Task<List<Impostos>> ObterTodosImpostoDaNota(Guid nfeId)
        {
            var result = await _dbContext.GetColection.FindAsync(c => c.NfeId == nfeId);
            return result.ToList();
        }

        public async Task<Impostos> ObterTodosImpostoProduto(Guid produtoId)
        {
            var result = await _dbContext.GetColection.FindAsync(c => c.ProdutoId == produtoId);
            return result.FirstOrDefault();
        }
    }
}
