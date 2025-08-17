using Corporate.Contta.Schedule.Domain.Contracts.Repositories;
using Corporate.Contta.Schedule.Domain.Entities.ConfigurationFhAgg;
using Corporate.Contta.Schedule.Domain.Entities.NotificationAgg;
using Corporate.Contta.Schedule.Infra.Repositories.Base;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Infra.Repositories
{
    public class ConfigurationFhRepository : BaseRepository<ConfigurationFh>, IConfigurationFhRepository
    {
        private static MongoDBContext<ConfigurationFh> _dbContext = new MongoDBContext<ConfigurationFh>();

        public ConfigurationFhRepository() : base(_dbContext) { }

        public async Task<ConfigurationFh> Get(Guid? empresaId)
        {
            var result = _dbContext.GetColection.Find(c => c.CompanyInformation.Equals(empresaId)).FirstOrDefault();

            return result;
        }


        public async Task Insert(ConfigurationFh configurationFh)
        {
            configurationFh.Id = Guid.NewGuid();
            await _dbContext.GetColection.InsertOneAsync(configurationFh).ConfigureAwait(false);
        }

        public async Task<ConfigurationFh> Update(ConfigurationFh configurationFh)
        {
            try
            {
                var configurationFhDto = _dbContext.GetColection.Find(c => c.CompanyInformation.Equals(configurationFh.CompanyInformation)).FirstOrDefault();
                if (configurationFhDto != null)
                {


                    var update = Builders<ConfigurationFh>.Update.Set(c => c.FechamentoSimples.DataFechamento, configurationFh.FechamentoSimples.DataFechamento)
                                                                .Set(c => c.FechamentoLivroEntrada.DataFechamento, configurationFh.FechamentoLivroEntrada.DataFechamento)
                                                                .Set(c => c.FechamentoLivroEntrada.CodUltimoEnviou, configurationFh.FechamentoLivroEntrada.CodUltimoEnviou)
                                                                .Set(c => c.FechamentoLivroSaida.DataFechamento, configurationFh.FechamentoLivroSaida.DataFechamento)
                                                                .Set(c => c.FechamentoLivroSaida.CodUltimoEnviou, configurationFh.FechamentoLivroSaida.CodUltimoEnviou)
                                                                .Set(c => c.FechamentoLivroCaixa.DataFechamento, configurationFh.FechamentoLivroCaixa.DataFechamento)
                                                                .Set(c => c.FechamentoLivroCaixa.CodUltimoEnviou, configurationFh.FechamentoLivroCaixa.CodUltimoEnviou)
                                                                .Set(c => c.CompanyInformation, configurationFh.CompanyInformation);

                    var updateResult = await _dbContext.GetColection.UpdateOneAsync(c => c.Id == configurationFhDto.Id, update);

                    if (updateResult.ModifiedCount > 0)
                    {
                        var configurationDto = _dbContext.GetColection.Find(c => c.CompanyInformation.Equals(configurationFh.CompanyInformation)).FirstOrDefault();
                        return  configurationDto;                    }
                        
                    else
                    {
                        var configurationDto = _dbContext.GetColection.Find(c => c.CompanyInformation.Equals(configurationFh.CompanyInformation)).FirstOrDefault();
                        return  configurationDto;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return configurationFh;
        }

    }
}
