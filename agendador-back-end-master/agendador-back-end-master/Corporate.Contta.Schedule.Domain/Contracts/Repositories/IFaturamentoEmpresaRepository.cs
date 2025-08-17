using Corporate.Contta.Schedule.Domain.Entities.NfeAgg;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Domain.Contracts.Repositories
{
    public interface IFaturamentoEmpresaRepository
    {
        Task Insert(FaturamentoEmpresa faturamentoEmpresa);
         double InsertCurt(FaturamentoEmpresa faturamentoEmpresa);

        Task<bool> Update(FaturamentoEmpresa faturamentoEmpresa,bool fehcarFaturamento);
        Task<bool> UpdateFaturas(FaturamentoEmpresa faturamentoEmpresa);

        Task<bool> Delete(Guid idFaturamentoEmpresa);

        Task<FaturamentoEmpresa> GetByIdEmpresa(Guid idEmpresa);

    }
}
