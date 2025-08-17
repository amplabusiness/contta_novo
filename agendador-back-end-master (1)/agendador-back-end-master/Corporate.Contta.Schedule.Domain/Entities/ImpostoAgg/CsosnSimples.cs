using System.Collections.Generic;

namespace Corporate.Contta.Schedule.Domain.Entities.Imporsto
{
    public class CsosnSimples
    {
        public List<int> GetCsosn()
        {
            List<int> csosn = new List<int>
            {
                /// <summary>
                /// Tributada pelo Simples Nacional com permissão de crédito
                /// </summary>
                101,

                /// <summary>
                /// Tributada pelo Simples Nacional sem permissão de crédito
                /// </summary>
                102,

                /// <summary>
                /// Isenção do ICMS no Simples Nacional para faixa de receita bruta
                /// </summary>
                103,

                /// <summary>
                /// Tributada pelo Simples Nacional com permissão de crédito e com cobrança do ICMS por substituição tributária
                /// </summary>
                201,

                /// <summary>
                /// Tributada pelo Simples Nacional sem permissão de crédito e com cobrança do ICMS por substituição tributária
                /// </summary>
                202,


                /// <summary>
                /// Isenção do ICMS no Simples Nacional para faixa de receita bruta e com cobrança do ICMS por substituição tributária
                /// </summary>
                203,

                /// <summary>
                /// Imune
                /// </summary>
                300,

                /// <summary>
                /// Não tributada pelo Simples Nacional
                /// </summary>
                400,

                /// <summary>
                /// ICMS cobrado anteriormente por substituição tributária (substituído) ou por antecipação
                /// </summary>
                500,

                /// <summary>
                /// Outros
                /// </summary>
                900
            };

            return csosn;
        }
        
    }
}
