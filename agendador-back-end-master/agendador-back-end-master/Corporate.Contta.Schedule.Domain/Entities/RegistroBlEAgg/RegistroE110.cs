using System;

namespace Corporate.Contta.Schedule.Domain.Entities.RegistroBlEAgg
{
    public class RegistroE110
    {
        public Guid? Id { get; set; }

        /// <summary>
        ///     Valor total dos débitos por "Saídas e prestações com débito do imposto"
        /// </summary>
        public decimal valorDebitoImpostos { get; set; }

        /// <summary>
        ///     Valor total dos ajustes a débito decorrentes do documento fiscal.
        /// </summary>
        public decimal valorAjustesDebitoDocFiscal { get; set; }

        /// <summary>
        ///     Valor total de "Ajustes a débito"
        /// </summary>
        public decimal valorAjustesDebito { get; set; }

        /// <summary>
        ///     Valor total de Ajustes “Estornos de créditos”
        /// </summary>
        public decimal valorEstornosCreditos { get; set; }

        /// <summary>
        ///     Valor total dos créditos por "Entradas e aquisições com crédito do imposto"
        /// </summary>
        public decimal valorCreditoImpostos { get; set; }

        /// <summary>
        ///     Data final a que a apuração se refere.
        /// </summary>
        public DateTime dataFinal { get; set; }

        /// <summary>
        ///     Valor total dos ajustes a crédito decorrentes do documento fiscal.
        /// </summary>
        public decimal valorAjustesCreditoDocFiscal { get; set; }

        /// <summary>
        ///     Valor total de "Ajustes a crédito"
        /// </summary>
        public decimal valorAjustesCredito { get; set; }

        /// <summary>
        ///     Valor total de Ajustes “Estornos de Débitos”
        /// </summary>
        public decimal valorEstornosDebitos { get; set; }

        /// <summary>
        ///     Valor total de "Saldo credor do período anterior"
        /// </summary>
        public decimal saldoCredorAnterior { get; set; }

        /// <summary>
        ///     Valor do saldo devedor apurado
        /// </summary>
        public decimal valorSaldoDevedor { get; set; }

        /// <summary>
        ///     Valor total de "Deduções"
        /// </summary>
        public decimal valorDeducoes { get; set; }

        /// <summary>
        ///     Valor total de "ICMS a recolher (11-12)
        /// </summary>
        public decimal valorIcmsRecolher { get; set; }

        /// <summary>
        ///     Valor total de "Saldo credor a transportar para o período seguinte”
        /// </summary>
        public decimal valorSaldoCredorIcms { get; set; }

        /// <summary>
        ///     Valores recolhidos ou a recolher, extraapuração.
        /// </summary>
        public decimal extraApuracao { get; set; }
    }
}
