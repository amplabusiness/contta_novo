using System;
using Contta.Common;

namespace Contta.SpedFiscal
{
    /// <summary>
    ///     BLOCO D: DOCUMENTOS FISCAIS II - SERVIÇOS (ICMS)
    /// </summary>
    public class BlocoD
    {
        /// <summary>
        ///     REGISTRO D001: ABERTURA DO BLOCO D
        /// </summary>
        public class RegistroD001 : RegistroBaseSped
        {
            /// <summary>
            /// Inicializa uma nova instância da classe <see cref="RegistroD001"/>.
            /// </summary>
            public RegistroD001()
            {
                Reg = "D001";
            }

            /// <summary>
            ///     Indicador de movimento: 0 - Bloco com dados informados; 1 - Bloco sem dados informados.
            /// </summary>
            [SpedCampos(2, "IND_MOV", "N", 1, 0, true)]
            public IndMovimento IndMov { get; set; }
        }

        /// <summary>
        ///     REGISTRO D100: NOTA FISCAL DE SERVIÇO DE TRANSPORTE (CÓDIGO 07) E CONHECIMENTOS DE TRANSPORTE RODOVIÁRIO DE CARGAS
        ///     (CÓDIGO 08),
        ///     CONHECIMENTOS DE TRANSPORTE DE CARGAS AVULSO (CÓDIGO 8B), AQUAVIÁRIO DE CARGAS (CÓDIGO 09), AÉREO (CÓDIGO 10),
        ///     FERROVIÁRIO DE CARGAS (CÓDIGO 11) E
        ///     MULTIMODAL DE CARGAS (CÓDIGO 26), NOTA FISCAL DE TRANSPORTE FERROVIÁRIO DE CARGA ( CÓDIGO 27) E CONHECIMENTO DE
        ///     TRANSPORTE ELETRÔNICO – CT-e (CÓDIGO 57).
        /// </summary>
        public class RegistroD100 : RegistroBaseSped
        {
            /// <summary>
            /// Inicializa uma nova instância da classe <see cref="RegistroD100"/>.
            /// </summary>
            public RegistroD100()
            {
                Reg = "D100";
            }

            /// <summary>
            ///     Indicador do tipo de operação:
            ///     0- Aquisição;
            ///     1- Prestação
            /// </summary>
            [SpedCampos(2, "IND_OPER", "C", 1, 0, true)]
            public int IndOper { get; set; }

            /// <summary>
            ///     Indicador do emitente do documento fiscal:
            ///     0 - Emissão própria;
            ///     1 - Terceiros;
            /// </summary>
            [SpedCampos(3, "IND_EMIT", "C", 1, 0, true)]
            public int IndEmit { get; set; }

            /// <summary>
            ///     Código do participante (campo 02 do Registro 0150):
            ///     - do emitente do documento ou do remetente das mercadorias, no caso de entradas;
            ///     - do adquirente, no caso de saídas.
            /// </summary>
            [SpedCampos(4, "COD_PART", "C", 60, 0, true)]
            public string CodPart { get; set; }

            /// <summary>
            ///     Código do modelo do documento fiscal, conforme a Tabela 4.1.1
            /// </summary>
            [SpedCampos(5, "COD_MOD", "C", 2, 0, true)]
            public string CodMod { get; set; }

            /// <summary>
            ///     Código da situação do documento fiscal, conforme a Tabela 4.1.2
            /// </summary>
            [SpedCampos(6, "COD_SIT", "N", 2, 0, true)]
            public int CodSit { get; set; }

            /// <summary>
            ///     Série do documento fiscal
            /// </summary>
            [SpedCampos(7, "SER", "C", 4, 0, false)]
            public string Ser { get; set; }

            /// <summary>
            ///     Subsérie do documento fiscal
            /// </summary>
            [SpedCampos(8, "SUB", "C", 3, 0, false)]
            public string Sub { get; set; }

            /// <summary>
            ///     Número do documento fiscal
            /// </summary>
            [SpedCampos(9, "NUM_DOC", "N", 9, 0, true)]
            public string NumDoc { get; set; }

            /// <summary>
            ///     Chave do Conhecimento de Transporte Eletrônico
            /// </summary>
            [SpedCampos(10, "CHV_CTE", "N", 44, 0, false)]
            public string ChvCte { get; set; }

            /// <summary>
            ///     Data da emissão do documento fiscal
            /// </summary>
            [SpedCampos(11, "DT_DOC", "N", 8, 0, true)]
            public DateTime DtDoc { get; set; }

            /// <summary>
            ///     Data da aquisição ou da prestação do serviço
            /// </summary>
            [SpedCampos(12, "DT_DOC", "N", 8, 0, false)]
            public DateTime DtAP { get; set; }

            /// <summary>
            ///     Tipo de Conhecimento de Transporte Eletrônico conforme definido no Manual de Integração do CT-e
            /// </summary>
            [SpedCampos(13, "TP_CT-e", "N", 1, 0, false)]
            public int TpCte { get; set; }

            /// <summary>
            ///     Chave do CT-e de referência cujos valores foram complementados (opção “1” do campo anterior) ou cujo débito foi
            ///     anulado(opção “2” do campo anterior).
            /// </summary>
            [SpedCampos(14, "CHV_CTE_REF", "N", 44, 0, false)]
            public string ChvCteRef { get; set; }

            /// <summary>
            ///     Valor total do documento fisca
            /// </summary>
            [SpedCampos(15, "VL_DOC", "N", 0, 2, true)]
            public decimal VlDoc { get; set; }

            /// <summary>
            ///     Valor total do desconto
            /// </summary>
            [SpedCampos(16, "VL_DESC", "N", 0, 2, false)]
            public decimal VlDesc { get; set; }

            /// <summary>
            ///     Indicador do tipo do frete:
            ///     0- Por conta de terceiros;
            ///     1- Por conta do emitente;
            ///     2- Por conta do destinatário;
            ///     9- Sem cobrança de frete.
            /// </summary>
            [SpedCampos(17, "IND_FRT", "N", 1, 0, true)]
            public int IndFrt { get; set; }

            /// <summary>
            ///     Valor total do serviço
            /// </summary>
            [SpedCampos(18, "VL_SERV", "N", 0, 2, true)]
            public decimal VlServ { get; set; }

            /// <summary>
            ///     Valor da base de cálculo do ICMS
            /// </summary>
            [SpedCampos(19, "VL_BC_ICMS", "N", 0, 2, false)]
            public decimal VlBcIcms { get; set; }

            /// <summary>
            ///     Valor do ICMS
            /// </summary>
            [SpedCampos(20, "VL_ICMS", "N", 0, 2, false)]
            public decimal VlIcms { get; set; }

            /// <summary>
            ///     Valor do ICMS
            /// </summary>
            [SpedCampos(21, "VL_NT", "N", 0, 2, false)]
            public decimal VlNt { get; set; }

            /// <summary>
            ///     Código da informação complementar do documento fiscal (campo 02 do Registro 0450)
            /// </summary>
            [SpedCampos(22, "COD_INF", "C", 6, 0, false)]
            public string CodInf { get; set; }

            /// <summary>
            ///     Código da informação complementar do documento fiscal (campo 02 do Registro 0450)
            /// </summary>
            [SpedCampos(22, "COD_CTA", "C", 0, 0, false)]
            public string CodCta { get; set; }
        }

        /// <summary>
        ///     REGISTRO D110: itens das Notas Fiscais de Serviços de Transporte
        /// </summary>
        public class RegistroD110 : RegistroBaseSped
        {
            /// <summary>
            /// Este Registro deve ser apresentado para informar os itens das Notas Fiscais de Serviços de Transporte (Código 07) fornecidas no Registro D100. <see cref="RegistroD120"/>.
            /// </summary>
            public RegistroD110()
            {
                Reg = "D110";
            }

            /// <summary>
            ///    Número sequencial do item no documento fiscal.
            /// </summary>
            [SpedCampos(2, "NUM_ITEM", "N", int.MaxValue, 0, true)]
            public int NumItem { get; set; }

            /// <summary>
            ///     Código do item (Campo 02 do Registro 0200).
            /// </summary>
            [SpedCampos(3, "COD_ITEM", "C", 6, 0, true)]
            public int CodigoItem { get; set; }

            /// <summary>
            ///     Valor do serviço.
            /// </summary>
            [SpedCampos(4, "VL_SERV", "N", int.MaxValue, 0, true)]
            public int ValorServico { get; set; }

            /// <summary>
            ///    Outros Valores.
            /// </summary>
            [SpedCampos(5, "VL_OUT", "N", int.MaxValue, 0, true)]
            public int OutroValores { get; set; }
        }


        /// <summary>
        ///     REGISTRO D120: Complemento da Nota Fiscal de Serviços de Transporte  DO BLOCO D.
        /// </summary>
        public class RegistroD120 : RegistroBaseSped
        {
            /// <summary>
            /// Este Registro deve ser apresentado para informar o complemento das Notas Fiscais de Serviços de Transporte (Código 07), com Municípios de origem e destino do transporte.. <see cref="RegistroD120"/>.
            /// </summary>
            public RegistroD120()
            {
                Reg = "D120";
            }

            /// <summary>
            ///    Código do Município de origem do serviço, conforme a Tabela IBGE (Preencher com "9999999", se Exterior).
            /// </summary>
            [SpedCampos(2, "COD_MUN_ORIG", "N", int.MaxValue, 0, true)]
            public int CodigoMunicipio { get; set; }

            /// <summary>
            ///     Código do município de destino, conforme a Tabela IBGE (Preencher com "9999999", se Exterior).
            /// </summary>
            [SpedCampos(2, "COD_MUN_DEST", "C", 7, 0, true)]
            public int CodigoMunicipioDes  { get; set; }

            /// <summary>
            ///    	Placa de identificação do veículo.
            /// </summary>
            [SpedCampos(2, "VEIC_ID", "C", int.MaxValue, 0, true)]
            public int IndentificacaoVeiculo { get; set; }

            /// <summary>
            ///   Sigla da UF da placa do veículo.
            /// </summary>
            [SpedCampos(2, "UF_ID", "N", int.MaxValue, 0, true)]
            public int UFVeiculo { get; set; }
        }

        /// <summary>
        ///     REGISTRO D120: Complemento da Nota Fiscal de Serviços de Transporte  DO BLOCO D.
        /// </summary>
        public class RegistroD140 : RegistroBaseSped
        {
            /// <summary>
            ///<see cref="RegistroD140"/>.
            /// </summary>
            public RegistroD140()
            {
                Reg = "D140";
            }

            /// <summary>
            ///   Código do participante (campo 02 do Registro 0150):- consignatário, se houver.
            /// </summary>
            [SpedCampos(2, "COD_PART_CONSG", "N", int.MaxValue, 0, true)]
            public int CodigoParticipante { get; set; }

            /// <summary>
            ///    Código do participante (campo 02 do Registro 0150):- redespachado, se houver.
            /// </summary>
            [SpedCampos(2, "COD_PART_RED", "C", 7, 0, true)]
            public int CodigoParticipanteResdespacho { get; set; }

            /// <summary>
            ///    Indicador do tipo do frete da operação de redespacho:
                   //0: Sem redespacho;
                   //1: Por conta do emitente;
                   //2: Por conta do destinatário;
                   //9: Outros.
            /// </summary>
            [SpedCampos(3, "IND_FRT_RED", "C", int.MaxValue, 0, true)]
            public int IndicadorTipoFrete { get; set; }

            /// <summary>
            ///  Código do Município de origem do serviço, conforme a Tabela IBGE (Preencher com "9999999", se Exterior).
            /// </summary>
            [SpedCampos(4, "COD_MUN_ORIG", "N", int.MaxValue, 0, true)]
            public int CodigoMuniOrigem { get; set; }

            /// <summary>
            ///  Código do Município de destino, conforme a Tabela IBGE (Preencher com "9999999", se Exterior).
            /// </summary>
            [SpedCampos(5, "COD_MUN_DEST", "N", int.MaxValue, 0, true)]
            public int CodigoMuniDestino { get; set; }

            /// <summary>
            ///  Soma de valores de Sec/Cat (serviços de coleta/custo adicional de transporte).
            /// </summary>
            [SpedCampos(6, "VL_SEC_CAT", "N", int.MaxValue, 0, true)]
            public int SomaValorServ { get; set; }

            /// <summary>
            ///  Soma de valores de despacho.
            /// </summary>
            [SpedCampos(7, "VL_DESP", "N", int.MaxValue, 0, true)]
            public int SomaValorDesp { get; set; }


            /// <summary>
            ///  Soma dos valores de pedágio.
            /// </summary>
            [SpedCampos(8, "VL_PEDG", "N", int.MaxValue, 0, true)]
            public int SomaValorPed { get; set; }

            /// <summary>
            ///  Valor total do frete.
            /// </summary>
            [SpedCampos(9, "VL_FRT", "N", int.MaxValue, 0, true)]
            public int TotalFrete { get; set; }

            /// <summary>
            ///  Sigla da Unidade da Federação (UF) da placa do veículo.
            /// </summary>
            [SpedCampos(10, "UF_ID", "N", int.MaxValue, 0, true)]
            public int UfVeiculo { get; set; }
        }

        /// <summary>
        ///     REGISTRO D130: Complemento do Conhecimento Aquaviário de Cargas Avulso (Código 09) DO BLOCO D.
        /// </summary>
        public class RegistroD130 : RegistroBaseSped
        {
            /// <summary>
            /// <see cref="RegistroD130"/>.
            /// </summary>
            public RegistroD130()
            {
                Reg = "D130";
            }

            /// <summary>
            ///    Indicador do tipo da navegação:
                   //0: Interior;
                   //1: Cabotagem.
            /// </summary>
            [SpedCampos(2, "IND_NAV", "C", 7, 0, true)]
            public int TipoNavegação { get; set; }

            /// <summary>
            ///    Número da viagem.
            /// </summary>
            [SpedCampos(3, "VIAGEM", "C", int.MaxValue, 0, true)]
            public int NumViagem { get; set; }

            /// <summary>
            ///  Valor das despesas portuárias.
            /// </summary>
            [SpedCampos(4, "VL_DESP_PORT", "N", int.MaxValue, 0, true)]
            public int ValorDespPortuaria { get; set; }


            /// <summary>
            /// 	Valor das despesas com carga e descarga.
            /// </summary>
            [SpedCampos(5, "VL_DESP_CAR_DESC", "N", int.MaxValue, 0, true)]
            public int ValorCargaDescarga { get; set; }

            /// <summary>
            /// 	Valor do Adicional do Frete para Renovação da Marinha Mercante (AFRMM).
            /// </summary>
            [SpedCampos(6, "VL_FRT_MM", "N", int.MaxValue, 0, true)]
            public int ValorAdicionalFrete { get; set; }

        }


        /// <summary>
        ///     REGISTRO D130: Complemento do Conhecimento Aquaviário de Cargas Avulso (Código 09) DO BLOCO D.
        /// </summary>
        public class RegistroD150 : RegistroBaseSped
        {
            /// <summary>
            /// <see cref="RegistroD150"/>.
            /// </summary>
            public RegistroD150()
            {
                Reg = "D150";
            }

            /// <summary>
            ///    	Indicador do tipo de tarifa aplicada:
                   //0: Exp.;
                   //1: Enc.;
                   //2: C.I.;
                   //9: Outra.
            /// </summary>
            [SpedCampos(2, "IND_TFA", "C", 7, 0, true)]
            public int IndTipoTarifa { get; set; }

            /// <summary>
            ///   	Peso taxado.
            /// </summary>
            [SpedCampos(2, "VL_PESO_TX", "C", int.MaxValue, 0, true)]
            public int PesoTaxado { get; set; }

            /// <summary>
            /// Valor da taxa terrestre.
            /// </summary>
            [SpedCampos(2, "VL_TX_TERR", "N", int.MaxValue, 0, true)]
            public int ValorTaxaTerrestre { get; set; }


            /// <summary>
            /// Valor da taxa de redespacho.
            /// </summary>
            [SpedCampos(2, "VL_TX_RED", "N", int.MaxValue, 0, true)]
            public int ValorTaxaResp { get; set; }

            /// <summary>
            /// 	Valor da taxa "ad valorem".
            /// </summary>
            [SpedCampos(2, "VL_TX_ADV", "N", int.MaxValue, 0, true)]
            public int ValorTaxaAD { get; set; }

        }


        /// <summary>
        ///     REGISTRO D160: Carga transportada (Código 08, 8B, 09, 10, 11, 26 e 27) DO BLOCO D.
        /// </summary>
        public class RegistroD160 : RegistroBaseSped
        {
            /// <summary>
            /// <see cref="RegistroD160"/>.
            /// </summary>
            public RegistroD160()
            {
                Reg = "D160";
            }

            /// <summary>
            ///    Identificação do número do despacho.            
            /// </summary>
            [SpedCampos(2, "DESPACHO", "C", 7, 0, true)]
            public int Despacho { get; set; }

            /// <summary>
            ///   	CNPJ ou CPF do remetente das mercadorias que constam na Nota Fiscal.
            /// </summary>
            [SpedCampos(3, "CNPJ_CPF_REM", "C", int.MaxValue, 0, true)]
            public int CpfCnpjRemetente { get; set; }

            /// <summary>
            /// nscrição Estadual (IE) do remetente das mercadorias que constam na Nota Fiscal.
            /// </summary>
            [SpedCampos(4, "IE_REM", "N", int.MaxValue, 0, true)]
            public int InscricaoEstaRemet { get; set; }


            /// <summary>
            /// 	Código do Município de origem conforme Tabela IBGE (Preencher com "9999999", se Exterior).
            /// </summary>
            [SpedCampos(5, "COD_MUN_ORI", "N", int.MaxValue, 0, true)]
            public int CodMunicipioOrigem { get; set; }

            /// <summary>
            /// 	CNPJ ou CPF do destnatário das mercadorias que constam na Nota Fiscal.
            /// </summary>
            [SpedCampos(6, "CNPJ_CPF_DEST", "N", int.MaxValue, 0, true)]
            public int CnfCnpjDest { get; set; }

            /// <summary>
            /// 	Inscrição Estadual (IE) do destinatário das mercadorias que constam na Nota Fiscal.
            /// </summary>
            [SpedCampos(7, "IE_DEST", "N", int.MaxValue, 0, true)]
            public int InscrDestinatario { get; set; }

            /// <summary>
            /// Código do Município de destino, conforme Tabela IBGE (Preencher com "9999999", se Exterior).
            /// </summary>
            [SpedCampos(8, "COD_MUN_DEST", "N", int.MaxValue, 0, true)]
            public int CodMunDestino { get; set; }

        }

        /// <summary>
        ///     REGISTRO D190: REGISTRO ANALÍTICO DOS DOCUMENTOS (CÓDIGO 07, 08, 8B, 09, 10, 11, 26, 27 e 57).
        /// </summary>
        public class RegistroD190 : RegistroBaseSped
        {
            /// <summary>
            /// Inicializa uma nova instância da classe <see cref="RegistroD190"/>.
            /// </summary>
            public RegistroD190()
            {
                Reg = "D190";
            }

            /// <summary>
            ///     Código da Situação Tributária referente ao ICMS, conforme a Tabela indicada no item 4.3.1
            /// </summary>
            [SpedCampos(2, "CST_ICMS", "N", 3, 0, true)]
            public int CstIcms { get; set; }

            /// <summary>
            ///     Código Fiscal de Operação e Prestação, conforme a tabela indicada no item 4.2.2
            /// </summary>
            [SpedCampos(3, "CFOP", "N", 4, 0, true)]
            public int Cfop { get; set; }

            /// <summary>
            ///     Alíquota do ICMS
            /// </summary>
            [SpedCampos(4, "ALIQ_ICMS", "N", 6, 2, true)]
            public decimal AliqIcms { get; set; }

            /// <summary>
            ///     Valor da operação correspondente à combinação de CST_ICMS, CFOP, e alíquota do ICMS.
            /// </summary>
            [SpedCampos(5, "VL_OPR", "N", 0, 2, true)]
            public decimal VlOpr { get; set; }

            /// <summary>
            ///     Parcela correspondente ao "Valor da base de cálculo do ICMS" referente à combinação CST_ICMS, CFOP, e alíquota do
            ///     ICMS
            /// </summary>
            [SpedCampos(6, "VL_BC_ICMS", "N", 0, 2, true)]
            public decimal VlBcIcms { get; set; }

            /// <summary>
            ///     Parcela correspondente ao "Valor do ICMS" referente à combinação CST_ICMS,  CFOP e alíquota do ICMS
            /// </summary>
            [SpedCampos(7, "VL_ICMS", "N", 0, 2, true)]
            public decimal VlIcms { get; set; }

            /// <summary>
            ///     Valor não tributado em função da redução da base de cálculo do ICMS, referente à combinação de CST_ICMS, CFOP e
            ///     alíquota do ICMS.
            /// </summary>
            [SpedCampos(8, "VL_RED_BC", "N", 0, 2, true)]
            public decimal VlRedBc { get; set; }

            /// <summary>
            ///     Código da observação do lançamento fiscal (campo 02 do Registro 0460)
            /// </summary>
            [SpedCampos(9, "COD_OBS", "C", 6, 0, false)]
            public int CodObs { get; set; }
        }

        /// <summary>
        ///     REGISTRO D990: ENCERRAMENTO DO BLOCO D.
        /// </summary>
        public class RegistroD990: RegistroBaseSped
        {
            /// <summary>
            /// Inicializa uma nova instância da classe <see cref="RegistroD990"/>.
            /// </summary>
            public RegistroD990()
            {
                Reg = "D990";
            }

            /// <summary>
            ///     Quantidade total de linhas do Bloco D
            /// </summary>
            [SpedCampos(2, "QTD_LIN_D", "N", int.MaxValue, 0, true)]
            public int QtdLinD { get; set; }
        }
    }
}
