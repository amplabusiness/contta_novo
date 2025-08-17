using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.SpedContta.Models
{
    public interface IRepositorio<T> where T : class
    {
        Task<T> Add(T entity);
        void Delete(string id);
        Task<IEnumerable<T>> GetAll();
        void Dispose();
    }
}
