using Contta.SpedFiscal;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.SpedContta.Models.Nfes
{
    public interface IEmpresaDestinatariaRepositorio : IRepositorio<Bloco0>
    {
        Task<bool> EmpresaJaExiste(string cnpj);
        Task<Bloco0> ObterPorCnpj(string cnpj);
    }
}
