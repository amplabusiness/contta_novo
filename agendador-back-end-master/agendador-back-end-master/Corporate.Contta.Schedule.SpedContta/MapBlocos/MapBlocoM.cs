using Contta.EfdContribuicoes;
using MongoDB.Driver;

namespace Corporate.Contta.Schedule.SpedContta.MapBlocos
{
    public class MapBlocoM
    {

        public void GetBlocoF()
        {
            IMongoClient mongoClient = new MongoClient("mongodb://localhost");
            IMongoDatabase database = mongoClient.GetDatabase("BlocoM");
            IMongoCollection<BlocoM.RegistroM600> tabela01 = database.GetCollection<BlocoM.RegistroM600>("RegistroM600");


            BlocoM.RegistroM100 registroM100 = new BlocoM.RegistroM100();         
            BlocoM.RegistroM200 registroM200 = new BlocoM.RegistroM200();         
            BlocoM.RegistroM500 registroM500 = new BlocoM.RegistroM500();
            BlocoM.RegistroM600 registroM600 = new BlocoM.RegistroM600();
            BlocoM.RegistroM800 registroM800 = new BlocoM.RegistroM800();

            registroM100.COD_CRED = "";
            registroM100.IND_CRED_ORI = "";
            registroM100.VL_BC_PIS = 0;
            registroM100.ALIQ_PIS = 0;
            registroM100.QUANT_BC_PIS = 0;
            registroM100.ALIQ_PIS_QUANT = 0;
            registroM100.VL_CRED = 0;
            registroM100.VL_AJUS_ACRES = 0;
            registroM100.VL_AJUS_REDUC = 0;
            registroM100.VL_CRED_DIF = 0;
            registroM100.VL_CRED_DISP = 0;
            registroM100.IND_DESC_CRED = 0;
            registroM100.VL_CRED_DESC = 0;
            registroM100.SLD_CRED = 0;


            registroM200.VlTotContNcPer = 0;
            registroM200.VlTotCredDesc = 0;
            registroM200.VlTotCredDescAnt = 0;
            registroM200.VlTotContNcDev = 0;
            registroM200.VlRetNc = 0;
            registroM200.VlOutDedNc = 0;
            registroM200.VlContNcRec = 0;
            registroM200.VlTotContCumPer = 0;
            registroM200.VlRetCum = 0;
            registroM200.VlOutDedCum = 0;
            registroM200.VlContCumRec = 0;
            registroM200.VlTotContRec = 0;

            registroM500.COD_CRED = "";
            registroM500.IND_CRED_ORI = 0;
            registroM500.VL_BC_COFINS = 0;
            registroM500.ALIQ_COFINS = 0;
            registroM500.QUANT_BC_COFINS = 0;
            registroM500.ALIQ_COFINS_QUANT = 0;
            registroM500.VL_CRED = 0;
            registroM500.VL_AJUS_ACRES = 0;
            registroM500.VL_AJUS_REDUC = 0;
            registroM500.VL_CRED_DIFER = 0;
            registroM500.VL_CRED_DISP = 0;
            registroM500.IND_DESC_CRED = "";
            registroM500.VL_CRED_DESC = 0;
            registroM500.SLD_CRED = 0;

            registroM600.VlTotContNcPer = 0;
            registroM600.VlTotCredDesc = 0;
            registroM600.VlTotCredDescAnt = 0;
            registroM600.VlTotContNcDev = 0;
            registroM600.VlRetNc = 0;
            registroM600.VlOutDedNc = 0;
            registroM600.VlContNcRec = 0;
            registroM600.VlTotContCumPer = 0;
            registroM600.VlRetCum = 0;
            registroM600.VlOutDedCum = 0;
            registroM600.VlContCumRec = 0;
            registroM600.VlTotContRec = 0;


            registroM800.CST_COFINS = "";
            registroM800.VL_TOT_REC = 0;
            registroM800.COD_CTA = 0;
            registroM800.DESC_COMPL = 0;
        }

    }
}
