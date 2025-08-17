using System;

namespace Corporate.Contta.Schedule.Domain.Entities.RegistroBlEAgg
{
    public class RegistroE113
    {
        public Guid? Id { get; set; }
        /// <summary>
        ///     Código do participante
        /// </summary>
        /// <remarks>
        ///     - do emitente do documento ou do remetente das mercadorias, no caso das entradas;
        ///     - do adquirente, no caso de saídas
        /// </remarks>
        public string codParticipante { get; set; }

        /// <summary>
        ///     Código do modelo do documento fiscal
        /// </summary>
        public string codModeloDocumento { get; set; }

        /// <summary>
        ///     Série do documento fiscal
        /// </summary>
        public string serie { get; set; }

        /// <summary>
        ///     Subsérie do documento fiscal
        /// </summary>
        public string subserie { get; set; }

        /// <summary>
        ///     Número do documento fiscal
        /// </summary>
        public long numeroDocumento { get; set; }

        /// <summary>
        ///     Data da emissão do documento fiscal
        /// </summary>
        public DateTime dataEmissao { get; set; }

        /// <summary>
        ///     Código do item
        /// </summary>
        public string codItem { get; set; }

        /// <summary>
        ///     Valor do ajuste para operação/item
        /// </summary>
        public decimal valorAjusteItem { get; set; }

        /// <summary>
        ///     A chave do item
        /// </summary>
        public decimal chave { get; set; }
    }
}
