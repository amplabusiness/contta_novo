using Corporate.Contta.Schedule.Domain.Contracts.Repositories;
using Corporate.Contta.Schedule.Domain.Entities.ServicoEntityAgg;
using Corporate.Contta.Schedule.Infra.Repositories.Base;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;


namespace Corporate.Contta.Schedule.Infra.Repositories
{
    public class ServicoEntityRepository : BaseRepository<ServicoEntity>, IServicoEntityRepository
    {
        private static MongoDBContext<ServicoEntity> _dbContext = new MongoDBContext<ServicoEntity>();

        public ServicoEntityRepository() : base(_dbContext) { }

        public async Task<ServicoEntity> GetById(Guid id)
        {
            var result = await _dbContext.GetColection.FindAsync(c => c.Id == id).ConfigureAwait(false);
            return result.FirstOrDefault();
        }
    }
}
