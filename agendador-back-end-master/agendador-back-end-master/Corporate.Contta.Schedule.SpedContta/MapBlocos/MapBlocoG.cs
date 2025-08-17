using Contta.SpedFiscal;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.SpedContta
{
    public class MapBlocoG
    {
        public void GetBlocoG()
        {
            IMongoClient mongoClient = new MongoClient("mongodb://localhost");
            IMongoDatabase database = mongoClient.GetDatabase("Bloco0");
            IMongoCollection<BlocoG.RegistroG001> tabela01 = database.GetCollection<BlocoG.RegistroG001>("registro0001");
            IMongoCollection<BlocoG.RegistroG110> tabela02 = database.GetCollection<BlocoG.RegistroG110>("registro0100");
            IMongoCollection<BlocoG.RegistroG125> tabela03 = database.GetCollection<BlocoG.RegistroG125>("registro0125");
            IMongoCollection<BlocoG.RegistroG126> tabela04 = database.GetCollection<BlocoG.RegistroG126>("registro0126");
            IMongoCollection<BlocoG.RegistroG130> tabela05 = database.GetCollection<BlocoG.RegistroG130>("registro0130");
            IMongoCollection<BlocoG.RegistroG140> tabela06 = database.GetCollection<BlocoG.RegistroG140>("registro0140");
            IMongoCollection<BlocoG.RegistroG990> tabeka07 = database.GetCollection<BlocoG.RegistroG990>("registro990");

            BlocoG.RegistroG001 registroG001 = new BlocoG.RegistroG001();
            BlocoG.RegistroG110 registroG110 = new BlocoG.RegistroG110();
            BlocoG.RegistroG125 registroG125 = new BlocoG.RegistroG125();
            BlocoG.RegistroG126 registroG126 = new BlocoG.RegistroG126();
            BlocoG.RegistroG130 registroG130 = new BlocoG.RegistroG130();
            BlocoG.RegistroG140 registroG140 = new BlocoG.RegistroG140();
            BlocoG.RegistroG990 registroG990 = new BlocoG.RegistroG990();
        }
    }
}
