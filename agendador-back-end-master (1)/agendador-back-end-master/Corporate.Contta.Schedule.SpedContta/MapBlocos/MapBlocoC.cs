using Contta.Common;
using Contta.SpedFiscal;
using MongoDB.Driver;
using Corporate.Contta.Schedule.SpedContta.Base;
using Corporate.Contta.Schedule.SpedContta.Contex.NfeDTOs;
using Corporate.Contta.Schedule.SpedContta.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;

namespace Corporate.Contta.Schedule.SpedContta.MapBlocos
{
    public class MapBlocoC
    {
        private EmpresaEmitCollection DbEmpresaEmit = new EmpresaEmitCollection();
        private EmpresaDestCollection DbEmpresaDest = new EmpresaDestCollection();
        private NfeCollection DbNfe = new NfeCollection();
        private ProdutosCollection DbProdutos = new ProdutosCollection();
        private ImpostosCollection DbImpostos = new ImpostosCollection();

        public void GetBlocoC()
        {
            IMongoClient mongoClient = new MongoClient("mongodb+srv://thiago:thiago@agendador.f65ge.mongodb.net/conttadb?connect=replicaSet&retryWrites=true&w=majority");
            IMongoDatabase database = mongoClient.GetDatabase("ConttaSistemas");
            IMongoCollection<BlocoC.RegistroC001> tabelaC01 = database.GetCollection<BlocoC.RegistroC001>("registroC001");
            IMongoCollection<BlocoC.RegistroC100> tabelaC02 = database.GetCollection<BlocoC.RegistroC100>("registroC100");
            IMongoCollection<BlocoC.RegistroC101> tabelaC03 = database.GetCollection<BlocoC.RegistroC101>("registroC101");
            IMongoCollection<BlocoC.RegistroC105> tabelaC04 = database.GetCollection<BlocoC.RegistroC105>("registroC105");
            IMongoCollection<BlocoC.RegistroC110> tabelaC05 = database.GetCollection<BlocoC.RegistroC110>("registroC110");
            IMongoCollection<BlocoC.RegistroC111> tabelaC06 = database.GetCollection<BlocoC.RegistroC111>("registroC111");
            IMongoCollection<BlocoC.RegistroC112> tabelaC07 = database.GetCollection<BlocoC.RegistroC112>("registroC112");
            IMongoCollection<BlocoC.RegistroC113> tabelaC08 = database.GetCollection<BlocoC.RegistroC113>("registroC113");
            IMongoCollection<BlocoC.RegistroC114> tabelaC09 = database.GetCollection<BlocoC.RegistroC114>("registroC114");
            IMongoCollection<BlocoC.RegistroC115> tabelaC10 = database.GetCollection<BlocoC.RegistroC115>("registroC115");
            IMongoCollection<BlocoC.RegistroC116> tabelaC11 = database.GetCollection<BlocoC.RegistroC116>("registroC116");
            IMongoCollection<BlocoC.RegistroC120> tabelaC12 = database.GetCollection<BlocoC.RegistroC120>("registroC120");
            IMongoCollection<BlocoC.RegistroC130> tabelaC13 = database.GetCollection<BlocoC.RegistroC130>("registroC130");
            IMongoCollection<BlocoC.RegistroC140> tabelaC14 = database.GetCollection<BlocoC.RegistroC140>("registroC140");
            IMongoCollection<BlocoC.RegistroC141> tabelaC15 = database.GetCollection<BlocoC.RegistroC141>("registroC141");
            IMongoCollection<BlocoC.RegistroC160> tabelaC16 = database.GetCollection<BlocoC.RegistroC160>("registroC160");
            IMongoCollection<BlocoC.RegistroC165> tabelaC17 = database.GetCollection<BlocoC.RegistroC165>("registroC165");
            IMongoCollection<BlocoC.RegistroC170> tabelaC18 = database.GetCollection<BlocoC.RegistroC170>("registroC170");
            IMongoCollection<BlocoC.RegistroC171> tabelaC19 = database.GetCollection<BlocoC.RegistroC171>("registroC171");
            IMongoCollection<BlocoC.RegistroC172> tabelaC20 = database.GetCollection<BlocoC.RegistroC172>("registroC172");
            IMongoCollection<BlocoC.RegistroC173> tabelaC21 = database.GetCollection<BlocoC.RegistroC173>("registroC173");
            IMongoCollection<BlocoC.RegistroC174> tabelaC22 = database.GetCollection<BlocoC.RegistroC174>("registroC174");
            IMongoCollection<BlocoC.RegistroC175> tabelaC23 = database.GetCollection<BlocoC.RegistroC175>("registroC175");
            IMongoCollection<BlocoC.RegistroC176> tabelaC24 = database.GetCollection<BlocoC.RegistroC176>("registroC176");
            IMongoCollection<BlocoC.RegistroC177> tabelaC25 = database.GetCollection<BlocoC.RegistroC177>("registroC177");
            IMongoCollection<BlocoC.RegistroC178> tabelaC26 = database.GetCollection<BlocoC.RegistroC178>("registroC178");
            IMongoCollection<BlocoC.RegistroC179> tabelaC27 = database.GetCollection<BlocoC.RegistroC179>("registroC179");
            IMongoCollection<BlocoC.RegistroC190> tabelaC28 = database.GetCollection<BlocoC.RegistroC190>("registroC190");
            IMongoCollection<BlocoC.RegistroC195> tabelaC29 = database.GetCollection<BlocoC.RegistroC195>("registroC195");
            IMongoCollection<BlocoC.RegistroC300> tabelaC30 = database.GetCollection<BlocoC.RegistroC300>("registroC300");
            IMongoCollection<BlocoC.RegistroC310> tabelaC31 = database.GetCollection<BlocoC.RegistroC310>("registroC310");
            IMongoCollection<BlocoC.RegistroC321> tabelaC32 = database.GetCollection<BlocoC.RegistroC321>("registroC321");
            IMongoCollection<BlocoC.RegistroC350> tabelaC33 = database.GetCollection<BlocoC.RegistroC350>("registroC350");
            IMongoCollection<BlocoC.RegistroC370> tabelaC34 = database.GetCollection<BlocoC.RegistroC370>("registroC370");
            IMongoCollection<BlocoC.RegistroC390> tabelaC35 = database.GetCollection<BlocoC.RegistroC390>("registroC390");
            IMongoCollection<BlocoC.RegistroC400> tabelaC36 = database.GetCollection<BlocoC.RegistroC400>("registroC400");
            IMongoCollection<BlocoC.RegistroC405> tabelaC37 = database.GetCollection<BlocoC.RegistroC405>("registroC405");
            IMongoCollection<BlocoC.RegistroC410> tabelaC38 = database.GetCollection<BlocoC.RegistroC410>("registroC410");
            IMongoCollection<BlocoC.RegistroC420> tabelaC39 = database.GetCollection<BlocoC.RegistroC420>("registroC420");
            IMongoCollection<BlocoC.RegistroC425> tabelaC40 = database.GetCollection<BlocoC.RegistroC425>("registroC425");
            IMongoCollection<BlocoC.RegistroC460> tabelaC41 = database.GetCollection<BlocoC.RegistroC460>("registroC460");
            IMongoCollection<BlocoC.RegistroC465> tabelaCC41 = database.GetCollection<BlocoC.RegistroC465>("registroC465");
            IMongoCollection<BlocoC.RegistroC470> tabelaC42 = database.GetCollection<BlocoC.RegistroC470>("registroC470");
            IMongoCollection<BlocoC.RegistroC490> tabelaC43 = database.GetCollection<BlocoC.RegistroC490>("registroC490");
            IMongoCollection<BlocoC.RegistroC495> tabelaC44 = database.GetCollection<BlocoC.RegistroC495>("registroC495");
            IMongoCollection<BlocoC.RegistroC500> tabelaC45 = database.GetCollection<BlocoC.RegistroC500>("registroC500");
            IMongoCollection<BlocoC.RegistroC510> tabelaC46 = database.GetCollection<BlocoC.RegistroC510>("registroC510");
            IMongoCollection<BlocoC.RegistroC590> tabelaC47 = database.GetCollection<BlocoC.RegistroC590>("registroC590");
            IMongoCollection<BlocoC.RegistroC600> tabelaC48 = database.GetCollection<BlocoC.RegistroC600>("registroC600");
            IMongoCollection<BlocoC.RegistroC601> tabelaC49 = database.GetCollection<BlocoC.RegistroC601>("registroC601");
            IMongoCollection<BlocoC.RegistroC610> tabelaC50 = database.GetCollection<BlocoC.RegistroC610>("registroC610");
            IMongoCollection<BlocoC.RegistroC690> tabelaC51 = database.GetCollection<BlocoC.RegistroC690>("registroC690");
            IMongoCollection<BlocoC.RegistroC700> tabelaC52 = database.GetCollection<BlocoC.RegistroC700>("registroC700");
            IMongoCollection<BlocoC.RegistroC790> tabelaC53 = database.GetCollection<BlocoC.RegistroC790>("registroC790");
            IMongoCollection<BlocoC.RegistroC791> tabelaC54 = database.GetCollection<BlocoC.RegistroC791>("registroC791");
            IMongoCollection<BlocoC.RegistroC800> tabelaC55 = database.GetCollection<BlocoC.RegistroC800>("registroC800");
            IMongoCollection<BlocoC.RegistroC850> tabelaC56 = database.GetCollection<BlocoC.RegistroC850>("registroC850");
            IMongoCollection<BlocoC.RegistroC860> tabelaC57 = database.GetCollection<BlocoC.RegistroC860>("registroC860");
            IMongoCollection<BlocoC.RegistroC890> tabelaC58 = database.GetCollection<BlocoC.RegistroC890>("registroC890");
            IMongoCollection<BlocoC.RegistroC990> tabelaC59 = database.GetCollection<BlocoC.RegistroC990>("registroC990");

            BlocoC.RegistroC001 registroC001 = new BlocoC.RegistroC001();
            BlocoC.RegistroC100 registroC100 = new BlocoC.RegistroC100();
            BlocoC.RegistroC101 registroC101 = new BlocoC.RegistroC101();
            BlocoC.RegistroC105 registroC105 = new BlocoC.RegistroC105();
            BlocoC.RegistroC110 registroC110 = new BlocoC.RegistroC110();
            BlocoC.RegistroC111 registroC111 = new BlocoC.RegistroC111();
            BlocoC.RegistroC112 registroC112 = new BlocoC.RegistroC112();
            BlocoC.RegistroC113 registroC113 = new BlocoC.RegistroC113();
            BlocoC.RegistroC114 registroC114 = new BlocoC.RegistroC114();
            BlocoC.RegistroC115 registroC115 = new BlocoC.RegistroC115();
            BlocoC.RegistroC116 registroC116 = new BlocoC.RegistroC116();
            BlocoC.RegistroC120 registroC120 = new BlocoC.RegistroC120();
            BlocoC.RegistroC130 registroC130 = new BlocoC.RegistroC130();
            BlocoC.RegistroC140 registroC140 = new BlocoC.RegistroC140();
            BlocoC.RegistroC141 registroC141 = new BlocoC.RegistroC141();
            BlocoC.RegistroC160 registroC160 = new BlocoC.RegistroC160();
            BlocoC.RegistroC165 registroC165 = new BlocoC.RegistroC165();
            BlocoC.RegistroC170 registroC170 = new BlocoC.RegistroC170();
            BlocoC.RegistroC171 registroC171 = new BlocoC.RegistroC171();
            BlocoC.RegistroC172 registroC172 = new BlocoC.RegistroC172();
            BlocoC.RegistroC173 registroC173 = new BlocoC.RegistroC173();
            BlocoC.RegistroC174 registroC174 = new BlocoC.RegistroC174();
            BlocoC.RegistroC175 registroC175 = new BlocoC.RegistroC175();
            BlocoC.RegistroC176 registroC176 = new BlocoC.RegistroC176();
            BlocoC.RegistroC177 registroC177 = new BlocoC.RegistroC177();
            BlocoC.RegistroC178 registroC178 = new BlocoC.RegistroC178();
            BlocoC.RegistroC179 registroC179 = new BlocoC.RegistroC179();
            BlocoC.RegistroC190 registroC190 = new BlocoC.RegistroC190();
            BlocoC.RegistroC195 registroC195 = new BlocoC.RegistroC195();
            BlocoC.RegistroC197 registroC197 = new BlocoC.RegistroC197();
            BlocoC.RegistroC300 registroC300 = new BlocoC.RegistroC300();
            BlocoC.RegistroC310 registroC310 = new BlocoC.RegistroC310();
            BlocoC.RegistroC320 registroC320 = new BlocoC.RegistroC320();
            BlocoC.RegistroC321 registroC321 = new BlocoC.RegistroC321();
            BlocoC.RegistroC350 registroC350 = new BlocoC.RegistroC350();
            BlocoC.RegistroC370 registroC370 = new BlocoC.RegistroC370();
            BlocoC.RegistroC390 registroC390 = new BlocoC.RegistroC390();
            BlocoC.RegistroC400 registroC400 = new BlocoC.RegistroC400();
            BlocoC.RegistroC405 registroC405 = new BlocoC.RegistroC405();
            BlocoC.RegistroC410 registroC410 = new BlocoC.RegistroC410();
            BlocoC.RegistroC420 registroC420 = new BlocoC.RegistroC420();
            BlocoC.RegistroC425 registroC425 = new BlocoC.RegistroC425();
            BlocoC.RegistroC460 registroC460 = new BlocoC.RegistroC460();
            BlocoC.RegistroC465 registroC465 = new BlocoC.RegistroC465();
            BlocoC.RegistroC470 registroC470 = new BlocoC.RegistroC470();
            BlocoC.RegistroC490 registroC490 = new BlocoC.RegistroC490();
            BlocoC.RegistroC495 registroC495 = new BlocoC.RegistroC495();
            BlocoC.RegistroC500 registroC500 = new BlocoC.RegistroC500();
            BlocoC.RegistroC510 registroC510 = new BlocoC.RegistroC510();
            BlocoC.RegistroC590 registroC590 = new BlocoC.RegistroC590();
            BlocoC.RegistroC600 registroC600 = new BlocoC.RegistroC600();
            BlocoC.RegistroC601 registroC601 = new BlocoC.RegistroC601();
            BlocoC.RegistroC610 registroC610 = new BlocoC.RegistroC610();
            BlocoC.RegistroC690 registroC690 = new BlocoC.RegistroC690();
            BlocoC.RegistroC700 registroC700 = new BlocoC.RegistroC700();
            BlocoC.RegistroC790 registroC790 = new BlocoC.RegistroC790();
            BlocoC.RegistroC791 registroC791 = new BlocoC.RegistroC791();
            BlocoC.RegistroC800 registroC800 = new BlocoC.RegistroC800();
            BlocoC.RegistroC850 registroC850 = new BlocoC.RegistroC850();
            BlocoC.RegistroC860 registroC860 = new BlocoC.RegistroC860();
            BlocoC.RegistroC890 registroC890 = new BlocoC.RegistroC890();
            BlocoC.RegistroC990 registroC990 = new BlocoC.RegistroC990();

            var listaProdutos = DbProdutos.GetAllProdutos();
            var listaNfe = DbNfe.GetAllNfe();
            var listaEmitent = DbEmpresaEmit.GetAllEmpresaEmit();
            var listaDes = DbEmpresaDest.GetAllEmpresaDest();
            var listaImposto = DbImpostos.GetAllImposto();

            foreach (var item in listaNfe)
            {
                var nfeDTO = listaNfe.Where(c => c.Id == item.Id).FirstOrDefault();
                var empresaEmitDTO = listaEmitent.Where(c => c.Id == item.EmpresaEmetId).FirstOrDefault();
                var impostoDTO = listaImposto.Where(c => c.NfeId == item.Id).FirstOrDefault();

                List<string> listaProdNcm = new List<string>();

                var blocoC = ""; /*tabelaC02.Find(c => c.ChvNfe == item.CodBarra).FirstOrDefault();*/

                if (blocoC == "")
                {
                    registroC001.IndMov = IndMovimento.BlocoComDados;

                    //Pelo descrição da apuração vai ser verificado se a nota fiscal e de saída ou entrada.

                    if (nfeDTO != null)
                    {
                        if (nfeDTO.TipNfe == 0)
                            registroC100.IndOper = 1;
                        else
                            registroC100.IndOper = 0;
                    }

                    registroC100.Id = Guid.NewGuid();
                    registroC100.IndEmit = 0;
                    registroC100.CodPart = nfeDTO.CodParticipante;
                    registroC100.CodSit = 00;//Importante//00	Documento regular.01	Escrituração extemporânea de documento regular.02	Documento cancelado.03	Escrituração extemporânea de documento cancelado.04	NF-e, NFC-e ou CT-e - denegado.05	NF-e, NFC-e ou CT-e - Numeração inutilizada.
                    registroC100.Ser = nfeDTO.Serie.ToString();                                         //06  Documento Fiscal Complementar.07	Escrituração extemporânea de documento complementar.08	Documento Fiscal emitido com base em Regime Especial ou Norma Específica.
                    registroC100.NumDoc = nfeDTO.Nnfe.ToString();
                    registroC100.ChvNfe = nfeDTO.CodBarra;
                    registroC100.DtDoc = nfeDTO.DhEmi;
                    registroC100.DtEs = nfeDTO.DhSaida;
                    registroC100.VlDoc = Convert.ToDecimal(nfeDTO.VtTotalNfe);
                    registroC100.IndPgto = nfeDTO.TipoPagamento;
                    registroC100.VlDesc = Convert.ToDecimal(nfeDTO.VlTotalDesc);
                    registroC100.VlMerc = Convert.ToDecimal(nfeDTO.VlTotalPro);
                    registroC100.IndFrt = nfeDTO.TipoFrete;
                    registroC100.VlFrt = Convert.ToDecimal(nfeDTO.VlTotalFrete);
                    registroC100.VlSeg = Convert.ToDecimal(nfeDTO.VlTotalSeguro);
                    registroC100.VlOutDa = Convert.ToDecimal(nfeDTO.VlOutDes);
                    registroC100.VlBcIcms = Convert.ToDecimal(nfeDTO.BaseCAlIcms);
                    registroC100.VlIcms = Convert.ToDecimal(nfeDTO.VtIcms);
                    registroC100.VlBcIcmsSt = Convert.ToDecimal(nfeDTO.BaseCalIcmsSt);                  
                    registroC100.VlIpi = Convert.ToDecimal(nfeDTO.VlIpi);
                    registroC100.VlCofins = Convert.ToDecimal(nfeDTO.VlCofins);
                    registroC100.VlCofinsSt = nfeDTO.ValorTotalCofinsSt;
                    registroC100.EmpresaID = item.Id;

                    var listaProd = listaProdutos.Where(c => c.NfeId == item.Id).ToList();
                    registroC100.ListaProdNcm = listaProdNcm;

                    foreach (var produto in listaProd)
                    {
                        registroC170.EmpresaId = item.EmpresaEmetId;
                        registroC170.NfeId = nfeDTO.Id;
                        registroC170.IndMov = IndMovFisicaItem.Sim;
                        registroC170.NumItem = Convert.ToInt16(produto.NItemPedido);
                        registroC170.DescrCompl = "";
                        //registroC170.CodNat = produto.DesOperacao;//Importante 
                        //registroC170.CstIcms = Convert.ToInt16(produto.CSOSN);//Importante
                        registroC170.Cfop = Convert.ToInt32(produto.Cfop);
                        registroC170.Unid = produto.UnidMedida;
                        registroC170.Qtd = Convert.ToDecimal(produto.Quantidade);
                        registroC170.VlItem = Convert.ToDecimal(produto.VlProduto);
                        registroC170.VlDesc = Convert.ToDecimal( produto.VlTlDesconto);
                        registroC170.DescrCompl = produto.DescProduto;
                        registroC170.CodItem = produto.CodProduto; 
                        //NCM produto                      
                       
                        listaProdNcm.Add(produto.NcmProd);
                    }

                    registroC170.AliqIcms = 0;
                    registroC170.VlBcIcmsSt = Convert.ToDecimal(nfeDTO.BaseCalIcmsSt);
                    registroC170.VlBcIcms = Convert.ToDecimal(nfeDTO.BaseCAlIcms);
                    registroC170.VlIcms = Convert.ToDecimal(nfeDTO.VtIcms);
                    registroC170.CstIpi = "";                   
                    registroC170.VlBcIpi = 0;
                    registroC170.VlIpi = impostoDTO.Ipi;
                    registroC170.CstPis = 0;
                    registroC170.AliqIpi = 0;
                    registroC170.AliqPis = impostoDTO.AliqPis;
                    registroC170.VlBcPis = 0;
                    registroC170.QuantBcPis = 0;
                    registroC170.CstCofins = 0;
                    registroC170.AliqCofins = 0;
                    registroC170.VlBcCofins = 0;
                    registroC170.QuantBcCofins = 0;
                    registroC170.VlPis = 0;
                    registroC170.VlCofins = 0;
                    registroC170.VlIcmsSt = impostoDTO.VlIcmsSt;
                    registroC170.CodCta = "";

                    registroC190.CstIcms = 0;
                    registroC190.Cfop = 0;
                    registroC190.AliqIcms = impostoDTO.AliqIcms;
                    registroC190.VlOpr = 0;
                    registroC190.VlBcIcms = 0;                    
                    registroC190.VlIcms = 0;
                    registroC190.VlBcIcmsSt = 0;
                    registroC190.VlIcmsSt = impostoDTO.VlIcmsSt;
                    registroC190.VlIpi = 0;
                    

                    //Enserir Dados
                    tabelaC01.InsertOne(registroC001);
                    tabelaC02.InsertOne(registroC100);
                    tabelaC18.InsertOne(registroC170);
                    tabelaC28.InsertOne(registroC190);
                }

                //Consulta Id empresa cadastrada
                var registroC100s = tabelaC02.Find(c => c.EmpresaID == item.Id).ToList();

                var listaNcmAf = ListaNcm.GetLivros().ToList();

                List<NcmDTO> ncmDTOs = new List<NcmDTO>();

                foreach (var produtos in registroC100s)
                {
                    foreach (var subncm in produtos.ListaProdNcm)
                    {
                        if (subncm == produtos.NumDoc)
                        {
                            ncmDTOs.Add(new NcmDTO
                            {
                                Id = Guid.NewGuid(),
                                Ncm = subncm
                            });
                        }
                    }
                }
            }


            //Valor do PIS/Pasep                
            //Valor do PIS/Pasep ST
            registroC100.VlAbatNt = 0;//desconto ICMS nas remessas para ZFM, se o cliente for na nona franca suframa ver o desconto que ele tem direito.
            //Este registro tem por objetivo identificar a UF destinatária do recolhimento do ICMS ST, quando esta for diversa 
            //da UF do destinatário do produto.Ex.Leasing de veículo quando a entidade financeira está localizada em uma UF e o destinatário do produto em outra UF.
            //registroC105.Uf = nfeDTO. Ver a necesssidade de passar esse informação

            registroC100.VlPisSt = 0;//  

            //Este registro deve ser apresentado, obrigatoriamente, quando no campo –
            //“Informações Complementares” da nota fiscal - constar a discriminação de processos referenciados no documento fiscal.
            if (registroC110.TxtCompl != "")
            {
                registroC111.NumProc = "";//Indentificador do processo ou ato concessório
                registroC111.IndProc = 0;//Verificar que informação vai ser passardo nesse campo
            }
            //Este registro deve ser apresentado, obrigatoriamente, quando no campo
            //– “Informações Complementares” da nota fiscal - constar a identificação de um documento de arrecadação.
            if (registroC110.TxtCompl != "")
            {
                registroC112.CodDa = 1;//Código do modelo da arrecadação,ver como vai ser passado essa informação
                registroC112.Uf = "";//Unidade benefinciaria do recolhimento
                registroC112.NumDa = "";//Numero de mumero de arrecadação
            }

            registroC115.IndCarga = IndTipoTransporte.Rodoviario;//Ver a logica que vai ser passado nesse campo para definir tipo de transporte
            registroC115.CnpjCol = "";
            registroC115.IeCol = "";
            registroC115.CpfCol = "";
            registroC115.CodMunCol = "";
            registroC115.CnpjEntg = "";
            registroC115.IeEntg = "";
            registroC115.CpfEntg = "";
            registroC115.CodMunEntg = "";


            registroC100.VlPis = 0;//Valor taotal do PIS  

            //Este registro tem por objetivo prestar informações complementares constantes da NF-e quando das operações interestaduais destinadas a consumidor final NÃO contribuinte do ICMS
            registroC101.VlFcpUfDest = 0;//Valor total d fundo de combate a pobresa do UF de destino
            registroC101.VlIcmsUfDest = 0;//Valor ICMS interestadual para a UF de destino
            registroC101.VlIcmsUfRem = 0;//Valor total do ICMS interestaduak para a uf do remetente 

            //Este registro tem por objetivo identificar a UF destinatária do recolhimento do ICMS ST,
            //quando esta for diversa da UF do destinatário do produto. Ex. Leasing de veículo quando a entidade financeira está localizada em uma UF e o destinatário do produto em outra UF.
            registroC105.Oper = IndTipoOperacaoStUfDiversa.CombustiveisLubrificantes;//Veificar que infomação vai ser passada nesse caso

            //Este registro tem por objetivo informar, detalhadamente, outros documentos fiscais que tenham sido mencionados nas informações complementares
            //do documento que está sendo escriturado no registro C100, exceto cupons fiscais, que devem ser informados no registro C114. Exemplos:
            //nota fiscal de remessa de mercadoria originária de venda para entrega futura e nota fiscal de devolução de compras.
            registroC113.IndOper = 0;//Indicador do tipo da operação entrada/saida/prestação/aquisição
            registroC113.IndEmit = 1;//Indicação do emitente do titulo 0-emissaõ propia, 1- terceiros 
            registroC113.CodPart = "";//Ver como vai ser passado esse dados nesse campo
            registroC113.CodMod = 0;//Código Documento fiscal
            registroC113.Ser = "";
            registroC113.Sub = "";//Ver qual irformação vai ser passado nesse campo
            registroC113.NumDoc = "";
            registroC113.DtDoc = DateTime.Now;

            //Este registro será utilizado para informar, detalhadamente, nas operações de saídas,
            //cupons fiscais que tenham sido mencionados nas informações complementares do documento que está sendo escriturado no registro C100.
            //Nas operações de entradas, somente informar quando o emitente do cupom fiscal for o próprio informante do arquivo.
            registroC114.CodMod = 0;
            registroC114.EcfFab = "";//N° serie de fabricação do ECF
            registroC114.EcfCx = 0;//Ver que infomação vai ser passada nesse campo
            registroC114.NumDoc = "";
            registroC114.DtDoc = DateTime.Now;

            //Este registro será utilizado para informar, detalhadamente, nas operações de saídas, cupons fiscais eletrônicos que tenham sido mencionados nas informações complementares do documento que está sendo escriturado no registro C100.
            //Nas operações de entradas no registro C100,
            //somente informar quando o emitente do cupom fiscal for o próprio informante do arquivo. Este registro está relacionado ao documento fiscal informado no registro C800 ou C860.
            registroC116.CodMod = "";//Cupon fical eletronico
            registroC116.NrSat = "";//Número de serie equipamento SAT ver oque vai ser preenchido nesse campo
            registroC116.ChvCfe = "";//chave do cupun fiscal
            registroC116.NumCfe = 0;//numero cupon fiscal
            registroC116.DtDoc = DateTime.Now;//Emissao formato ddmmaaaa

            //Este registro tem por objetivo informar detalhes das operações de importação, 
            //que estejam sendo documentadas pela nota fiscal escriturada no registro C100, quando o campo IND_OPER for igual a “0” (zero), indicando operação de entrada.
            registroC120.CodDocImp = 0;//0-Declaração -1declaração simples,ver como vai ser passado essa informação nesse campo
            registroC120.NumDocImp = "";//referente a importação
            registroC120.PisImp = 0;//Referente a importação
            registroC120.CofinsImp = 0;//Referente a importação
            registroC120.NumAcDraw = "";//Regime Drawbck

            //Este Registro tem por objetivo informar dados da prestação de serviços sob não-incidência ou não tributados pelo ICMS e ainda detalhes sobre a retenção de Imposto de Renda Retido na Fonte (IRRF) e de contribuições previdenciárias.
            registroC130.VlServNt = 0;//Serviço não atribuido pelo icms
            registroC130.VlBcIssqn = 0;//Base de calculo
            registroC130.VlIssqn = 0;//Valor do Issqn
            registroC130.VlBcIrrf = 0;//base de calculo retino na fonte importo de renda
            registroC130.VlIrrf = 0;//valor do imposto retido na fonte
            registroC130.VlBcPrev = 0;//base de calculo previdencia social
            registroC130.VlPrev = 0;//valor previdencia social 

            //Este Registro tem por objetivo informar dados da fatura comercial, sempre que a aquisição ou venda de mercadorias for a prazo,
            //por meio de Notas Fiscais Modelos 1 ou 1A. Devem ser consideradas as informações quando da emissão do documento fiscal, incluindo a parcela paga no ato da operação, se for o caso.
            registroC140.IndEmit = 0;//Indicador do titulo 0-- Emissai propia 1-- Terceiros
            registroC140.IndTit = 0;//tipo de titulo 00-duplicata --01 Cheque --02 Promissória --03 Recibo --99 Outros
            registroC140.DescTit = "";//Descrição do titulo
            registroC140.NumTit = "";//Numero indentificador do titulo
            registroC140.QtdParc = 0;//Qtd de parcelas receber/Bagar
            registroC140.VlTit = 0;//Valar total dos creditos

            //Este Registro deve ser apresentado, obrigatoriamente, sempre que for informado o Registro C140, devendo ser discriminados o valor e a data de vencimento de cada uma das parcelas.
            registroC141.NumParc = 0;//Numero da parcela receber/pagar
            registroC141.DtVcto = DateTime.Now;//Data venvimento da parcela
            registroC141.VlParc = 0;//Valor da parcela "Campo repetido ver a necessidade de passar novamete esse campo"

            //Este Registro tem por objetivo informar detalhes dos volumes, do transportador e do veículo empregado no transporte nas operações de saídas.
            registroC160.CodPart = "";//Codigo do particiante, ver como sera passando essa informação nesse campo
            registroC160.VeicId = "";//Placa de indentificação do veiculo,campo repedito ver a necessidade de passar novamnte esse campo
            registroC160.QtdVol = 0;//Qtd de volumes que está transportandi
            registroC160.PesoBrt = 0;//Peso bruto da mercadorias que esta sendo transportada
            registroC160.PesoLiq = 0;//Peso liquido dos volunes trasportados kg
            registroC160.UfId = "";//Uf da placa do veiculo que esta sendo transportado,campo repetido ver a necessidade de ter novamente esse campo


            //Este Registro deve ser apresentado pelas empresas do segmento de combustíveis(distribuidoras, refinarias, revendedoras) em operações de saída. Postos de combustíveis não devem apresentar este Registro.
            registroC165.CodPart = "";//Campo repetido
            registroC165.VeicId = "";//Capmpo repetido
            registroC165.CodAut = "";//Ver que código que e fornecedio pela sefaz para preeechimento desse campo
            registroC165.NrPasse = "";//Numero Passe fiscal
            registroC165.Hora = DateTime.Now;//Horas da saida da merdadoria
            registroC165.Temper = 0;//Temperatura em graus Celsius utilizada para quantificação do volume de combustível
            registroC165.QtdVol = 0;//Qtd de volumes trasportad em KG,campo repetido ver como vai ser passado essa informação
            registroC165.PesoBrt = 0;//Campo repetido
            registroC165.PesoLiq = 0;//Campo repetido
            registroC165.NomMot = "";//Nome do motorsista 
            registroC165.Cpf = "";//Cpf motorista
            registroC165.UfId = "";//Campo repetido


            registroC110.CodInf = "";
            registroC110.TxtCompl = "";

            registroC170.DescrCompl = "";//Descrição do item conforme documento fiscal            
            registroC170.IndApur = IndPeriodoApuracaoIpi.Decendial;//Intidacor periudo de indicação,0-Mensal -1 Decendial, ver que informação vai ser passado
            registroC170.CodEnq = "";//Código enquadramento legal do IPI, ver que informação deve ser passado aki
            registroC170.AliqSt = 0; ;
            registroC170.AliqCofinsReais = 0;//Aliquata da Confins em reias
            registroC170.AliqPisReais = 0;//Aliquata em reias do PIS
           
           



            //Que trata do "Armazenamento de combustíveis (Código 01 e 55)" do Sped - Fiscal.
            registroC171.NumTanque = "";//Tanque armazenamento do compustivel
            registroC171.Qtde = 0;//Qtd de volumes amarzenado

            //ISSQN é o Imposto sobre serviços de qualquer natureza,
            registroC172.VlBcIssqn = 0;//campo repetido/ver
            registroC172.AliqIssqn = 0;//Campo repetido/ver
            registroC172.VlIssqn = 0;//Campo repetido/ver

            //Que tem por objetivo apresentar informações adicionais a respeito das operações com medicamentos(Código 01 e 55).
            registroC173.LoteMed = "";//Numero do loto de fabricação do medicamento
            registroC173.QtdItem = 0;//qdt item por lote
            registroC173.DtFab = DateTime.Now;//Data fabricação do medicamento
            registroC173.DtVal = DateTime.Now;//Data de expiração da validade do medicamento
            registroC173.IndMed = 0;//Indicador base de calculo do ICMS   0 - Base de cálculo referente ao preço tabelado ou preço máximo sugerido
            ///     1 - Base cálculo - Margem de valor agregado
            ///     2 - Base de cálculo referente à Lista Negativa
            ///     3 - Base de cálculo referente à Lista Positiva
            ///     4 - Base de cálculo referente à Lista Neutra
            registroC173.TpProd = 0;//Tipo de produtos  0 - Similar 1 - Genérico 2 - Ético ou de marca
            registroC173.VlTabMax = 0;//Valor preço de tabela ou maximo 

            //Que tem como objetivo demonstrar as operações com armas de fogo. Registramos que a obrigatoriedade de entrega desse Registro abrange apenas as operações de saída.
            registroC174.IndArm = 0;//Indicador tipo de arma de fogo 0 - Uso permitido  1 - Uso restrito
            registroC174.NumArm = "";//Numero de série de fabricação
            registroC174.DescrCompl = "";//Descrição geral da arma

            //Que tem por objetivo representar a escrituração da NFC-e, código 65, os documentos fiscais totalizados por CST PIS, CST Cofins, CFOP, alíquota de PIS e alíquota da Cofins. Trata-se de registro com procedimento de escrituração similar ao adotado para o registro C190 da EFD-ICMS/IPI.
            registroC175.IndVeicOper = 0;//Indicador tipo de operação veicular 0 - Venda para concessionária, 1 - Faturamento direto,2 - Venda direta, 3 - Venda da concessionária, 9 - Outros
            registroC175.Cnpj = "";//Cnpj da consecionaria
            registroC175.Uf = "";//Estado da consecionaria
            registroC175.ChassiVeic = "";//Chssis do veiculo da operação

            //Este registro deve ser informado quando da escrituração de documento fiscal, que acoberte operação que represente desfazimento de substituição tributária realizada em operações anteriores.
            //O documento informado neste registro deverá ser diferente do documento informado no registro pai(C100), pois é o documento referente à(s) última(s) aquisição(ões) da mercadoria e à retenção do imposto.
            registroC176.CodModUltE = "";//Ver como vai se passado essa informação
            registroC176.NumDocUltE = 0;//ver como vai ser passado essa informação
            registroC176.SerUltE = "";//Ver como vai ser passado essa informação
            registroC176.DtUltE = DateTime.Now;//Data da ultima entrega de mercaodia
            registroC176.CodPartUltE = "";//código do participante ver como vai ser passado essa informação
            registroC176.QuantUltE = 0;//qtd do item relativo a ultima entrega,ver sobre esse campo
            registroC176.VlUnitUltE = 0;//ver que informação vai ser passado nesse campo
            registroC176.VlUnitBcSt = 0;//Valor unitário da base de cálculo do imposto pago por substituição

            //Este registro deverá ser apresentado somente pelos contribuintes obrigados por legislação específica de cada UF, com o objetivo de agregar informações adicionais ao item, de acordo com tabela a ser publicada pela UF.
            registroC177.CodSeloIpi = "";//Código do selo de controle IPI, ver 
            registroC177.QtSeloIpi = 0;//Ver as informaçao que vai passar nesse campo

            //Que tem por objetivo fornecer informações adicionais sobre os produtos cuja forma de tributação do Imposto sobre Produtos Industrializados (IPI), fixada em reais, seja calculada por unidade ou por determinada quantidade de produto, conforme
            registroC178.ClEnq = "";//Ver informação desse campo
            registroC178.VlUnid = 0;//Ver sobre esse valor
            registroC178.QuantPad = 0;//qtd unidade padrão de tributação, ver essa informação



            //Que tem por objetivo informar operações que envolvam repasse, dedução e complemento de ICMS devido por Substituição Tributária (ICMS-ST) nas operações interestaduais e nas operações com substituído intermediário.
            registroC179.BcStOrigDest = 0;//Ver informação desse campo
            registroC179.IcmsStRep = 0;//Ver esse campos
            registroC179.IcmsStCompl = 0;//Valor do ICMS-ST a complementar a UF de destino
            registroC179.BcRet = 0;//Ver esse campo 
            registroC179.IcmsRet = 0;//Ver esse campo

            registroC190.VlRedBc = 0;
            registroC190.CodObs = "";


            registroC195.CodObs = "";
            registroC195.TxtCompl = "";

            //tem por objetivo detalhar outras obrigações tributárias, ajustes e informações de valores provenientes de documento fiscal do Registro C195, que podem ou não alterar o cálculo do valor do imposto.
            registroC197.CodAj = "";
            registroC197.DescrComplAj = "";
            registroC197.CodItem = "";
            registroC197.VlBcIcms = 0;
            registroC197.AliqIcms = 0;
            registroC197.VlIcms = 0;
            registroC197.VlOutros = 0;

            //que tem por objetivo apresentar um resumo diário, por série e subsérie, de todas as Notas Fiscais de Venda a Consumidor (NFVC), Modelo 02, emitidas.
            registroC300.CodMod = 0;
            registroC300.Ser = "";
            registroC300.Sub = "";
            registroC300.NumDocIni = 0;
            registroC300.NumDocFin = 0;
            registroC300.DtDoc = DateTime.Now;
            registroC300.VlDoc = 0;
            registroC300.VlPis = 0;
            registroC300.VlCofins = 0;
            registroC300.CodCta = "";

            //Que tem por objetivo informar a consolidação diária dos valores das Notas Fiscais de Venda ao Consumidor(NFVC), Modelo 2, não emitidas por Equipamento Emissor de Cupom Fiscal(ECF). O Registro C320, num conceito mais sintético, é um Registro Analítico do Resumo Diário das NFVC.
            registroC320.CstIcms = 0;
            registroC320.Cfop = 0;
            registroC320.AliqIcms = 0;
            registroC320.VlOpr = 0;
            registroC320.VlBcIcms = 0;
            registroC320.VlIcms = 0;
            registroC320.VlRedBc = 0;
            registroC320.CodObs = "";

            //Que tem por objetivo detalhar, por itens de mercadoria, a consolidação diária dos valores das Notas Fiscais de Venda ao Consumidor(NFVC), não emitidas por Equipamento Emissor de Cupom Fiscal(ECF).
            registroC321.CodItem = "";
            registroC321.Qtd = 0;
            registroC321.Unid = "";
            registroC321.VlItem = 0;
            registroC321.VlDesc = 0;
            registroC321.VlBcIcms = 0;
            registroC321.VlIcms = 0;
            registroC321.VlPis = 0;
            registroC321.VlCofins = 0;

            //que trata da Nota Fiscal de Venda a Consumidor (Código 02).
            registroC350.Ser = "";
            registroC350.SubSer = "";
            registroC350.NumDoc = 0;
            registroC350.DtDoc = DateTime.Now;
            registroC350.CnpjCpf = "";
            registroC350.VlMerc = 0;
            registroC350.VlDoc = 0;
            registroC350.VlDesc = 0;
            registroC350.VlPis = 0;
            registroC350.VlCofins = 0;
            registroC350.CodCta = "";

            //que tem por objetivo detalhar os itens da Nota Fiscal de Venda ao Consumidor - NFVC (Código 02).
            registroC370.NumItem = 0;
            registroC370.CodItem = "";
            registroC370.Qtd = 0;
            registroC370.Unid = "";
            registroC370.VlItem = 0;
            registroC370.VlDesc = 0;

            //que tem por objetivo informar as Notas Fiscais de Venda ao Consumidor (NFVC), não emitidas por Emissor de Cupom Fiscal (ECF).
            registroC390.CstIcms = 0;
            registroC390.Cfop = 0;
            registroC390.AliqIcms = 0;
            registroC390.VlOpr = 0;
            registroC390.VlBcIcms = 0;
            registroC390.VlIcms = 0;
            registroC390.VlRedBc = 0;
            registroC390.CodObs = "";

            //Que tem por objetivo identificar os equipamentos de ECF(Código 02, 2D e 60).
            registroC400.CodMod = "";
            registroC400.EcfMod = "";
            registroC400.EcfFab = "";
            registroC400.EcfCx = 0;

            //Que trata da Redução Z (Código 02, 2D e 60).
            registroC405.DtDoc = DateTime.Now;
            registroC405.Cro = 0;
            registroC405.Crz = 0;
            registroC405.NumCoofin = 0;
            registroC405.GtFin = 0;
            registroC405.VlBrt = 0;

            //Deve ser apresentado sempre que houver produtos totalizados na Redução Z que acarretem valores de PIS/ Pasep e Cofins a serem informados.
            registroC410.VlPis = 0;
            registroC410.VlCofins = 0;

            //Que trta do Registro dos totalizadores parciais da Redução Z (Código 02, 2D e 60).
            registroC420.CodTotPar = "";
            registroC420.VlrAcumTot = 0;
            registroC420.NrTot = 0;
            registroC420.DescrNrTot = "";

            //O registro C425 tem por objetivo identificar os produtos comercializados na data da movimentação relativa à Redução Z informada, sendo obrigatório, quando os totalizadores forem iguais a "xxTnnnn", "Tnnnn", "Fn", "In", "Nn".
            registroC425.CodItem = "";
            registroC425.Qtd = 0;
            registroC425.Unid = "";
            registroC425.VlItem = 0;
            registroC425.VlPis = 0;
            registroC425.VlCofins = 0;

            //O Registro C460 deve ser apresentado para a identificação dos documentos fiscais emitidos pelos usuários de equipamentos ECF, que foram totalizados na Redução Z.
            //Para Cupom Fiscal cancelado, informar somente os campos "COD_MOD", "COD_SIT" e "NUM_DOC", sem os registros filhos.
            registroC460.CodMod = "";
            registroC460.CodSit = 0;
            registroC460.NumDoc = 0;
            registroC460.DtDoc = DateTime.Now;
            registroC460.VlDoc = 0;
            registroC460.VlPis = 0;
            registroC460.VlCofins = 0;
            registroC460.CpfCnpj = "";
            registroC460.NomeAdq = "";

            //Que trata do (Complemento do Cupom Fiscal Eletrônico emitido por ECF - CF - e - ECF(Código 60).
            registroC465.ChvCfe = "";
            registroC465.NumCcf = 0;

            //O Registro C470 deve ser apresentado para informar os itens dos documentos fiscais emitidos pelos usuários de equipamentos ECF, que foram totalizados na Redução Z. 
            //    O serviço de competência Municipal(sujeito ao ISSQN) também deverá ser informado nesse registro.
            //    Para tanto, deverá ser criado o correspondente item no Registro 0200, cujo conteúdo do campo "TIPO_ITEM" será igual "09"(Serviços).Não informar o registro para o item cuja venda foi totalmente cancelada.
            registroC470.CodItem = "";
            registroC470.Qtd = 0;
            registroC470.QtdCanc = 0;
            registroC470.Unid = "";
            registroC470.VlItem = 0;
            registroC470.CstIcms = 0;
            registroC470.Cfop = 0;
            registroC470.AliqIcms = 0;
            registroC470.VlPis = 0;
            registroC470.VlCofins = 0;

            //Este Registro tem por objetivo representar a escrituração dos documentos fiscais emitidos por ECF e totalizados pela combinação de CST, 
            //CFOP e Alíquota. Não informar este registro para os totalizadores "Can-T", "Can-S" e "Can-O" informados no Registro C420.
            registroC490.CstIcms = 0;
            registroC490.Cfop = 0;
            registroC490.AliqIcms = 0;
            registroC490.VlOpr = 0;
            registroC490.VlBcIcms = 0;
            registroC490.VlIcms = 0;
            registroC490.CodObs = "";

            //Este Registro deve ser apresentado pelo contribuinte domiciliado no Estado da Bahia até 31/12/2013, resumindo todas as informações num único registro
            //por item de mercadorias, não dispensando a apresentação do Registro C400 e registros filhos. A partir de 01/01/2014, os contribuintes situados na Bahia não apresentam este Registro e devem apresentar o Registro C425.
            registroC495.AliqIcms = 0;
            registroC495.CodItem = "";
            registroC495.Qtd = 0;
            registroC495.QtdCanc = 0;
            registroC495.Unid = "";
            registroC495.VlItem = 0;
            registroC495.VlDesc = 0;
            registroC495.VlCanc = 0;
            registroC495.VlAcmo = 0;
            registroC495.VlBcIcms = 0;
            registroC495.VlIcms = 0;
            registroC495.VlIsen = 0;
            registroC495.VlNt = 0;
            registroC495.VlIcmsSt = 0;


            // Este registro deve ser apresentado, nas operações de saída, pelos contribuintes do segmento de energia elétrica e não obrigados ao Convênio ICMS 115 / 03,
            //pelos contribuintes do segmento de fornecimento de gás e, nas operações de entrada, por todos os contribuintes adquirentes.
            //A partir de janeiro de 2020, deve ser apresentado também pelos contribuintes que emitirem a NF3e(modelo 66), mesmo que obrigados ao Convênio 115 / 03.
            //https://www.valor.srv.br/guias/guiasIndex.php?idGuia=80
            registroC500.IndOper = IndTipoOperacaoProduto.Entrada;//Que tipo de operação vai ser feita nesse campo
            registroC500.IndEmit = IndEmitente.EmissaoPropria;//Que tipo de operação vai ser feita nesse campo
            registroC500.CodPart = "";//do adquirente ou do fornecedor
            registroC500.CodMod = "";
            registroC500.CodSit = IndCodSitDoc.DocumentoRegular;//Ver que informação vai ser passa aki
            registroC500.Ser = "";
            registroC500.Sub = "";
            registroC500.CodCons = 0;//01-Comercial 02-Consumo proprio 03-Iluminação publica 04-Industria -
            registroC500.NumDoc = 0;
            registroC500.DtDoc = DateTime.Now;
            registroC500.DtEs = DateTime.Now;
            registroC500.VlDoc = 0;
            registroC500.VlDesc = 0;
            registroC500.VlForn = 0;
            registroC500.VlServNt = 0;
            registroC500.VlTerc = 0;
            registroC500.VlDa = 0;
            registroC500.VlBcIcms = 0;
            registroC500.VlIcms = 0;
            registroC500.VlBcIcmsSt = 0;
            registroC500.VlIcmsSt = 0;
            registroC500.CodInf = "";
            registroC500.VlPis = 0;
            registroC500.VlCofins = 0;
            registroC500.TpLigacao = 0;
            registroC500.CodGrupoTensao = 0;

            //O Registro C510 deve ser apresentado para informar os itens das Notas Fiscais/ Contas de Energia Elétrica(Código 06 da Tabela Documentos Fiscais do ICMS), 
            //Notas Fiscais/ Contas de fornecimento de água canalizada(Código 29) e Notas Fiscais Consumo Fornecimento de Gás(Código 28 da Tabela Documentos Fiscais do ICMS), nas operações de saída.
            registroC510.NumItem = 0;
            registroC510.CodItem = "";
            registroC510.CodClass = 0;
            registroC510.Qtd = 0;
            registroC510.Unid = "";
            registroC510.VlItem = 0;
            registroC510.VlDesc = 0;
            registroC510.CstIcms = 0;
            registroC510.Cfop = 0;
            registroC510.VlBcIcms = 0;
            registroC510.AliqIcms = 0;
            registroC510.VlIcms = 0;
            registroC510.VlBcIcmsSt = 0;
            registroC510.AliqSt = 0;
            registroC510.VlIcmsSt = 0;
            registroC510.IndRec = 0;
            registroC510.CodPart = "";
            registroC510.VlPis = 0;
            registroC510.VlCofins = 0;
            registroC510.CodCta = "";

            //O Registro C590 representa a escrituração dos documentos fiscais dos modelos especificados no Registro C500, totalizados pelo agrupamento das combinações dos valores de CST, CFOP e Alíquota dos itens de cada documento.
            //Deve haver 1(um) Registro C590 com os totais de cada combinação de valores de CST, CFOP e Alíquota, informados nos itens dos Registros C510(operações de saída) ou com base nos documentos fiscais de entrada.
            registroC590.CstIcms = 0;
            registroC590.Cfop = 0;
            registroC590.AliqIcms = 0;
            registroC590.VlOpr = 0;
            registroC590.VlBcIcms = 0;
            registroC590.VlIcms = 0;
            registroC590.VlBcIcmsSt = 0;
            registroC590.VlIcmsSt = 0;
            registroC590.VlRedBc = 0;
            registroC590.CodObs = "";

            //O Registro C600 deve ser apresentado na consolidação diária de Notas Fiscais/ Conta de energia elétrica(Código 06 da Tabela Documentos Fiscais do ICMS),
            //Notas Fiscais de fornecimento dágua(Código 29 da Tabela Documentos Fiscais do ICMS)
            //e Notas Fiscais / Conta de fornecimento de gás(Código 28 da Tabela Documentos Fiscais do ICMS) para empresas não obrigadas ao Convênio ICMS nº 115 / 2003.
            registroC600.CodMod = "";
            registroC600.CodMun = 0;
            registroC600.Ser = "";
            registroC600.Sub = 0;
            registroC600.CodCons = 0;
            registroC600.QtdCons = 0;
            registroC600.QtdCanc = 0;
            registroC600.DtDoc = DateTime.Now;
            registroC600.VlDoc = 0;
            registroC600.VlDesc = 0;
            registroC600.Cons = 0;
            registroC600.VlForn = 0;
            registroC600.VlServNt = 0;
            registroC600.VlTerc = 0;
            registroC600.VlDa = 0;
            registroC600.VlBcIcms = 0;
            registroC600.VlIcms = 0;
            registroC600.VlBcIcmsSt = 0;
            registroC600.VlIcmsSt = 0;
            registroC600.VlPis = 0;
            registroC600.VlCofins = 0;

            //O Registro C601 tem por objetivo informar a numeração dos documentos cancelados da consolidação diária dos documentos fiscais do Registro C600.
            registroC601.NumDocCanc = 0;

            // Registro C610 tem por objetivo discriminar por item os registros consolidados apresentados no Registro C600.
            registroC610.CodClass = 0;
            registroC610.CodItem = "";
            registroC610.Qtd = 0;
            registroC610.Unid = "";
            registroC610.VlItem = 0;
            registroC610.VlDesc = 0;
            registroC610.CstIcms = 0;
            registroC610.Cfop = 0;
            registroC610.AliqIcms = 0;
            registroC610.VlBcIcms = 0;
            registroC610.VlIcms = 0;
            registroC610.VlBcIcmsSt = 0;
            registroC610.VlIcmsSt = 0;
            registroC610.VlPis = 0;
            registroC610.VlCofins = 0;
            registroC610.VlCofins = 0;
            registroC610.CodCta = "";

            //O Registro C690 tem por objetivo representar a escrituração dos documentos fiscais dos modelos especificados no Registro C600, totalizados pelo agrupamento das combinações dos valores de CST,
            //CFOP e Alíquota dos itens de cada registro consolidado. Existirá um Registro C690 para cada combinação de valores de CST, CFOP e Alíquota que existir nos itens (Registro C610), totalizando estes itens.
            registroC690.CstIcms = 0;
            registroC690.Cfop = 0;
            registroC690.AliqIcms = 0;
            registroC690.VlOpr = 0;
            registroC690.VlBcIcms = 0;
            registroC690.VlIcms = 0;
            registroC690.VlRedBc = 0;
            registroC690.VlBcIcmsSt = 0;
            registroC690.VlIcmsSt = 0;
            registroC690.CodObs = "";

            //Este Registro deve ser apresentado com a consolidação das Notas Fiscais / Conta de Energia Elétrica(código 06 da Tabela Documentos Fiscais do ICMS) 
            //pelas empresas obrigadas à entrega do arquivo previsto no Convênio ICMS nº 115 / 2003.
            //https://www.valor.srv.br/guias/guiasIndex.php?idGuia=87
            registroC700.CodMod = "";
            registroC700.Ser = "";
            registroC700.NroOrdIni = 0;
            registroC700.NroOrdFin = 0;
            registroC700.DtDocIni = DateTime.Now;
            registroC700.DtDocFin = DateTime.Now;
            registroC700.Nom_Mest = "";
            registroC700.ChvCodDig = "";

            //Este Registro representa a escrituração dos documentos fiscais dos modelos especificados no Registro C700, totalizados pelo agrupamento das combinações dos valores de Código da Situação Tributária (CST),
            //Código Fiscal de Operação e Prestação (CFOP) e Alíquota dos itens de cada registro consolidado.
            registroC790.CstIcms = 0;
            registroC790.Cfop = 0;
            registroC790.AliqIcms = 0;
            registroC790.VlOpr = 0;
            registroC790.VlBcIcms = 0;
            registroC790.VlIcms = 0;
            registroC790.VlBcIcmsSt = 0;
            registroC790.VlIcmsSt = 0;
            registroC790.VlRedBc = 0;
            registroC790.CodObs = "";

            //Registro de Informações de ST por UF
            registroC791.Uf = "";
            registroC791.VlBcIcmsSt = 0;
            registroC791.VlIcmsSt = 0;

            //O Registro C800 deve ser gerado para cada Cupom Fiscal Eletrônico (CF-e-SAT) (Código 59) emitido por equipamento SAT-CF-e, conforme Ajuste Sinief nº 11/2010.
            registroC800.CodMod = "";
            registroC800.CodSit = 0;
            registroC800.NumCfe = 0;
            registroC800.DtDoc = DateTime.Now;
            registroC800.VlCfe = 0;
            registroC800.VlPis = 0;
            registroC800.VlCofins = 0;
            registroC800.CnpjCpf = "";
            registroC800.NrSat = "";
            registroC800.ChvCfe = "";
            registroC800.VlDesc = 0;
            registroC800.VlMerc = 0;
            registroC800.VlOutDa = 0;
            registroC800.VlIcms = 0;
            registroC800.VlPisSt = 0;
            registroC800.VlCofinsSt = 0;

            //O Registro C850 tem por objetivo representar a escrituração do Cupom Fiscal Eletrônico (CF-e-SAT), código 59,
            //segmentado por Código da Situação Tributária (CST), Código Fiscal de Operação e Prestação (CFOP) e Alíquota do ICMS.
            registroC850.CstIcms = 0;
            registroC850.Cfop = 0;
            registroC850.AliqIcms = 0;
            registroC850.VlOpr = 0;
            registroC850.VlBcIcms = 0;
            registroC850.VlIcms = 0;
            registroC850.CodObs = "";

            //Este Registro tem por objetivo identificar os equipamentos SAT-CF-e e deve ser informado por todos os contribuintes que utilizem tais equipamentos na emissão de documentos fiscais.
            registroC860.CodMod = "";
            registroC860.NrSat = "";
            registroC860.DtDoc = DateTime.Now;
            registroC860.NumDocIni = 0;
            registroC860.NumDocFin = 0;

            //Este Registro tem por objetivo promover a consolidação dos CF-e-SAT emitidos no período, por equipamento SAT-CF-e.
            registroC890.CstIcms = 0;
            registroC890.Cfop = 0;
            registroC890.AliqIcms = 0;
            registroC890.VlOpr = 0;
            registroC890.VlBcIcms = 0;
            registroC890.VlIcms = 0;
            registroC890.CodObs = "";

            //O Registro C990 destina-se a identificar o encerramento do Bloco C e informar a quantidade de linhas(registros) existentes no bloco.
            registroC990.QtdLinC = 0;

        }
    }
}
