using Corporate.Contta.Schedule.Domain.Entities;
using Corporate.Contta.Schedule.Domain.Entities.EstoqueAgg;
using Corporate.Contta.Schedule.Domain.Entities.NfeAgg;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Infra.Repositories.Interfaces
{
    public interface ICompanyGateway
    {
        Task<CompanyInformation> GetCompanyInformationByCnpj(string cnpj, string userId, Guid id, bool ConfirmarCadastro, Guid? userIdTerceiro);
        Task<EmpresaDest> GetCompanyInformationByCnpjDest(string cnpj, string userId, Guid id);
        Task NewComanyLote(string listCnpj, string token);
        List<Estoque> GetListaEstoque(string diretory);
    }
}
