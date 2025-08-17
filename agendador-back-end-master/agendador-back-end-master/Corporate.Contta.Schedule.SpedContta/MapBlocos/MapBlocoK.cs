using Contta.SpedFiscal;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.SpedContta.MapBlocos
{
   public class MapBlocoK
    {
        public void GetBLocoK()
        {
            IMongoClient mongoClient = new MongoClient("mongodb://localhost");
            IMongoDatabase database = mongoClient.GetDatabase("BlocoK");
            IMongoCollection<BlocoK.RegistroK001> tabelaK01 = database.GetCollection<BlocoK.RegistroK001>("registroK001");
            IMongoCollection<BlocoK.RegistroK100> tabelaK02 = database.GetCollection<BlocoK.RegistroK100>("RegistroK100");
            IMongoCollection<BlocoK.RegistroK200> tabelaK03 = database.GetCollection<BlocoK.RegistroK200>("RegistroK200");
            IMongoCollection<BlocoK.RegistroK220> tabelaK04 = database.GetCollection<BlocoK.RegistroK220>("RegistroK220");
            IMongoCollection<BlocoK.RegistroK230> tabelaK05 = database.GetCollection<BlocoK.RegistroK230>("RegistroK230");
            IMongoCollection<BlocoK.RegistroK235> tabelaK06 = database.GetCollection<BlocoK.RegistroK235>("RegistroK235");
            IMongoCollection<BlocoK.RegistroK250> tabelaK07 = database.GetCollection<BlocoK.RegistroK250>("RegistroK250");
            IMongoCollection<BlocoK.RegistroK255> tabelaK08 = database.GetCollection<BlocoK.RegistroK255>("RegistroK255");
            //IMongoCollection<BlocoK.RegistroK260> tabelaK09 = database.GetCollection<BlocoK.RegistroK260>("RegistroK260");
            //IMongoCollection<BlocoK.RegistroK265> tabelaK10 = database.GetCollection<BlocoK.RegistroK265>("RegistroK265");
            //IMongoCollection<BlocoK.RegistroK270> tabelaK11 = database.GetCollection<BlocoK.RegistroK270>("RegistroK270");
            //IMongoCollection<BlocoK.RegistroK275> tabelaK12 = database.GetCollection<BlocoK.RegistroK275>("RegistroK275");
            //IMongoCollection<BlocoK.RegistroK280> tabelaK13 = database.GetCollection<BlocoK.RegistroK280>("RegistroK280");
            //IMongoCollection<BlocoK.RegistroK290> tabelaK14 = database.GetCollection<BlocoK.RegistroK290>("RegistroK290");
            //IMongoCollection<BlocoK.RegistroK291> tabelaK15 = database.GetCollection<BlocoK.RegistroK291>("RegistroK291");
            //IMongoCollection<BlocoK.RegistroK292> tabelaK16 = database.GetCollection<BlocoK.RegistroK292>("RegistroK292");
            //IMongoCollection<BlocoK.RegistroK300> tabelaK17 = database.GetCollection<BlocoK.RegistroK300>("RegistroK300");
            //IMongoCollection<BlocoK.RegistroK301> tabelaK18 = database.GetCollection<BlocoK.RegistroK301>("RegistroK301");
            //IMongoCollection<BlocoK.RegistroK302> tabelaK19 = database.GetCollection<BlocoK.RegistroK302>("RegistroK302");
            IMongoCollection<BlocoK.RegistroK990> tabelaK20 = database.GetCollection<BlocoK.RegistroK990>("RegistroK990");

            BlocoK.RegistroK001 registroK001 = new BlocoK.RegistroK001();
            BlocoK.RegistroK100 registroK100 = new BlocoK.RegistroK100();
            BlocoK.RegistroK200 registroK200 = new BlocoK.RegistroK200();
            BlocoK.RegistroK220 registroK220 = new BlocoK.RegistroK220();
            BlocoK.RegistroK230 registroK230 = new BlocoK.RegistroK230();
            BlocoK.RegistroK235 registroK235 = new BlocoK.RegistroK235();
            BlocoK.RegistroK250 registroK250 = new BlocoK.RegistroK250();
            BlocoK.RegistroK255 registroK255 = new BlocoK.RegistroK255();
            //BlocoK.RegistroK260 registroK260 = new BlocoK.RegistroK260();
            //BlocoK.RegistroK265 registroK265 = new BlocoK.RegistroK265();
            //BlocoK.RegistroK270 registroK270 = new BlocoK.RegistroK270();
            //BlocoK.RegistroK275 registroK275 = new BlocoK.RegistroK275();
            //BlocoK.RegistroK280 registroK280 = new BlocoK.RegistroK280();
            //BlocoK.RegistroK290 registroK290 = new BlocoK.RegistroK290();
            //BlocoK.RegistroK291 registroK291 = new BlocoK.RegistroK291();
            //BlocoK.RegistroK292 registroK292 = new BlocoK.RegistroK292();
            //BlocoK.RegistroK300 registroK300 = new BlocoK.RegistroK300();
            //BlocoK.RegistroK301 registroK301 = new BlocoK.RegistroK301();
            //BlocoK.RegistroK302 registroK302 = new BlocoK.RegistroK302();
            BlocoK.RegistroK990 registroK990 = new BlocoK.RegistroK990();
        }
    }
}
