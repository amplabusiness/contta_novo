using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Domain.Enum
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
        NaoIdentificada,
        CartaCorrecao,
        CienciaOperacao,
        eventoCTe,
        ConfirmacaoOperaca,
        DesconhecimentoOperacao
    }
}
