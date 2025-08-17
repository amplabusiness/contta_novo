using System;

namespace Corporate.Contta.Schedule.Domain.Entities.RegistroBlEAgg
{
    public class RegistroE111
    {
        public Guid? Id { get; set; }
        /// <summary>
        ///     Código de ajuste da apuração e dedução
        /// </summary>
        public string codAjuste { get; set; }

        /// <summary>
        ///     Descrição complementar do ajuste da apuração
        /// </summary>
        public string descComplementar { get; set; }

        /// <summary>
        ///     Valor do ajuste da apuração
        /// </summary>
        public decimal valorAjuste { get; set; }
    }
}
