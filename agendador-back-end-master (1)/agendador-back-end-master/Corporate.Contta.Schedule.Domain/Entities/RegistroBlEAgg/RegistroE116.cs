using System;

namespace Corporate.Contta.Schedule.Domain.Entities.RegistroBlEAgg
{
    public class RegistroE116
    {
        public Guid? Id { get; set; }
        /// <summary>
        ///     Código da obrigação a recolher, conforme a Tabela 5.4
        /// </summary>
        public string codIcms { get; set; }

        /// <summary>
        ///     Valor da obrigação a recolher
        /// </summary>
        public decimal valorIcms { get; set; }

        /// <summary>
        ///     Data de vencimento da obrigação
        /// </summary>
        public string dataVencimentoIcms { get; set; }

        /// <summary>
        ///     Código de receita referente à obrigação, próprio da unidade da federação, conforme legislação estadual.
        /// </summary>
        public string codReceita { get; set; }

        /// <summary>
        ///     Número do processo ou auto de infração ao qual a obrigação está vinculada, se houver.
        /// </summary>
        public string numeroProcesso { get; set; }

        /// <summary>
        ///     Indicador da origem do processo:
        ///     0- SEFAZ;
        ///     1- Justiça Federal;
        ///     2- Justiça Estadual;
        ///     9- Outros
        /// </summary>
        public int origemProcesso { get; set; }

        /// <summary>
        ///     Descrição resumida do processo que embasou o lançamento
        /// </summary>
        public string descProcesso { get; set; }

        /// <summary>
        ///     Descrição complementar das obrigações a recolher.
        /// </summary>
        public string descComplementar { get; set; }

        /// <summary>
        ///     Informe o mês de referência no formato "mmaaaa"
        /// </summary>
        public string mesReferencia { get; set; }
    }
}
