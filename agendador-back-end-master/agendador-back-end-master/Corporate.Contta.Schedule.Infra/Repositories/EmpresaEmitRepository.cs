using Corporate.Contta.Schedule.Domain.Contracts.Repositories;
using Corporate.Contta.Schedule.Domain.Entities.NfeAgg;
using Corporate.Contta.Schedule.Infra.Repositories.Base;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Infra.Repositories
{
    public class EmpresaEmitRepository : BaseRepository<EmpresaEmit>, IEmpresaEmitRepository
    {
        private static MongoDBContext<EmpresaEmit> _dbContext = new MongoDBContext<EmpresaEmit>();

        public EmpresaEmitRepository() : base(_dbContext) { }
        public async Task<EmpresaEmit> GetByDocumento(Guid documento)
        {
            //documento = documento.Replace(".", "").Replace("-", "").Replace("/", "");
            var result = await _dbContext.GetColection.FindAsync(c => c.Id == documento).ConfigureAwait(false);
            return result.FirstOrDefault();
        }

        public async Task<EmpresaEmit> GetById(Guid id)
        {
            var result = await _dbContext.GetColection.FindAsync(c => c.Id.HasValue && c.Id.Value == id).ConfigureAwait(false);
            return result.FirstOrDefault();
        }

        public  async Task<List<EmpresaEmit>> GetAll()
        {            
            var empresas = await _dbContext.GetColection.FindAsync(Builders<EmpresaEmit>.Filter.Empty);
            return empresas.ToList();
           
        }

        public async Task<EmpresaEmit> ObterPorCnpj(string cnpj)
        {
            var result =  _dbContext.GetColection.Find(c => c.Cnpj.Equals(cnpj)).FirstOrDefault();
            return result;
        }

        public async Task<EmpresaEmit> Insert(EmpresaEmit empresaEmit)
        {
            try
            {
                if (empresaEmit != null)
                    empresaEmit.Id = Guid.NewGuid();
               await _dbContext.GetColection.InsertOneAsync(empresaEmit);

                return empresaEmit;
            }
            catch (Exception)
            {

                throw;
            }
         
        }
    }
}
