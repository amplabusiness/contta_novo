using Contta.Common;
using Contta.SpedFiscal;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Corporate.Contta.Schedule.SpedContta.MapBlocos
{
    public class MapBlocoE
    {
        public void GetBloboE()
        {
            IMongoClient mongoClient = new MongoClient("mongodb://localhost");
            IMongoDatabase database = mongoClient.GetDatabase("Bloco0");
            IMongoCollection<BlocoE.RegistroE001> tabelaE01 = database.GetCollection<BlocoE.RegistroE001>("RegistroE001");
            IMongoCollection<BlocoE.RegistroE100> tabelaE02 = database.GetCollection<BlocoE.RegistroE100>("RegistroE100");
            IMongoCollection<BlocoE.RegistroE110> tabelaE03 = database.GetCollection<BlocoE.RegistroE110>("RegistroE110");
            IMongoCollection<BlocoE.RegistroE111> tabelaE04 = database.GetCollection<BlocoE.RegistroE111>("RegistroE111");
            IMongoCollection<BlocoE.RegistroE112> tabelaE05 = database.GetCollection<BlocoE.RegistroE112>("RegistroE112");
            IMongoCollection<BlocoE.RegistroE113> tabelaE06 = database.GetCollection<BlocoE.RegistroE113>("RegistroE113");
            IMongoCollection<BlocoE.RegistroE115> tabelaE07 = database.GetCollection<BlocoE.RegistroE115>("RegistroE115");
            IMongoCollection<BlocoE.RegistroE116> tabelaE08 = database.GetCollection<BlocoE.RegistroE116>("RegistroE116");
            IMongoCollection<BlocoE.RegistroE200> tabelaE09 = database.GetCollection<BlocoE.RegistroE200>("RegistroE200");
            IMongoCollection<BlocoE.RegistroE210> tabelaE10 = database.GetCollection<BlocoE.RegistroE210>("RegistroE210");
            IMongoCollection<BlocoE.RegistroE220> tabelaE11 = database.GetCollection<BlocoE.RegistroE220>("RegistroE220");
            IMongoCollection<BlocoE.RegistroE230> tabelaE12 = database.GetCollection<BlocoE.RegistroE230>("RegistroE230");
            IMongoCollection<BlocoE.RegistroE240> tabelaE13 = database.GetCollection<BlocoE.RegistroE240>("RegistroE240");
            IMongoCollection<BlocoE.RegistroE250> tabelaE14 = database.GetCollection<BlocoE.RegistroE250>("RegistroE250");
            IMongoCollection<BlocoE.RegistroE300> tabelaE15 = database.GetCollection<BlocoE.RegistroE300>("RegistroE300");
            IMongoCollection<BlocoE.RegistroE310> tabelaE16 = database.GetCollection<BlocoE.RegistroE310>("RegistroE310");
            IMongoCollection<BlocoE.RegistroE311> tabelaE17 = database.GetCollection<BlocoE.RegistroE311>("RegistroE311");
            IMongoCollection<BlocoE.RegistroE312> tabelaE18 = database.GetCollection<BlocoE.RegistroE312>("RegistroE312");
            IMongoCollection<BlocoE.RegistroE313> tabelaE19 = database.GetCollection<BlocoE.RegistroE313>("RegistroE313");
            IMongoCollection<BlocoE.RegistroE316> tabelaE20 = database.GetCollection<BlocoE.RegistroE316>("RegistroE316");
            IMongoCollection<BlocoE.RegistroE500> tabelaE21 = database.GetCollection<BlocoE.RegistroE500>("RegistroE500");
            IMongoCollection<BlocoE.RegistroE510> tabelaE22 = database.GetCollection<BlocoE.RegistroE510>("RegistroE510");
            IMongoCollection<BlocoE.RegistroE520> tabelaE23 = database.GetCollection<BlocoE.RegistroE520>("RegistroE520");
            IMongoCollection<BlocoE.RegistroE530> tabelaE24 = database.GetCollection<BlocoE.RegistroE530>("RegistroE530");
            IMongoCollection<BlocoE.RegistroE990> tabelaE25 = database.GetCollection<BlocoE.RegistroE990>("RegistroE990");


            BlocoE.RegistroE001 registroE001 = new BlocoE.RegistroE001();
            BlocoE.RegistroE100 registroE100 = new BlocoE.RegistroE100();
            BlocoE.RegistroE110 registroE110 = new BlocoE.RegistroE110();
            BlocoE.RegistroE111 registroE111 = new BlocoE.RegistroE111();
            BlocoE.RegistroE112 registroE112 = new BlocoE.RegistroE112();
            BlocoE.RegistroE113 registroE113 = new BlocoE.RegistroE113();
            BlocoE.RegistroE115 registroE115 = new BlocoE.RegistroE115();
            BlocoE.RegistroE116 registroE116 = new BlocoE.RegistroE116();
            BlocoE.RegistroE200 registroE200 = new BlocoE.RegistroE200();
            BlocoE.RegistroE210 registroE210 = new BlocoE.RegistroE210();
            BlocoE.RegistroE220 registroE220 = new BlocoE.RegistroE220();
            BlocoE.RegistroE230 registroE230 = new BlocoE.RegistroE230();
            BlocoE.RegistroE240 registroE240 = new BlocoE.RegistroE240();
            BlocoE.RegistroE250 registroE250 = new BlocoE.RegistroE250();
            BlocoE.RegistroE300 registroE300 = new BlocoE.RegistroE300();
            BlocoE.RegistroE310 registroE310 = new BlocoE.RegistroE310();
            BlocoE.RegistroE311 registroE311 = new BlocoE.RegistroE311();
            BlocoE.RegistroE312 registroE312 = new BlocoE.RegistroE312();
            BlocoE.RegistroE313 registroE313 = new BlocoE.RegistroE313();
            BlocoE.RegistroE316 registroE316 = new BlocoE.RegistroE316();
            BlocoE.RegistroE500 registroE500 = new BlocoE.RegistroE500();
            BlocoE.RegistroE510 registroE510 = new BlocoE.RegistroE510();
            BlocoE.RegistroE520 registroE520 = new BlocoE.RegistroE520();
            BlocoE.RegistroE530 registroE530 = new BlocoE.RegistroE530();
            BlocoE.RegistroE990 registroE990 = new BlocoE.RegistroE990();

            registroE001.IND_MOV = IndMovimento.BlocoSemDados;

            registroE100.DT_INI = DateTime.Now;
            registroE100.DT_FIN = DateTime.Now;

            registroE110.VL_TOT_DEBITOS = 0;
            registroE110.VL_AJ_DEBITOS = 0;
            registroE110.VL_TOT_AJ_DEBITOS = 0;
            registroE110.VL_ESTORNOS_CRED = 0;
            registroE110.VL_TOT_CREDITOS = 0;
            registroE110.VL_AJ_CREDITOS = 0;
            registroE110.VL_TOT_AJ_CREDITOS = 0;
            registroE110.VL_ESTORNOS_DEB = 0;
            registroE110.VL_SLD_CREDOR_ANT = 0;
            registroE110.VL_SLD_APURADO = 0;
            registroE110.VL_TOT_DED = 0;
            registroE110.VL_ICMS_RECOLHER = 0;
            registroE110.VL_SLD_CREDOR_TRANSPORTAR = 0;
            registroE110.DEB_ESP = 0;    
            


            registroE111.CodAjApur = "";
            registroE111.DescrComplAj = "";
            registroE111.VlAjApur = 0;

            registroE112.IndProc = "";
            registroE112.Proc = "";
            registroE112.TxtCompl = "";

            registroE113.CodPart = "";
            registroE113.CodMod = "";
            registroE113.Ser = "";
            registroE113.Sub = "";
            registroE113.NumDoc = 0;
            registroE113.DtDoc = DateTime.Now;
            registroE113.CodItem = "";
            registroE113.VlAjItem = 0;

            registroE115.CodInfAdic = "";
            registroE115.VlInfAdic = 0;
            registroE115.DescrComplAj = "";

            registroE116.CodOr = "";
            registroE116.VlOr = 0;
            registroE116.DtVcto = "";
            registroE116.CodRec = "";
            registroE116.NumProc = "";
            registroE116.IndProc = 0;
            registroE116.Proc = "";
            registroE116.TxtCompl = "";
            registroE116.MesRef = "";

            registroE200.Uf = "";
            registroE200.DtIni = DateTime.Now;
            registroE200.DtFin = DateTime.Now;

            registroE210.IndMovSt = 0;
            registroE210.VlSldCredAntSt = 0;
            registroE210.VlDevolSt = 0;
            registroE210.VlRessarcSt = 0;
            registroE210.VlOutCredSt = 0;
            registroE210.VlAjCreditosSt = 0;
            registroE210.VlRetencaoSt = 0;
            registroE210.VlOutDebSt = 0;
            registroE210.VlAjDebitosSt = 0;
            registroE210.VlSldDevAntSt = 0;
            registroE210.VlDeducoesSt = 0;
            registroE210.VlIcmsRecolSt = 0;
            registroE210.VlSldCredStTransportar = 0;
            registroE210.DebEspSt = 0;


            registroE220.CodAjApur = "";
            registroE220.DescrComplAj = "";
            registroE220.VlAjApur = 0;

            registroE230.NumDa = "";
            registroE230.NumProc = "";
            registroE230.IndProc = 0;
            registroE230.Proc = "";
            registroE230.TxtCompl = "";

            registroE240.CodPart = "";
            registroE240.CodMod = "";
            registroE240.Ser = "";
            registroE240.Sub = 0;
            registroE240.NumDoc = 0;
            registroE240.DtDoc = DateTime.Now;
            registroE240.CodItem = "";
            registroE240.VlAjItem = 0;

            registroE250.CodOr = "";
            registroE250.VlOr = 0;
            registroE250.DtVcto = DateTime.Now;
            registroE250.CodRec = "";
            registroE250.NumProc = "";
            registroE250.IndProc = "";
            registroE250.Proc = "";
            registroE250.TxtCompl = "";
            registroE250.MesRef = DateTime.Now;

            registroE300.Uf = "";
            registroE300.DtIni = DateTime.Now;
            registroE300.DtFun = DateTime.Now;


            registroE310.IndMovDifal = 0;
            registroE310.VlSldCredAntDifal = 0;
            registroE310.VlTotDebitosDifal = 0;
            registroE310.VlOutDebDifal = 0;
            registroE310.VlTotDebFcp = 0;
            registroE310.VlTotCreditosDifal = 0;
            registroE310.VlTotCredFcp = 0;
            registroE310.VlOutCredDifal = 0;
            registroE310.VlSldDevAntDifal = 0;
            registroE310.VlDeducoesDifal = 0;
            registroE310.VlRecol = 0;
            registroE310.VlSldCredTransportar = 0;
            registroE310.DebEspDifal = 0;


            registroE311.CodAjApur = "";
            registroE311.DescrComplAj = "";
            registroE311.VlAjApur = 0;

            registroE312.NumDa = "";
            registroE312.NumProc = "";
            registroE312.INumProc = 0;//Esse campo voi modificado por conta de ter uma outra entidade com mesmo nome.
            registroE312.Proc = "";
            registroE312.TxtCompl = "";

            registroE313.CodPart = "";
            registroE313.CodMod = "";
            registroE313.Ser = "";
            registroE313.Sub = "";
            registroE313.NumDoc = 0;
            registroE313.ChvDocE = "";
            registroE313.DtDoc = DateTime.Now;
            registroE313.CodItem = "";
            registroE313.VlAjItem = 0;

            registroE316.CodOr = "";
            registroE316.VlOr = 0;
            registroE316.DtVcto = DateTime.Now;
            registroE316.CodRec = "";
            registroE316.NumProc = "";
            registroE316.IndProc = 0;
            registroE316.Proc = "";
            registroE316.TxtCompl = "";
            registroE316.MesRef = DateTime.Now;


            registroE500.IND_APUR = "";
            registroE500.DT_INI = DateTime.Now;
            registroE500.DT_FIN = DateTime.Now;

            registroE510.Cfop = 0;
            registroE510.CstIpi = 0;
            registroE510.VlContIpi = 0;
            registroE510.VlBcIpi = 0;
            registroE510.VlIpi = 0;

            registroE520.VlSdAntIpi = 0;
            registroE520.VlDebIpi = 0;
            registroE520.VlCredIpi = 0;
            registroE520.VlOdIpi = 0;
            registroE520.VlOcIpi = 0;
            registroE520.VlScIpi = 0;
            registroE520.VlSdIpi = 0;

            registroE530.IndAj = 0;
            registroE530.VlAj = 0;
            registroE530.CodAj = "";
            registroE530.IndDoc = 0;
            registroE530.NumDoc = "";
            registroE530.DescrAj =  "";

            registroE990.QTD_LIN_E = "";
        }
    }
}
