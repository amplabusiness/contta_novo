using System.Collections.Generic;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Domain.Contracts.Repositories
{
    public interface IRepositorio<T> where T : class
    {
        void Add(T entity);
        void Delete(string id);
        Task<IEnumerable<T>> GetAll();
        void Dispose();
    }
}
