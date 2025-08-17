using Corporate.Contta.Schedule.Domain.Contracts.Repositories;
using Corporate.Contta.Schedule.Domain.Entities.Configuration;
using Corporate.Contta.Schedule.Infra.Repositories.Base;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Infra.Repositories
{
    public class ConfigurationUserRepository : BaseRepository<ConfigurationUser>, IConfigurationUserRepository
    {
        private static MongoDBContext<ConfigurationUser> _dbContext = new MongoDBContext<ConfigurationUser>();

        public ConfigurationUserRepository() : base(_dbContext) { }

        public async Task<ConfigurationUser> Get(Guid configurationId)
        {
            var builder = Builders<ConfigurationUser>.Filter;
            var filter = builder.Eq(nt => nt.UserId, configurationId);
            var result = _dbContext.GetColection.Find(filter);
            var configuration = result.FirstOrDefault();

            return result.FirstOrDefault();
        }

        public async Task Insert(ConfigurationUser configurationUser)
        {
            configurationUser.Id = Guid.NewGuid();
            await _dbContext.GetColection.InsertOneAsync(configurationUser).ConfigureAwait(false);
        }

        public async Task<bool> Update(ConfigurationUser configurationUser)
        {
            var update = Builders<ConfigurationUser>.Update.Set(c => c.DashboardTutorial, configurationUser.DashboardTutorial)
                                              .Set(c => c.SubstituicaoTutorial, configurationUser.SubstituicaoTutorial)
                                              .Set(c => c.PisConfinsTutorial, configurationUser.PisConfinsTutorial)
                                              .Set(c => c.ClickedDownLoadButton, configurationUser.ClickedDownLoadButton)
                                              .Set(c => c.ClickedChangeCompanyButton, configurationUser.ClickedChangeCompanyButton)
                                              .Set(c => c.IcmSInsento, configurationUser.IcmSInsento)
                                              .Set(c => c.IcmSImune, configurationUser.IcmSImune)
                                              .Set(c => c.PISCofinsIsento, configurationUser.PISCofinsIsento)
                                              .Set(c => c.PISCofinsImune, configurationUser.PISCofinsImune)
                                              .Set(c => c.ProductTb, configurationUser.ProductTb);

            var updateResult = await _dbContext.GetColection.UpdateOneAsync(c => c.Id == configurationUser.Id, update);

            return updateResult.ModifiedCount > 0;
        }

        public async Task<bool> UpdateConf(Guid userId)
        {
            try
            {
                var configurationUser = _dbContext.GetColection.Find(c => c.UserId.Equals(userId)).FirstOrDefault();
                if (configurationUser != null)
                {
                    configurationUser.ProductTb = true;

                    var update = Builders<ConfigurationUser>.Update.Set(c => c.DashboardTutorial, configurationUser.DashboardTutorial)
                                               .Set(c => c.SubstituicaoTutorial, configurationUser.SubstituicaoTutorial)
                                               .Set(c => c.PisConfinsTutorial, configurationUser.PisConfinsTutorial)
                                               .Set(c => c.ClickedDownLoadButton, configurationUser.ClickedDownLoadButton)
                                               .Set(c => c.ClickedChangeCompanyButton, configurationUser.ClickedChangeCompanyButton)
                                               .Set(c => c.IcmSInsento, configurationUser.IcmSInsento)
                                               .Set(c => c.IcmSImune, configurationUser.IcmSImune)
                                               .Set(c => c.PISCofinsIsento, configurationUser.PISCofinsIsento)
                                               .Set(c => c.PISCofinsImune, configurationUser.PISCofinsImune)
                                               .Set(c => c.ProductTb, configurationUser.ProductTb);

                    var updateResult = await _dbContext.GetColection.UpdateOneAsync(c => c.Id == configurationUser.Id, update);

                    if (updateResult.ModifiedCount > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {               
                throw;
            }

            return false;
        }
    }
}
