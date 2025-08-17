using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmaloteContta.Models.Respositories
{
    public interface IImpostosRepositorio : IRepositorio<Speed.Impostos>
    {
        Task<Speed.Impostos> ObterTodosImpostoProduto(Guid produtoId);
        Task<List<Speed.Impostos>> ObterTodosImpostoDaNota(Guid nfeId);
    }
}
