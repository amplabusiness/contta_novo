using System;
using Contta.Common;

namespace Contta.EfdContribuicoes
{
    public class BlocoM
    {
        public class RegistroM001 : RegistroBaseSped
        {
            public RegistroM001()
            {
                Reg = "M001";
            }

            [SpedCampos(2, "IND_MOV", "C", 1, 0, true)]
            public int IndMov { get; set; }
        }

        public class RegistroM100 : RegistroBaseSped
        {
            public RegistroM100()
            {
                Reg = "M100";
            }

            //Código de Tipo de Crédito apurado no período, conforme a Tabela 4.3.6.
            [SpedCampos(2, "COD_CRED", "C", 4, 2, true)]
            public string COD_CRED { get; set; }

            //	Indicador de Crédito Oriundo de:
            //0 – Operações próprias;
            //1 – Evento de incorporação, cisão ou fusão.
            [SpedCampos(3, "IND_CRED_ORI", "C", 3, 2, true)]
            public string IND_CRED_ORI { get; set; }

            //Valor da Base de Cálculo do Crédito
            [SpedCampos(4, "VL_BC_PIS", "N", 0, 2, true)]
            public decimal VL_BC_PIS { get; set; }

            //Alíquota do PIS/PASEP (em percentual)
            [SpedCampos(5, "ALIQ_PIS", "N", 0, 2, true)]
            public decimal ALIQ_PIS { get; set; }

            //Quantidade – Base de cálculo PIS
            [SpedCampos(6, "QUANT_BC_PIS", "N", 0, 2, true)]
            public decimal QUANT_BC_PIS { get; set; }

            //Alíquota do PIS (em reais)
            [SpedCampos(7, "ALIQ_PIS_QUANT", "N", 0, 2, true)]
            public decimal ALIQ_PIS_QUANT { get; set; }

            //Valor total do crédito apurado no período
            [SpedCampos(8, "VL_CRED", "N", 0, 2, true)]
            public decimal VL_CRED { get; set; }

            //Valor total dos ajustes de acréscimo
            [SpedCampos(9, "VL_AJUS_ACRES", "N", 0, 2, true)]
            public decimal VL_AJUS_ACRES { get; set; }

            //Valor total dos ajustes de redução
            [SpedCampos(10, "VL_AJUS_REDUC", "N", 0, 2, true)]
            public decimal VL_AJUS_REDUC { get; set; }

            //Valor total do crédito diferido no período
            [SpedCampos(11, "VL_CRED_DIF", "N", 0, 2, true)]
            public decimal VL_CRED_DIF { get; set; }

            //Valor Total do Crédito Disponível relativo ao Período (08 + 09 – 10 – 11)
            [SpedCampos(12, "VL_CRED_DISP", "N", 0, 2, true)]
            public decimal VL_CRED_DISP { get; set; }

            //            Indicador de opção de utilização do crédito disponível no período:
            //0 – Utilização do valor total para desconto da contribuição apurada no período, no Registro M200;
            //1 – Utilização de valor parcial para desconto da contribuição apurada no período, no Registro M200.
            [SpedCampos(13, "IND_DESC_CRED", "N", 0, 2, true)]
            public decimal IND_DESC_CRED { get; set; }

            //Valor do Crédito disponível, descontado da contribuição apurada no próprio período.
            //Se IND_DESC_CRED= 0, informar o valor total do Campo 12;
            //Se IND_DESC_CRED = 1, informar o valor parcial do Campo 12.
            [SpedCampos(14, "VL_CRED_DESC", "N", 0, 2, true)]
            public decimal VL_CRED_DESC { get; set; }

            //Saldo de créditos a utilizar em períodos futuros (12 – 14)
            [SpedCampos(15, "SLD_CRED", "N", 0, 2, true)]
            public decimal SLD_CRED { get; set; }

        }


        public class RegistroM200 : RegistroBaseSped
        {
            public RegistroM200()
            {
                Reg = "M200";
            }

            [SpedCampos(2, "VL_TOT_CONT_NC_PER", "N", 0, 2, true)]
            public decimal VlTotContNcPer { get; set; }

            [SpedCampos(3, "VL_TOT_CRED_DESC", "N", 0, 2, true)]
            public decimal VlTotCredDesc { get; set; }

            [SpedCampos(4, "VL_TOT_CRED_DESC_ANT", "N", 0, 2, true)]
            public decimal VlTotCredDescAnt { get; set; }

            [SpedCampos(5, "VL_TOT_CONT_NC_DEV", "N", 0, 2, true)]
            public decimal VlTotContNcDev { get; set; }

            [SpedCampos(6, "VL_RET_NC", "N", 0, 2, true)]
            public decimal VlRetNc { get; set; }

            [SpedCampos(7, "VL_OUT_DED_NC", "N", 0, 2, true)]
            public decimal VlOutDedNc { get; set; }

            [SpedCampos(8, "VL_CONT_NC_REC", "N", 0, 2, true)]
            public decimal VlContNcRec { get; set; }

            [SpedCampos(9, "VL_TOT_CONT_CUM_PER", "N", 0, 2, true)]
            public decimal VlTotContCumPer { get; set; }

            [SpedCampos(10, "VL_RET_CUM", "N", 0, 2, true)]
            public decimal VlRetCum { get; set; }

            [SpedCampos(11, "VL_OUT_DED_CUM", "N", 0, 2, true)]
            public decimal VlOutDedCum { get; set; }

            [SpedCampos(12, "VL_CONT_CUM_REC", "N", 0, 2, true)]
            public decimal VlContCumRec { get; set; }

            [SpedCampos(13, "VL_TOT_CONT_REC", "N", 0, 2, true)]
            public decimal VlTotContRec { get; set; }
        }

        public class RegistroM205 : RegistroBaseSped
        {
            public RegistroM205()
            {
                Reg = "M205";
            }

            [SpedCampos(2, "NUM_CAMPO", "C", 2, 0, true)]
            public int NumCampo { get; set; }

            [SpedCampos(3, "COD_REC", "C", 6, 0, true)]
            public int CodRec { get; set; }

            [SpedCampos(4, "VL_DEBITO", "N", 0, 2, true)]
            public decimal VlDebito { get; set; }
        }

        public class RegistroM210 : RegistroBaseSped
        {
            public RegistroM210()
            {
                Reg = "M210";
            }

            [SpedCampos(2, "COD_CONT", "C", 2, 0, true)]
            public int CodCont { get; set; }

            [SpedCampos(3, "VL_REC_BRT", "N", 0, 2, true)]
            public decimal VlRecBrt { get; set; }

            [SpedCampos(4, "VL_BC_CONT", "N", 0, 2, true)]
            public decimal VlBcCont { get; set; }

            [SpedCampos(5, "ALIQ_PIS", "N", 8, 4, false)]
            public decimal? AliqPis { get; set; }

            [SpedCampos(6, "QUANT_BC_PIS", "N", 0, 3, false)]
            public decimal? QuantBcPis { get; set; }

            [SpedCampos(7, "ALIQ_PIS_QUANT", "N", 0, 4, false)]
            public decimal? AliqPisQuant { get; set; }

            [SpedCampos(8, "VL_CONT_APUR", "N", 0, 2, true)]
            public decimal VlContApur { get; set; }

            [SpedCampos(9, "VL_AJUS_ACRES", "N", 0, 2, true)]
            public decimal VlAjusAcres { get; set; }

            [SpedCampos(10, "VL_AJUS_REDUC", "N", 0, 2, true)]
            public decimal VlAjusReduc { get; set; }

            [SpedCampos(11, "VL_CONT_DIFER", "N", 0, 2, false)]
            public decimal? VlContDifer { get; set; }

            [SpedCampos(12, "VL_CONT_DIFER_ANT", "N", 0, 2, false)]
            public decimal? VlContDiferAnt { get; set; }

            [SpedCampos(13, "VL_CONT_PER", "N", 0, 2, true)]
            public decimal VlContPer { get; set; }
        }

        public class RegistroM220 : RegistroBaseSped
        {
            public RegistroM220()
            {
                Reg = "M220";
            }

            [SpedCampos(2, "IND_AJ", "C", 1, 0, true)]
            public int IndAj { get; set; }

            [SpedCampos(3, "VL_AJ", "N", 0, 2, true)]
            public decimal VlAj { get; set; }

            [SpedCampos(4, "COD_AJ", "C", 2, 0, true)]
            public int CodAj { get; set; }

            [SpedCampos(5, "NUM_DOC", "C", 0, 0, false)]
            public string NumDoc { get; set; }

            [SpedCampos(6, "DESCR_AJ", "C", 0, 0, false)]
            public string DescrAj { get; set; }

            [SpedCampos(7, "DT_REF", "N", 8, 0, false)]
            public DateTime? DtRef { get; set; }
        }

        [SpedRegistros("01/10/2015", null)]
        public class RegistroM225 : RegistroBaseSped
        {
            public RegistroM225()
            {
                Reg = "M225";
            }

            [SpedCampos(2, "DET_VALOR_AJ", "N", 0, 2, true)]
            public decimal DetValorAj { get; set; }

            [SpedCampos(3, "CST_PIS", "N", 2, 0, false)]
            public int CstPis { get; set; }

            [SpedCampos(4, "DET_BC_CRED", "N", 0, 3, false)]
            public decimal? DetBcCred { get; set; }

            [SpedCampos(5, "DET_ALIQ", "N", 8, 4, false)]
            public decimal? DetAliq { get; set; }

            [SpedCampos(6, "DT_OPER_AJ", "N", 8, 0, true)]
            public DateTime DtOperAj { get; set; }

            [SpedCampos(7, "DESC_AJ", "C", 0, 0, false)]
            public string DescAj { get; set; }

            [SpedCampos(8, "COD_CTA", "C", 60, 0, false)]
            public string CodCta { get; set; }

            [SpedCampos(9, "INFO_COMPL", "C", 0, 0, false)]
            public string InfoCompl { get; set; }
        }

        public class RegistroM400 : RegistroBaseSped
        {
            public RegistroM400()
            {
                Reg = "M400";
            }

            //Código de Situação Tributária – CST das demais receitas auferidas no período,
            //sem incidência da contribuição, ou sem contribuição apurada a pagar, conforme a Tabela 4.3.3.
            [SpedCampos(2, "CST_PIS", "C", 4, 0, false)]
            public string CST_PIS { get; set; }

            //Valor total da receita bruta no período.
            [SpedCampos(3, "VL_TOT_REC", "C", 60, 0, false)]
            public decimal VL_TOT_REC { get; set; }

            //Código da conta analítica contábil debitada/creditada.
            [SpedCampos(4, "COD_CTA", "C", 255, 0, false)]
            public string COD_CTA { get; set; }

            //Descrição Complementar da Natureza da Receita.
            [SpedCampos(5, "DESC_COMP", "C", 0, 0, false)]
            public decimal DESC_COMP { get; set; }

        }



        public class RegistroM500 : RegistroBaseSped
        {
            public RegistroM500()
            {
                Reg = "M500";
            }

            //Código de Tipo de Crédito apurado no período, conforme a Tabela 4.3.6.
            [SpedCampos(2, "COD_CRED", "C", 3, 0, true)]
            public string COD_CRED { get; set; }

            //            //	Indicador de Crédito Oriundo de:
            //0 – Operações próprias;
            //1 – Evento de incorporação, cisão ou fusão.
            [SpedCampos(3, "IND_CRED_ORI", "N", 0, 2, true)]
            public decimal IND_CRED_ORI { get; set; }

            //Valor da Base de Cálculo do Crédito.
            [SpedCampos(4, "VL_BC_COFINS", "N", 0, 2, true)]
            public decimal VL_BC_COFINS { get; set; }

            //Alíquota da COFINS (em percentual).
            [SpedCampos(5, "ALIQ_COFINS", "N", 8, 4, false)]
            public decimal ALIQ_COFINS { get; set; }

            //Quantidade – Base de cálculo COFINS.
            [SpedCampos(6, "QUANT_BC_COFINS", "N", 0, 3, false)]
            public decimal QUANT_BC_COFINS { get; set; }

            //Alíquota da COFINS (em reais).
            [SpedCampos(7, "ALIQ_COFINS_QUANT", "N", 0, 4, false)]
            public decimal ALIQ_COFINS_QUANT { get; set; }

            //Valor total do crédito apurado no período.
            [SpedCampos(8, "VL_CRED", "N", 0, 2, true)]
            public decimal VL_CRED { get; set; }

            //Valor total dos ajustes de acréscimo.
            [SpedCampos(9, "VL_AJUS_ACRES", "N", 0, 2, true)]
            public decimal VL_AJUS_ACRES { get; set; }

            //Valor total dos ajustes de redução.
            [SpedCampos(10, "VL_AJUS_REDUC", "N", 0, 2, true)]
            public decimal VL_AJUS_REDUC { get; set; }

            //Valor total do crédito diferido no período.
            [SpedCampos(11, "VL_CRED_DIFER", "N", 0, 2, false)]
            public decimal VL_CRED_DIFER { get; set; }

            //Valor Total do Crédito Disponível relativo ao Período (08 + 09 – 10 – 11).
            [SpedCampos(12, "VL_CRED_DISP", "N", 0, 2, false)]
            public decimal VL_CRED_DISP { get; set; }

            //            Indicador de utilização do crédito disponível no período:
            //0 – Utilização do valor total para desconto da contribuição apurada no período, no Registro M600;
            //1 – Utilização de valor parcial para desconto da contribuição apurada no período, no Registro M600.
            [SpedCampos(13, "IND_DESC_CRED", "C", 1, 2, true)]
            public string IND_DESC_CRED { get; set; }

            //Valor do Crédito disponível, descontado da contribuição apurada no próprio período.
            //Se IND_DESC_CRED= 0, informar o valor total do Campo 12;
            //Se IND_DESC_CRED = 1, informar o valor parcial do Campo 12.
            [SpedCampos(14, "VL_CRED_DESC", "N", 0, 2, true)]
            public decimal VL_CRED_DESC { get; set; }

            //Aldo de créditos a utilizar em períodos futuros (12 – 14).
            [SpedCampos(15, "SLD_CRED", "N", 0, 2, true)]
            public decimal SLD_CRED { get; set; }
        }



        public class RegistroM600 : RegistroBaseSped
        {
            public RegistroM600()
            {
                Reg = "M600";
            }

            [SpedCampos(2, "VL_TOT_CONT_NC_PER", "N", 0, 2, true)]
            public decimal VlTotContNcPer { get; set; }

            [SpedCampos(3, "VL_TOT_CRED_DESC", "N", 0, 2, true)]
            public decimal VlTotCredDesc { get; set; }

            [SpedCampos(4, "VL_TOT_CRED_DESC_ANT", "N", 0, 2, true)]
            public decimal VlTotCredDescAnt { get; set; }

            [SpedCampos(5, "VL_TOT_CONT_NC_DEV", "N", 0, 2, true)]
            public decimal VlTotContNcDev { get; set; }

            [SpedCampos(6, "VL_RET_NC", "N", 0, 2, true)]
            public decimal VlRetNc { get; set; }

            [SpedCampos(7, "VL_OUT_DED_NC", "N", 0, 2, true)]
            public decimal VlOutDedNc { get; set; }

            [SpedCampos(8, "VL_CONT_NC_REC", "N", 0, 2, true)]
            public decimal VlContNcRec { get; set; }

            [SpedCampos(9, "VL_TOT_CONT_CUM_PER", "N", 0, 2, true)]
            public decimal VlTotContCumPer { get; set; }

            [SpedCampos(10, "VL_RET_CUM", "N", 0, 2, true)]
            public decimal VlRetCum { get; set; }

            [SpedCampos(11, "VL_OUT_DED_CUM", "N", 0, 2, true)]
            public decimal VlOutDedCum { get; set; }

            [SpedCampos(12, "VL_CONT_CUM_REC", "N", 0, 2, true)]
            public decimal VlContCumRec { get; set; }

            [SpedCampos(13, "VL_TOT_CONT_REC", "N", 0, 2, true)]
            public decimal VlTotContRec { get; set; }
        }

        public class RegistroM605 : RegistroBaseSped
        {
            public RegistroM605()
            {
                Reg = "M605";
            }

            [SpedCampos(2, "NUM_CAMPO", "C", 2, 0, true)]
            public int NumCampo { get; set; }

            [SpedCampos(3, "COD_REC", "C", 6, 0, true)]
            public int CodRec { get; set; }

            [SpedCampos(4, "VL_DEBITO", "N", 0, 2, true)]
            public decimal VlDebito { get; set; }
        }

        public class RegistroM610 : RegistroBaseSped
        {
            public RegistroM610()
            {
                Reg = "M610";
            }

            [SpedCampos(2, "COD_CONT", "C", 2, 0, true)]
            public int CodCont { get; set; }

            [SpedCampos(3, "VL_REC_BRT", "N", 0, 2, true)]
            public decimal VlRecBrt { get; set; }

            [SpedCampos(4, "VL_BC_CONT", "N", 0, 2, true)]
            public decimal VlBcCont { get; set; }

            [SpedCampos(5, "ALIQ_COFINS", "N", 8, 4, false)]
            public decimal? AliqCofins { get; set; }

            [SpedCampos(6, "QUANT_BC_COFINS", "N", 0, 3, false)]
            public decimal? QuantBcCofins { get; set; }

            [SpedCampos(7, "ALIQ_COFINS_QUANT", "N", 0, 4, false)]
            public decimal? AliqCofinsQuant { get; set; }

            [SpedCampos(8, "VL_CONT_APUR", "N", 0, 2, true)]
            public decimal VlContApur { get; set; }

            [SpedCampos(9, "VL_AJUS_ACRES", "N", 0, 2, true)]
            public decimal VlAjusAcres { get; set; }

            [SpedCampos(10, "VL_AJUS_REDUC", "N", 0, 2, true)]
            public decimal VlAjusReduc { get; set; }

            [SpedCampos(11, "VL_CONT_DIFER", "N", 0, 2, false)]
            public decimal? VlContDifer { get; set; }

            [SpedCampos(12, "VL_CONT_DIFER_ANT", "N", 0, 2, false)]
            public decimal? VlContDiferAnt { get; set; }

            [SpedCampos(13, "VL_CONT_PER", "N", 0, 2, true)]
            public decimal VlContPer { get; set; }
        }

        public class RegistroM620 : RegistroBaseSped
        {
            public RegistroM620()
            {
                Reg = "M620";
            }

            [SpedCampos(2, "IND_AJ", "C", 1, 0, true)]
            public int IndAj { get; set; }

            [SpedCampos(3, "VL_AJ", "N", 0, 2, true)]
            public decimal VlAj { get; set; }

            [SpedCampos(4, "COD_AJ", "C", 2, 0, true)]
            public int CodAj { get; set; }

            [SpedCampos(5, "NUM_DOC", "C", 0, 0, false)]
            public string NumDoc { get; set; }

            [SpedCampos(6, "DESCR_AJ", "C", 0, 0, false)]
            public string DescrAj { get; set; }

            [SpedCampos(7, "DT_REF", "N", 8, 0, false)]
            public DateTime? DtRef { get; set; }
        }

        [SpedRegistros("01/10/2015", null)]
        public class RegistroM625 : RegistroBaseSped
        {
            public RegistroM625()
            {
                Reg = "M625";
            }

            [SpedCampos(2, "DET_VALOR_AJ", "N", 0, 2, true)]
            public decimal DetValorAj { get; set; }

            [SpedCampos(3, "CST_COFINS", "N", 2, 0, false)]
            public int CstCofins { get; set; }

            [SpedCampos(4, "DET_BC_CRED", "N", 0, 3, false)]
            public decimal? DetBcCred { get; set; }

            [SpedCampos(5, "DET_ALIQ", "N", 8, 4, false)]
            public decimal? DetAliq { get; set; }

            [SpedCampos(6, "DT_OPER_AJ", "N", 8, 0, true)]
            public DateTime DtOperAj { get; set; }

            [SpedCampos(7, "DESC_AJ", "C", 0, 0, false)]
            public string DescAj { get; set; }

            [SpedCampos(8, "COD_CTA", "C", 60, 0, false)]
            public string CodCta { get; set; }

            [SpedCampos(9, "INFO_COMPL", "C", 0, 0, false)]
            public string InfoCompl { get; set; }
        }


        public class RegistroM800 : RegistroBaseSped
        {
            public RegistroM800()
            {
                Reg = "M800";
            }

            //Código de Situação Tributária – CST das demais receitas auferidas no período,
            //sem incidência da contribuição, ou sem contribuição apurada a pagar, conforme a Tabela 4.3.4.
            [SpedCampos(2, "CST_COFINS", "C", 2, 0, true)]
            public string CST_COFINS { get; set; }

            //Valor total da receita bruta no período.
            [SpedCampos(3, "VL_TOT_REC", "N", 6, 0, true)]
            public int VL_TOT_REC { get; set; }

            //Código da conta analítica contábil debitada/creditada.
            [SpedCampos(4, "COD_CTA", "C", 255, 2, true)]
            public decimal COD_CTA { get; set; }

            //Descrição Complementar da Natureza da Receita.
            [SpedCampos(4, "DESC_COMPL", "C", 0, 2, true)]
            public decimal DESC_COMPL { get; set; }
        }

        public class RegistroM990 : RegistroBaseSped
        {
            public RegistroM990()
            {
                Reg = "M990";
            }

            [SpedCampos(2, "QTD_LIN_M", "N", 0, 0, true)]
            public int QtdLinM { get; set; }
        }
    }
}
