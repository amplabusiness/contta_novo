using Corporate.Contta.Schedule.Domain.Entities.EstoqueAgg;
using Corporate.Contta.Schedule.Domain.Entities.NfeAgg;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Domain.Contracts.Repositories
{
    public interface IEstoqueRepository
    {
      Task<List<Estoque>> GetAllEstoque(Guid companyId);
     void AddEstoque(string diretorio, Guid empresaId);
    }
}
