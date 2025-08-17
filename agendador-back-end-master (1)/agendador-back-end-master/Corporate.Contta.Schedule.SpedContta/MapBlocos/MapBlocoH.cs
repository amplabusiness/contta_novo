using Contta.SpedFiscal;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.SpedContta.MapBlocos
{
    public class MapBlocoH
    {
        public void GetBlocoH()
        {
            IMongoClient mongoClient = new MongoClient("mongodb://localhost");
            IMongoDatabase database = mongoClient.GetDatabase("Bloco0");
            IMongoCollection<BlocoH.RegistroH001> tabelaH01 = database.GetCollection<BlocoH.RegistroH001>("registroH001");
            IMongoCollection<BlocoH.RegistroH005> tabelaH02 = database.GetCollection<BlocoH.RegistroH005>("registroH005");
            IMongoCollection<BlocoH.RegistroH010> tabelaH03 = database.GetCollection<BlocoH.RegistroH010>("registroH010");
            IMongoCollection<BlocoH.RegistroH020> tabelaH04 = database.GetCollection<BlocoH.RegistroH020>("registroH020");
            IMongoCollection<BlocoH.RegistroH990> tabelaH05 = database.GetCollection<BlocoH.RegistroH990>("registroH990");

            BlocoH.RegistroH001 registroH001 = new BlocoH.RegistroH001();
            BlocoH.RegistroH005 registroH005 = new BlocoH.RegistroH005();
            BlocoH.RegistroH010 registroH010 = new BlocoH.RegistroH010();
            BlocoH.RegistroH020 registroH020 = new BlocoH.RegistroH020();
            BlocoH.RegistroH990 registroH990 = new BlocoH.RegistroH990();
        }
    }
}
