using System;
using System.Collections.Generic;
using System.Text;

namespace EmaloteContta.Models
{
    public enum ETipoNota
    {
        NotaFiscalEletronica = 1,
        NotaFiscalConsumidorFinal,
        CTe,
        NotaFiscalDeServico,
        Devolucao,
        Cancelada,
        CanceladaCte,
        NaoIdentificada
    }
}
