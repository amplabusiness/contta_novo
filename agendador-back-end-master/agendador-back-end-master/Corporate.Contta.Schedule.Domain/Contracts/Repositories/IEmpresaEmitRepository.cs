using Corporate.Contta.Schedule.Domain.Entities.NfeAgg;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Domain.Contracts.Repositories
{
    public interface IEmpresaEmitRepository
    {
        Task<EmpresaEmit> GetByDocumento(Guid compayId);

        Task<EmpresaEmit> GetById(Guid id);

        Task<List<EmpresaEmit>> GetAll();

        Task<EmpresaEmit> ObterPorCnpj(string cnpj);

        Task<EmpresaEmit> Insert(EmpresaEmit empresaEmit);

    }
}
