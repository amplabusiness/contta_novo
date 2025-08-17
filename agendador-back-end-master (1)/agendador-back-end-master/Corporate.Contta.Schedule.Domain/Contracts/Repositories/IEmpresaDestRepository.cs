using Corporate.Contta.Schedule.Domain.Entities.NfeAgg;
using System;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Domain.Contracts.Repositories
{
    public interface IEmpresaDestRepository
    {
        Task<EmpresaDest> GetByDocumento(Guid companyId);

        Task<EmpresaDest> GetById(Guid? id);

        Task<EmpresaDest> ObterPorCnpj(string cnpj);

        Task<EmpresaDest> Insert(EmpresaDest empresaEmit);
    }
}
