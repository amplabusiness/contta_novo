using Corporate.Contta.Schedule.Domain.Entities.NfeAgg;
using System;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Domain.Contracts.Repositories
{
    public interface IImpostosRepository
    {
        Task<Impostos> GetByProductAndNfe(Guid produtoId, Guid nfeId);

        Task<Impostos> GetByCompanyAndNfe(Guid companyId, Guid nfeId);

        Task<Impostos> GetById(Guid id);

        Task Insert(Impostos impostos );
    }
}
