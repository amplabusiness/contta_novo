using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ConttaComsumidor.Models.Respositories;
using Contta.Inteligencia.Cnpj.Model.Entity;

namespace ConttaComsumidor.Infra.Base
{
    public class BaseRepository<T> : IRepositorio<T>, IDisposable where T : class, new()
    {
        private MongoDBContext<T> _dbContext;

        private static MongoDBContext<Empresa> _dbContextEmpresa = new MongoDBContext<Empresa>();

        public BaseRepository(MongoDBContext<T> dbContext)
        {
            _dbContext = dbContext;
        }
        public virtual async Task<T> Add(T entity)
        {
            try
            {
                await _dbContext.GetColection.InsertOneAsync(entity);
                return entity;
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
        public async void Delete(string id)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            await _dbContext.GetColection.DeleteOneAsync(filter);
        }

        public void Dispose() => GC.SuppressFinalize(this);

        public async Task<Empresa> Get(string cnpj)
        {
            try
            {
                var result = _dbContextEmpresa.GetColection.Find(c => c.cnpj.Equals(cnpj)).FirstOrDefault();

                return result;
            }
            catch (Exception)
            {

                throw;
            }
         
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var documents = await _dbContext.GetColection.FindAsync(Builders<T>.Filter.Empty);

            return await documents.ToListAsync();
        }
    }
}
