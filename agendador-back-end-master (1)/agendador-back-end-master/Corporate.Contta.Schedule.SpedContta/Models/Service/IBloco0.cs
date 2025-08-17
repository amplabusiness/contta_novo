using Contta.SpedFiscal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.SpedContta.Models.Service
{
    public interface IBloco0
    {

        Task<bool> GravarNota(Bloco0.Registro0000 registro0000);

        Task<List<Bloco0>> GetEmpresaEmitent();

    }
}
