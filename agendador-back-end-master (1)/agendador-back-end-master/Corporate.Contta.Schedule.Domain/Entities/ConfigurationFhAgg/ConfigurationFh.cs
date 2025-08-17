using Corporate.Contta.Schedule.Domain.Entities.Base;
using System;

namespace Corporate.Contta.Schedule.Domain.Entities.ConfigurationFhAgg
{
    public class ConfigurationFh : Entity
    {
        public Guid? CompanyInformation { get; set; }
        public FechamentoSimples FechamentoSimples { get; set; }
        public FechamentoLivroEntrada FechamentoLivroEntrada { get; set; }
        public FechamentoLivroCaixa FechamentoLivroCaixa { get; set; }
        public FechamentoLivroSaida FechamentoLivroSaida { get; set; }

    }

    public class FechamentoSimples
    {
        public DateTime DataFechamento { get; set; }
    }

    public class FechamentoLivroEntrada
    {
        public DateTime DataFechamento { get; set; }
        public string CodUltimoEnviou { get; set; }
    }

    public class FechamentoLivroCaixa
    {
        public DateTime DataFechamento { get; set; }
        public string CodUltimoEnviou { get; set; }
    }

    public class FechamentoLivroSaida
    {
        public DateTime DataFechamento { get; set; }
        public string CodUltimoEnviou { get; set; }
    }
}
