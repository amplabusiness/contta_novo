using System;

namespace Corporate.Contta.Schedule.Domain.Entities.RegistroBlEAgg
{
    public class RegistroE100
    {
        /// <summary>
        ///     Data inicial a que a apuração se refere.
        /// </summary>
        public DateTime dataInicial { get; set; }

        /// <summary>
        ///     Data final a que a apuração se refere.
        /// </summary>
        public DateTime dataFinal { get; set; }

        public Guid? Id { get; set; }
    }
}
