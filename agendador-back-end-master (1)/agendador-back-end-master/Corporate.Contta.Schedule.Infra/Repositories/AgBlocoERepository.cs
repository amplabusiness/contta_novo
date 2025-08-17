using Corporate.Contta.Schedule.Domain.Contracts.Repositories;
using Corporate.Contta.Schedule.Domain.Entities.BlocoE;
using Corporate.Contta.Schedule.Infra.Repositories.Base;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Infra.Repositories
{
    public class AgBlocoERepository : BaseRepository<AgBlocoE>, IAgBlocoERepository
    {
        private static MongoDBContext<AgBlocoE> _dbContext = new MongoDBContext<AgBlocoE>();
        public AgBlocoERepository() : base(_dbContext)
        {

        }

        public async Task Create(AgBlocoE agBlocoE)
        {
            if (agBlocoE != null)
                agBlocoE.Id = Guid.NewGuid();
           await _dbContext.GetColection.InsertOneAsync(agBlocoE);             
        }

        public async Task<AgBlocoE> GetByCompanyInformation(Guid companyInformation, DateTime date)
        {
            var blocoE = new AgBlocoE();
            var inicioMes = new DateTime(date.Year, date.Month, 1);
            var fimMes = inicioMes.AddMonths(1).AddDays(-1);

            var result = await _dbContext.GetColection
                             .FindAsync(c => c.CompanyInformationId == companyInformation &&
                             (c.E100.DataInicial >= inicioMes && c.E100.DataInicial <= fimMes))
                              .ConfigureAwait(false);

            var dados = result.FirstOrDefaultAsync();

            if(dados.Result != null)
            {
                return  dados.Result;
            }

            return blocoE;
        }
    }
}
