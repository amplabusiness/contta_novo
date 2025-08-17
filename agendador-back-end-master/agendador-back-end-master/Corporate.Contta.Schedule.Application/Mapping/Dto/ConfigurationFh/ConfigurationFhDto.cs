using System;

namespace Corporate.Contta.Schedule.Domain.Entities.ConfigurationFhAgg
{
    public class ConfigurationFhDto
    {
        public Guid Id { get; set; }
        public Guid CompanyInformation { get; set; }
        public FechamentoSimples FechamentoSimples { get; set; }
        public FechamentoLivroEntrada FechamentoLivroEntrada { get; set; }
        public FechamentoLivroCaixa FechamentoLivroCaixa { get; set; }
        public FechamentoLivroSaida FechamentoLivroSaida { get; set; }

    }
}
