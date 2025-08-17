using Contta.Common;
using Contta.SpedFiscal;
using Microsoft.VisualBasic;
using MongoDB.Driver;
using System;

namespace Corporate.Contta.Schedule.SpedContta.MapBlocos
{
    public class MapBlocoB
    {
        public void GetBlocoB()
        {
            IMongoClient mongoClient = new MongoClient("mongodb://localhost");
            IMongoDatabase database = mongoClient.GetDatabase("BlocoB");
            IMongoCollection<BlocoB.RegistroB001>  tabelaB01 = database.GetCollection<BlocoB.RegistroB001>("RegistroB001");
            IMongoCollection<BlocoB.RegistroB020>  tabelaB02 = database.GetCollection<BlocoB.RegistroB020>("RegistroB020");
            IMongoCollection<BlocoB.RegistroB025>  tabelaB03 = database.GetCollection<BlocoB.RegistroB025>("RegistroB025");
            IMongoCollection<BlocoB.RegistroB030>  tabelaB04 = database.GetCollection<BlocoB.RegistroB030>("RegistroB030");
            IMongoCollection<BlocoB.RegistroB035>  tabelaB05 = database.GetCollection<BlocoB.RegistroB035>("RegistroB035");
            IMongoCollection<BlocoB.RegistroB050>  tabelaB06 = database.GetCollection<BlocoB.RegistroB050>("RegistroB050");
            IMongoCollection<BlocoB.RegistroB0420> tabelaB07 = database.GetCollection<BlocoB.RegistroB0420>("RegistroB0420");
            IMongoCollection<BlocoB.RegistroB0440> tabelaB08 = database.GetCollection<BlocoB.RegistroB0440>("RegistroB0440");
            IMongoCollection<BlocoB.RegistroB0460> tabelaB09 = database.GetCollection<BlocoB.RegistroB0460>("RegistroB0460");
            IMongoCollection<BlocoB.RegistroB0470> tabelaB10 = database.GetCollection<BlocoB.RegistroB0470>("RegistroB0470");
            IMongoCollection<BlocoB.RegistroB0500> tabelaB11 = database.GetCollection<BlocoB.RegistroB0500>("RegistroB0500");
            IMongoCollection<BlocoB.RegistroB0510> tabelaB12 = database.GetCollection<BlocoB.RegistroB0510>("RegistroB0510");

            BlocoB.RegistroB001 registroB001 = new BlocoB.RegistroB001();
            BlocoB.RegistroB020 registroB020 = new BlocoB.RegistroB020();
            BlocoB.RegistroB025 registroB025 = new BlocoB.RegistroB025();
            BlocoB.RegistroB030 registroB030 = new BlocoB.RegistroB030();
            BlocoB.RegistroB035 registroB035 = new BlocoB.RegistroB035();
            BlocoB.RegistroB050 registroB050 = new BlocoB.RegistroB050();
            BlocoB.RegistroB0420 registroB0420 = new BlocoB.RegistroB0420();
            BlocoB.RegistroB0440 registroB0440 = new BlocoB.RegistroB0440();
            BlocoB.RegistroB0460 registroB0460 = new BlocoB.RegistroB0460();
            BlocoB.RegistroB0470 registroB0470 = new BlocoB.RegistroB0470();
            BlocoB.RegistroB0500 registroB0500 = new BlocoB.RegistroB0500();
            BlocoB.RegistroB0510 registroB0510 = new BlocoB.RegistroB0510();

            registroB001.IndDad = IndMovimento.BlocoComDados;

            registroB020.IndOper = IndOper.BlocoEmissTerc;
            registroB020.CodParticipante = 0;
            registroB020.CodModeloFiscal = 0;
            registroB020.CodSitDocu = 0;
            registroB020.SerieDoc = 0;
            registroB020.NumeroDocum = 0;
            registroB020.ChvNfe = "";
            registroB020.DtDocumento = DateTime.Now;
            registroB020.CodMuniciSever = "";
            registroB020.ValorCont = 0;
            registroB020.ValorMatTerceiros = 0;
            registroB020.ValorSubempreita = 0;
            registroB020.ValorIsentaIsss = 0;
            registroB020.ValorDedBaseCalcu = 0;
            registroB020.ValorBaseCalcu = 0;
            registroB020.ValorBaseCalcuRet = 0;
            registroB020.ValorRetTomador = 0;
            registroB020.ValorIssDest = 0;
            registroB020.CodLanFiscal = "";

            registroB025.ValorContParcelas = 0;
            registroB025.ValorCalIss = 0;
            registroB025.AlinqIss = 0;
            registroB025.ValorIss = 0;
            registroB025.ValorOperInsentas = 0;
            registroB025.CodServico = 0;

            registroB030.CodModeloFiscal = "";
            registroB030.QtdCancelados = 0;





        }
    }
}
