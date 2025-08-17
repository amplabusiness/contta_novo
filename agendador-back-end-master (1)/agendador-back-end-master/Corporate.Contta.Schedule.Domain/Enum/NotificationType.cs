using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Domain.Enum
{
    public enum NotificationType
    {
        /// <summary>
        ///     Bloco com dados informados
        /// </summary>
        Lista_de_empresas_cadastradas_com_sucesso = 1222,

        /// <summary>
        ///     Bloco com dados informados
        /// </summary>
        Lista_de_estoque_cadastradas_com_sucesso = 1223,

        /// <summary>
        ///     Bloco sem dados informados
        /// </summary>
        Erro_Gravacao_Lista_Empresa = 1223,

        /// <summary>
        ///     Erro ao gravar lista de estoque.
        /// </summary>
        Erro_Gravacao_Lista_Estoque = 1001,


        /// <summary>
        ///    Tabela Já Cadastrada Na Nossa Base De Dados.
        /// </summary>
        Alerta_Tabela_Já_Cadastrada = 1,



        /// <summary>
        ///    Tabela Já Cadastrada Na Nossa Base De Dados.
        /// </summary>
        Erro_Arquivo_Enviado_Invalido = 2,



    }
}
