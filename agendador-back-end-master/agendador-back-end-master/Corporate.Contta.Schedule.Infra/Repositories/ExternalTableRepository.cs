using Corporate.Contta.Schedule.Domain.Contracts.Repositories;
using Corporate.Contta.Schedule.Domain.Entities.ExternalTable;
using Corporate.Contta.Schedule.Infra.Repositories.Base;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Infra.Repositories
{
    class ExternalTableRepository : BaseRepository<IcmsSt>, IExternalTableRepository
    {
        private static MongoDBContext<IcmsSt> _dbContext = new MongoDBContext<IcmsSt>();

        private IExternalTableRepository _externalTableRepository;

        public ExternalTableRepository(IExternalTableRepository tableRepository) : base(_dbContext)
        {
            _externalTableRepository = tableRepository;
        }

        public async Task Insert(IcmsSt icmsSt)
        {
            await _dbContext.GetColection.InsertOneAsync(icmsSt).ConfigureAwait(false);
        }
    }
}
