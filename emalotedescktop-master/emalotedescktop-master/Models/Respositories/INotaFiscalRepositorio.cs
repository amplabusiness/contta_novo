using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmaloteContta.Models.Respositories
{
    public interface INotaFiscalRepositorio : IRepositorio<Speed.NFE>
    {
        Task<List<Speed.NFE>> ObterTodasAsNotasNaoProcessadas(string cnpj);
        Task<List<Speed.NFE>> ObterTodasAsNotasProcessadas(string cnpj);
        Task<List<Speed.NFE>> ObterTodasAsNotas(string cnpj);
        Task<bool> IntegrarNotaPeloId(Guid nfeId);
        Task<bool> IntegrarNotaPelaChave(string chave);

        Task<bool> NotaJaFoiGravada(string chave);

    }
}
