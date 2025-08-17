using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Domain.Entities.DashboardAgg.Apuracoes
{
    public class AgrupamentoDetalhamentoApuracao
    {
        public List<IcmsStPisConfinsDetalhamentoApuracao> IcmsStPisConfinsDetalhamentoApuracao { get; set; }
        public List<DevolucaoTransferenciaDetalhamentoApuracao> DevolucaoTransferenciaDetalhamentoApuracao { get; set; }
        public List<NotaFiscalCanceladaDetalhamentoApuracao> NotaFiscalCanceladaDetalhamentoApuracao { get; set; }
    }
}
