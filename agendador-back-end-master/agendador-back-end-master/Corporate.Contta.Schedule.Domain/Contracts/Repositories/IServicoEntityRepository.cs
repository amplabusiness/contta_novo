using Corporate.Contta.Schedule.Domain.Entities.ServicoEntityAgg;
using System;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Domain.Contracts.Repositories
{
    public interface IServicoEntityRepository
    {
        Task<ServicoEntity> GetById(Guid id);
    }
}
