using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmaloteContta.Models.Respositories
{
    public interface IEmpresaDestinatariaRepositorio : IRepositorio<Speed.EmpresaDest>
    {
        Task<bool> EmpresaJaExiste(string cnpj);
        Task<Speed.EmpresaDest> ObterPorCnpj(string cnpj);
    }
}
