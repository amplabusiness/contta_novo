using Corporate.Contta.Schedule.Domain.Contracts.Repositories;
using Corporate.Contta.Schedule.Domain.Entities.NfeAgg;
using Corporate.Contta.Schedule.Infra.Repositories.Base;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Infra.Repositories
{
    public class EmpresaDestRepository : BaseRepository<EmpresaDest>, IEmpresaDestRepository
    {
        private static MongoDBContext<EmpresaDest> _dbContext = new MongoDBContext<EmpresaDest>();

        public EmpresaDestRepository() : base(_dbContext) { }
        public async Task<EmpresaDest> GetByDocumento(Guid documento)
        {
            //documento = documento.Replace(".", "").Replace("-", "").Replace("/", "");
            var result = await _dbContext.GetColection.FindAsync(c => c.Id == documento).ConfigureAwait(false);
            return result.FirstOrDefault();
        }

        public async Task<EmpresaDest> GetById(Guid? id)
        {
            var result = await _dbContext.GetColection.FindAsync(c => c.Id.HasValue && c.Id.Value == id).ConfigureAwait(false);
            return result.FirstOrDefault();
        }

        public async Task<EmpresaDest> Insert(EmpresaDest empresaDest)
        {
            try
            {
                if (empresaDest != null)
                    empresaDest.Id = Guid.NewGuid();
                await _dbContext.GetColection.InsertOneAsync(empresaDest);

                return empresaDest;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<EmpresaDest> ObterPorCnpj(string cnpj)
        {
            try
            {
                var result = await _dbContext.GetColection.FindAsync(c => c.Cnpj.Equals(cnpj) || c.CPF.Equals(cnpj));
                return await result.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
