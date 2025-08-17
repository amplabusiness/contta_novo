using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmaloteContta.Models.Respositories;

namespace EmaloteContta.Infra.Base
{
    public class BaseRepository<T> : IRepositorio<T>, IDisposable where T : class, new()
    {
        private MongoDBContext<T> _dbContext;

        public BaseRepository(MongoDBContext<T> dbContext)
        {
            _dbContext = dbContext;
        }
        public virtual async Task<T> Add(T entity)
        {
            await _dbContext.GetColection.InsertOneAsync(entity);
            return entity;
        }
        public async void Delete(string id)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            await _dbContext.GetColection.DeleteOneAsync(filter);
        }

        public void Dispose() => GC.SuppressFinalize(this);

        public async Task<IEnumerable<T>> GetAll()
        {
            var documents = await _dbContext.GetColection.FindAsync(Builders<T>.Filter.Empty);

            return await documents.ToListAsync();
        }
    }
}
