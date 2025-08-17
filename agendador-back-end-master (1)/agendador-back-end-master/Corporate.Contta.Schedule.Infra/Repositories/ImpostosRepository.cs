using Corporate.Contta.Schedule.Domain.Contracts.Repositories;
using Corporate.Contta.Schedule.Domain.Entities.NfeAgg;
using Corporate.Contta.Schedule.Infra.Repositories.Base;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Infra.Repositories
{
    public class ImpostosRepository : BaseRepository<Impostos>, IImpostosRepository
    {
        private static MongoDBContext<Impostos> _dbContext = new MongoDBContext<Impostos>();

        public ImpostosRepository() : base(_dbContext)
        {
        }

        public async Task<Impostos> GetByProductAndNfe(Guid produtoId, Guid nfeId)
        {
            var result = await _dbContext.GetColection.FindAsync(c => c.ProdutoId.Equals(produtoId) && c.NfeId.Equals(nfeId)).ConfigureAwait(false);

            return result.FirstOrDefault();
        }

        public async Task<Impostos> GetByCompanyAndNfe(Guid CompanyInformation, Guid nfeId)
        {
            var result = await _dbContext.GetColection.FindAsync(c => c.CompanyInformation.Equals(CompanyInformation) && c.NfeId.Equals(nfeId)).ConfigureAwait(false);

            return result.FirstOrDefault();
        }

        public async Task<Impostos> GetById(Guid id)
        {
            var result = await _dbContext.GetColection.FindAsync(c => c.Id.Equals(id)).ConfigureAwait(false);

            return result.FirstOrDefault();
        }

        public async Task Insert(Impostos impostos)
        {
            if (impostos != null)
                impostos.Id = Guid.NewGuid();
             _dbContext.GetColection.InsertOne(impostos);
        }
    }
}
