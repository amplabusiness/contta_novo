using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoboEconet.Infra.Data.Interface
{
    public interface IRepositorio<T> where T : class
    {
        Task<T> Add(T entity);
        void Delete(string id);
        Task<IEnumerable<T>> GetAll();
        void Dispose();
    }
}
