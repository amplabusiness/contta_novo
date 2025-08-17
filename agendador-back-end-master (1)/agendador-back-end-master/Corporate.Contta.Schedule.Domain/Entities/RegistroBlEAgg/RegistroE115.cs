using System;

namespace Corporate.Contta.Schedule.Domain.Entities.RegistroBlEAgg
{
    public class RegistroE115
    {
        public Guid? Id { get; set; }
        /// <summary>
        ///     Código da informação adicional conforme tabela a ser definida pelas SEFAZ
        /// </summary>
        public string codInformacao { get; set; }

        /// <summary>
        ///     Valor referente à informação adicional
        /// </summary>
        public decimal valorInformacao { get; set; }

        /// <summary>
        ///     Descrição complementar do ajuste
        /// </summary>
        public string descComplementar { get; set; }
    }
}
