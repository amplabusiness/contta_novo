using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmaloteContta.Models.NotaFiscalEletronicaMod55;

namespace EmaloteContta.Models.Service
{
    public interface INotaFiscalServices
    {
        Task<bool> GravarNota(NfeProc nota);

    }
}
