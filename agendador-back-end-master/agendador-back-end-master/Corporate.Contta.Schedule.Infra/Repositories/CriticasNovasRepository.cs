using Corporate.Contta.Schedule.Domain.Contracts.Repositories;
using Corporate.Contta.Schedule.Domain.Entities;
using Corporate.Contta.Schedule.Domain.Entities.Configuration;
using Corporate.Contta.Schedule.Domain.Entities.Criticas;
using Corporate.Contta.Schedule.Infra.Repositories.Base;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Infra.Repositories
{
    public class CriticasNovasRepository : BaseRepository<CriticasNovas>, ICriticasRepository
    {
        private static MongoDBContext<CriticasNovas> _dbContext = new MongoDBContext<CriticasNovas>();
        private static MongoDBContext<CompanyInformation> _dbContextCompany = new MongoDBContext<CompanyInformation>();

        public CriticasNovasRepository() : base(_dbContext) { }

        public async  Task<CriticasNovas> Get(Guid companyId)
        {
            var builder = Builders<CriticasNovas>.Filter;
            var filter = builder.Eq(nt => nt.CompanyInformation, companyId);
            var result = _dbContext.GetColection.Find(filter);
            var criticasDto = result.FirstOrDefault();

            return criticasDto;
        }

        public async Task Insert(CriticasNovas criticasNovas)
        {
            var builder = Builders<CompanyInformation>.Filter;
            var filter = builder.Eq(nt => nt.Id, criticasNovas.CompanyInformation);
            var result = _dbContextCompany.GetColection.Find(filter);
            var empresaDto = result.FirstOrDefault();

            if(empresaDto != null)
            {
                criticasNovas.Id = Guid.NewGuid();
                await _dbContext.GetColection.InsertOneAsync(criticasNovas).ConfigureAwait(false);
            }                     
        }

        public Task Insert(CriticasAntigas criticasAntigas)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(ConfigurationUser configurationUser)
        {
            throw new NotImplementedException();
            //var update = Builders<ConfigurationUser>.Update.Set(c => c.DashboardTutorial, configurationUser.DashboardTutorial)

            //var updateResult = await _dbContext.GetColection.UpdateOneAsync(c => c.Id == configurationUser.Id, update);

            //return updateResult.ModifiedCount > 0;
        }
              
    }
}
