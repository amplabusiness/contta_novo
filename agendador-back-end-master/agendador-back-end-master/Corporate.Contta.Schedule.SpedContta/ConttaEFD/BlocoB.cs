using Contta.Common;
using System;

namespace Contta.SpedFiscal
{
    /// <summary>
    ///     BLOCO B: Escrituração e Apuração do ISS
    /// </summary>
    public class BlocoB
    {
        /// <summary>
        ///     REGISTRO B001: Abertura do Bloco B
        /// </summary>
        public class RegistroB001 : RegistroBaseSped
        {
            /// <summary>
            ///     Inicializa uma nova instância da classe <see cref="RegistroB001" />.
            /// </summary>
            public RegistroB001()
            {
                Reg = "B001";
            }

            /// <summary>
            ///     0 - Bloco com dados informados; 1 - Bloco sem dados informados.
            /// </summary>
            [SpedCampos(2, "IND_DAD", "N", 1, 0, true)]
            public IndMovimento IndDad { get; set; }
        }

        /// <summary>
        ///      REGISTRO B020: Registro B020 da EFD-ICMS/IPI (Sped-Fiscal)
        ///     - Nota Fiscal (Código 01), Nota Fiscal de serviços (Código 03), 
        ///     Nota Fiscal de serviços avulsa (Código 3B), Nota Fiscal de
        ///     produtor (Código 04), Conhecimento de Transporte Rodoviário 
        ///     de Cargas (Código 08), NF-e (Código 55) e NFC-e (Código 65)
        /// </summary>
        public class RegistroB020 : RegistroBaseSped
        {
            /// <summary>
            ///     Inicializa uma nova instância da classe <see cref="RegistroB020" />.
            /// </summary>
            public RegistroB020()
            {
                Reg = "B020";
            }

            /// <summary>
            ///     Indicador do tipo de operação:
            //0: Aquisição;
            //1: Prestação.
            /// </summary>
            [SpedCampos(2, "IND_OPER", "C", 1, 0, true)]
            public IndOper IndOper { get; set; }

            /// <summary>
            ///     Código do participante (campo 02 do Registro 0150):- do prestador, no caso de declarante na condição de tomador;
            ///     - do tomador, no caso de declarante na condição de prestador.
            /// </summary>
            [SpedCampos(3, "COD_PART", "C", int.MaxValue, 0, true)]
            public int CodParticipante { get; set; }

            /// <summary>
            ///   Código do modelo do documento fiscal, conforme a Tabela 4.1.3.
            /// </summary>
            [SpedCampos(4, "COD_MOD", "C", int.MaxValue, 0, true)]
            public int CodModeloFiscal { get; set; }

            /// <summary>
            ///  Código da situação do documento conforme Tabela 4.1.2.
            /// </summary>
            [SpedCampos(5, "COD_SIT", "N", int.MaxValue, 0, true)]
            public int CodSitDocu { get; set; }

            /// <summary>
            ///  Série do documento fiscal.
            /// </summary>
            [SpedCampos(6, "SER", "C", int.MaxValue, 0, true)]
            public int SerieDoc { get; set; }

            /// <summary>
            ///  Número do documento fiscal.
            /// </summary>
            [SpedCampos(7, "NUM_DOC", "N", int.MaxValue, 0, true)]
            public int NumeroDocum { get; set; }

            /// <summary>
            ///  Chave da Nota Fiscal Eletrônica.
            /// </summary>
            [SpedCampos(8, "CHV_NFE", "N", 60, 0, true)]
            public string ChvNfe { get; set; }

            /// <summary>
            ///  Data da emissão do documento fiscal.
            /// </summary>
            [SpedCampos(9, "DT_DOC", "N", 8, 0, true)]
            public DateTime DtDocumento { get; set; }

            /// <summary>
            ///  Código do município onde o serviço foi prestado, conforme a tabela IBGE.
            /// </summary>
            [SpedCampos(10, "COD_MUN_SERV", "N", 60, 0, true)]
            public string CodMuniciSever { get; set; }


            /// <summary>
            ///  Valor contábil (valor total do documento).
            /// </summary>
            [SpedCampos(11, "VL_CONT", "N", int.MaxValue, 0, true)]
            public int ValorCont { get; set; }

            /// <summary>
            ///  Valor do material fornecido por terceiros na prestação do serviço.
            /// </summary>
            [SpedCampos(12, "VL_MAT_TERC", "N", int.MaxValue, 0, true)]
            public int ValorMatTerceiros { get; set; }

            /// <summary>
            ///  Valor da subempreitada.
            /// </summary>
            [SpedCampos(13, "VL_SUB", "N", int.MaxValue, 0, true)]
            public int ValorSubempreita { get; set; }

            /// <summary>
            ///  Valor das operações isentas ou não-tributadas pelo ISS.
            /// </summary>
            [SpedCampos(14, "VL_ISNT_ISS", "N", int.MaxValue, 0, true)]
            public int ValorIsentaIsss { get; set; }

            /// <summary>
            /// Valor da dedução da base de cálculo.
            /// </summary>
            [SpedCampos(15, "VL_DED_BC", "N", int.MaxValue, 0, true)]
            public int ValorDedBaseCalcu { get; set; }

            /// <summary>
            /// Valor da base de cálculo do ISS.
            /// </summary>
            [SpedCampos(16, "VL_BC_ISS", "N", int.MaxValue, 0, true)]
            public int ValorBaseCalcu { get; set; }

            /// <summary>
            /// Valor da base de cálculo de retenção do ISS.
            /// </summary>
            [SpedCampos(17, "VL_BC_ISS_RT", "N", int.MaxValue, 0, true)]
            public int ValorBaseCalcuRet { get; set; }

            /// <summary>
            /// Valor do ISS retido pelo tomador.
            /// </summary>
            [SpedCampos(18, "VL_ISS_RT", "N", int.MaxValue, 0, true)]
            public int ValorRetTomador { get; set; }

            /// <summary>
            /// Valor do ISS destacado.
            /// </summary>
            [SpedCampos(19, "VL_ ISS", "N", int.MaxValue, 0, true)]
            public int ValorIssDest { get; set; }


            /// <summary>
            /// Código da observação do lançamento fiscal (campo 02 do Registro 0460).
            /// </summary>
            [SpedCampos(20, "COD_INF_OBS", "N", 60, 0, true)]
            public string CodLanFiscal { get; set; }

        }


        /// <summary>
        ///    Registro B025 da EFD-ICMS/IPI (Sped-Fiscal) - Detalhamento por combinação de alíquota 
        ///    e item da Lista de Serviços da LC 116/2003
        /// </summary>
        public class RegistroB025 : RegistroBaseSped
        {
            /// <summary>
            ///     Inicializa uma nova instância da classe <see cref="RegistroB025" />.
            /// </summary>
            public RegistroB025()
            {
                Reg = "B025";
            }

            /// <summary>
            ///     Parcela correspondente ao "Valor Contábil" referente à combinação da alíquota e item da lista.
            /// </summary>
            [SpedCampos(2, "VL_CONT_P", "N", int.MaxValue, 0, true)]
            public int ValorContParcelas { get; set; }

            /// <summary>
            ///     Parcela correspondente ao "Valor da base de cálculo do ISS" referente à combinação da alíquota e item da lista.
            /// </summary>
            [SpedCampos(3, "VL_BC_ISS_P", "N", int.MaxValue, 0, true)]
            public int ValorCalIss { get; set; }

            /// <summary>
            ///    Alíquota do ISS.
            /// </summary>
            [SpedCampos(4, "ALIQ_ISS", "N", int.MaxValue, 0, true)]
            public int AlinqIss { get; set; }

            /// <summary>
            ///  Parcela correspondente ao "Valor do ISS" referente à combinação da alíquota e item da lista.
            /// </summary>
            [SpedCampos(5, "VL_ISS_P", "N", int.MaxValue, 0, true)]
            public int ValorIss { get; set; }

            /// <summary>
            ///  Parcela correspondente ao "Valor das operações isentas ou não-tributadas pelo ISS"
            ///  referente à combinação da alíquota e item da lista.
            /// </summary>
            [SpedCampos(6, "VL_ISNT_ISS_P", "N", int.MaxValue, 0, true)]
            public int ValorOperInsentas { get; set; }

            /// <summary>
            /// Item da lista de serviços, conforme Tabela 4.6.3.
            /// </summary>
            [SpedCampos(7, "COD_SERV", "N", 4, 0, true)]
            public int CodServico { get; set; }
        }




        /// <summary>
        ///    Registro B030 da EFD-ICMS/IPI (Sped-Fiscal) - Nota Fiscal de serviços simplificada (Código 3A)
        /// </summary>
        public class RegistroB030 : RegistroBaseSped
        {
            /// <summary>
            ///     Inicializa uma nova instância da classe <see cref="RegistroB030" />.
            /// </summary>
            public RegistroB030()
            {
                Reg = "B030";
            }

            /// <summary>
            ///     Código do modelo do documento fiscal, conforme a Tabela 4.1.3.
            /// </summary>
            [SpedCampos(2, "COD_MOD", "C", 2, 0, true)]
            public string CodModeloFiscal { get; set; }

            /// <summary>
            ///  Quantidade de documentos cancelados.
            /// </summary>
            [SpedCampos(3, "QTD_CANC", "N", int.MaxValue, 0, true)]
            public int QtdCancelados { get; set; }          
        }


        /// <summary>
        ///    Registro B035 da EFD-ICMS/IPI (Sped-Fiscal) - 
        ///    Detalhamento por combinação de alíquota e item da Lista de Serviços da LC 116/2003
        /// </summary>
        public class RegistroB035 : RegistroBaseSped
        {
            /// <summary>
            ///     Inicializa uma nova instância da classe <see cref="RegistroB035" />.
            /// </summary>
            public RegistroB035()
            {
                Reg = "B030";
            }

            /// <summary>
            ///   Parcela correspondente ao "Valor Contábil" referente à combinação da alíquota e item da lista.
            /// </summary>
            [SpedCampos(2, "VL_CONT_P", "C", 2, 0, true)]
            public int VlContabelPar { get; set; }

            /// <summary>
            /// Parcela correspondente ao "Valor da base de cálculo do ISS" referente à combinação da alíquota e item da lista.
            /// </summary>
            [SpedCampos(3, "VL_BC_ISS_P", "N", int.MaxValue, 0, true)]
            public int VL_BC_ISS_P { get; set; }

            /// <summary>
            ///   	Alíquota do ISS.
            /// </summary>
            [SpedCampos(4, "ALIQ_ISS", "C", 2, 0, true)]
            public int ALIQ_ISS { get; set; }

            /// <summary>
            /// Parcela correspondente ao "Valor do ISS" referente à combinação da alíquota e item da lista.
            /// </summary>
            [SpedCampos(5, "VL_ISS_P", "N", int.MaxValue, 0, true)]
            public int VL_ISS_P { get; set; }

            /// <summary>
            ///   	Parcela correspondente ao "Valor das operações isentas ou nãotributadas pelo ISS" referente à combinação da alíquota e item da lista.
            /// </summary>
            [SpedCampos(6, "VL_ISNT_ISS_P", "C", 2, 0, true)]
            public int VL_ISNT_ISS_P { get; set; }

            /// <summary>
            /// Item da lista de serviços, conforme Tabela 4.6.3.
            /// </summary>
            [SpedCampos(7, "COD_SERV", "C", 4, 0, true)]
            public string COD_SERV { get; set; }
        }


        /// <summary>
        ///    Registro B050 da EFD-ICMS/IPI (Sped-Fiscal) - Serviços prestados por instituições financeiras
        /// </summary>
        public class RegistroB050 : RegistroBaseSped
        {
            /// <summary>
            ///     Inicializa uma nova instância da classe <see cref="RegistroB050" />.
            /// </summary>
            public RegistroB050()
            {
                Reg = "B050";
            }

        }

        /// <summary>
        ///   Registro B420 da EFD-ICMS/IPI (Sped-Fiscal) - 
        ///   Totalização dos valores de serviços prestados por combinação de alíquota e item da Lista se Serviços da LC 116/2003
        /// </summary>
        public class RegistroB0420 : RegistroBaseSped
        {
            /// <summary>
            ///     Inicializa uma nova instância da classe <see cref="RegistroB0420" />.
            /// </summary>
            public RegistroB0420()
            {
                Reg = "B0420";
            }

        }


        /// <summary>
        ///   Registro B440 da EFD-ICMS/IPI (Sped-Fiscal) - Totalização dos valores retidos
        /// </summary>
        public class RegistroB0440 : RegistroBaseSped
        {
            /// <summary>
            ///     Inicializa uma nova instância da classe <see cref="RegistroB0440" />.
            /// </summary>
            public RegistroB0440()
            {
                Reg = "B0440";
            }

        }

        /// <summary>
        ///   Registro B460 da EFD-ICMS/IPI (Sped-Fiscal) - Deduções do ISS.
        /// </summary>
        public class RegistroB0460 : RegistroBaseSped
        {
            /// <summary>
            ///     Inicializa uma nova instância da classe <see cref="RegistroB0460" />.
            /// </summary>
            public RegistroB0460()
            {
                Reg = "B0460";
            }

        }

        /// <summary>
        ///   Registro B470 da EFD-ICMS/IPI (Sped-Fiscal) - Apuração do ISS
        /// </summary>
        public class RegistroB0470 : RegistroBaseSped
        {
            /// <summary>
            ///     Inicializa uma nova instância da classe <see cref="RegistroB0470" />.
            /// </summary>
            public RegistroB0470()
            {
                Reg = "B0470";
            }

        }

        /// <summary>
        ///   Registro B500 da EFD-ICMS/IPI (Sped-Fiscal) - Apuração do ISS sociedade unipessoal
        /// </summary>
        public class RegistroB0500 : RegistroBaseSped
        {
            /// <summary>
            ///     Inicializa uma nova instância da classe <see cref="RegistroB0500" />.
            /// </summary>
            public RegistroB0500()
            {
                Reg = "B0500";
            }

        }

        /// <summary>
        ///  Registro B510 da EFD-ICMS/IPI (Sped-Fiscal) - Uniprofissional - Empregados e sócios
        /// </summary>
        public class RegistroB0510 : RegistroBaseSped
        {
            /// <summary>
            ///     Inicializa uma nova instância da classe <see cref="RegistroB0510" />.
            /// </summary>
            public RegistroB0510()
            {
                Reg = "B0510";
            }

        }
    }
}
