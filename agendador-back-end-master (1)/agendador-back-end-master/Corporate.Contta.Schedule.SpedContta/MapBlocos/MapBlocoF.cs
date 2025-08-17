using Contta.EfdContribuicoes;
using MongoDB.Driver;
using System;

namespace Corporate.Contta.Schedule.SpedContta.MapBlocos
{
    public class MapBlocoF
    {
        public void GetBlocoF()
        {
            IMongoClient mongoClient = new MongoClient("mongodb://localhost");
            IMongoDatabase database = mongoClient.GetDatabase("BlocoF");
            IMongoCollection<BlocoF.RegistroF600> tabela01 = database.GetCollection<BlocoF.RegistroF600>("RegistroF600");

            BlocoF.RegistroF600 registroF600 = new BlocoF.RegistroF600();

            registroF600.IndNatRet = 99;
            registroF600.DtRet = DateTime.Now;
            registroF600.VlBcRet = 0;
            registroF600.VlRet = 0;
            registroF600.CodRec = "";
            registroF600.IndNatRec = 0;
            registroF600.Cnpj = "";
            registroF600.VlRetPis = 0;
            registroF600.VlRetCofins = 0;
            registroF600.IndDec = 0;

            tabela01.InsertOne(registroF600);
        }
    }
}
