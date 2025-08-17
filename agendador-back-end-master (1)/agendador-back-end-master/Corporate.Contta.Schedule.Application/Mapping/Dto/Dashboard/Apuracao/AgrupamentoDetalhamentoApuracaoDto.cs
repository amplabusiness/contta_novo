using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Application.Mapping.Dto.Dashboard.Apuracao
{
    public class AgrupamentoDetalhamentoApuracaoDto
    {
        public List<IcmsStPisConfinsDetalhamentoApuracaoDto> IcmsStPisConfinsDetalhamentoApuracao { get; set; }
        public List<DevolucaoTransferenciaDetalhamentoApuracaoDto> DevolucaoTransferenciaDetalhamentoApuracao { get; set; }
        public List<NotaFiscalCanceladaDetalhamentoApuracaoDto> NotaFiscalCanceladaDetalhamentoApuracao { get; set; }
    }
}
