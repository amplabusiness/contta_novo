using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmaloteContta.Models.Respositories
{
    public interface IEmpresaEmitenteRepositorio : IRepositorio<Speed.CompanyInformation>
    {
        Task<bool> EmpresaJaExiste(string cnpj);
        Task<Speed.CompanyInformation> ObterPorCnpj(string cnpj);
    }
}
