
using Contta.SpedFiscal;
using MongoDB.Driver;

namespace Corporate.Contta.Schedule.SpedContta.MapBlocos
{
    public class MapBloco01
    {
        public void GetBloco01()
        {
            IMongoClient mongoClient = new MongoClient("mongodb://localhost");
            IMongoDatabase database = mongoClient.GetDatabase("Bloco01");
            IMongoCollection<Bloco1.Registro1001> tabela01 = database.GetCollection<Bloco1.Registro1001>("registro0001");
            IMongoCollection<Bloco1.Registro1010> tabela02 = database.GetCollection<Bloco1.Registro1010>("registro1010");
            IMongoCollection<Bloco1.Registro1100> tabela03 = database.GetCollection<Bloco1.Registro1100>("registro1100");
            IMongoCollection<Bloco1.Registro1105> tabela04 = database.GetCollection<Bloco1.Registro1105>("registro1105");
            IMongoCollection<Bloco1.Registro1110> tabela05 = database.GetCollection<Bloco1.Registro1110>("registro1110");
            IMongoCollection<Bloco1.Registro1200> tabela06 = database.GetCollection<Bloco1.Registro1200>("registro1200");
            IMongoCollection<Bloco1.Registro1210> tabela07 = database.GetCollection<Bloco1.Registro1210>("registro1210");
            IMongoCollection<Bloco1.Registro1300> tabela08 = database.GetCollection<Bloco1.Registro1300>("registro1300");
            IMongoCollection<Bloco1.Registro1310> tabela09 = database.GetCollection<Bloco1.Registro1310>("registro1310");
            IMongoCollection<Bloco1.Registro1320> tabela10 = database.GetCollection<Bloco1.Registro1320>("registro1320");
            IMongoCollection<Bloco1.Registro1350> tabela11 = database.GetCollection<Bloco1.Registro1350>("registro1350");
            IMongoCollection<Bloco1.Registro1360> tabela12 = database.GetCollection<Bloco1.Registro1360>("registro1350");
            IMongoCollection<Bloco1.Registro1370> tabela13 = database.GetCollection<Bloco1.Registro1370>("registro1370");
            IMongoCollection<Bloco1.Registro1390> tabela14 = database.GetCollection<Bloco1.Registro1390>("registro1390");
            IMongoCollection<Bloco1.Registro1391> tabela15 = database.GetCollection<Bloco1.Registro1391>("registro1391");
            IMongoCollection<Bloco1.Registro1400> tabela16 = database.GetCollection<Bloco1.Registro1400>("registro1400");
            IMongoCollection<Bloco1.Registro1500> tabela17 = database.GetCollection<Bloco1.Registro1500>("registro1500");
            IMongoCollection<Bloco1.Registro1600> tabela18 = database.GetCollection<Bloco1.Registro1600>("registro1600");
            IMongoCollection<Bloco1.Registro1700> tabela19 = database.GetCollection<Bloco1.Registro1700>("registro1700");
            IMongoCollection<Bloco1.Registro1710> tabela20 = database.GetCollection<Bloco1.Registro1710>("registro1710");
            IMongoCollection<Bloco1.Registro1800> tabela21 = database.GetCollection<Bloco1.Registro1800>("registro1800");
            IMongoCollection<Bloco1.Registro1900> tabela22 = database.GetCollection<Bloco1.Registro1900>("registro1900");
            IMongoCollection<Bloco1.Registro1910> tabela23 = database.GetCollection<Bloco1.Registro1910>("registro1910");
            IMongoCollection<Bloco1.Registro1920> tabela24 = database.GetCollection<Bloco1.Registro1920>("registro1920");
            IMongoCollection<Bloco1.Registro1921> tabela25 = database.GetCollection<Bloco1.Registro1921>("registro1921");
            IMongoCollection<Bloco1.Registro1922> tabela26 = database.GetCollection<Bloco1.Registro1922>("registro1922");
            IMongoCollection<Bloco1.Registro1923> tabela27 = database.GetCollection<Bloco1.Registro1923>("registro1923");
            IMongoCollection<Bloco1.Registro1925> tabela28 = database.GetCollection<Bloco1.Registro1925>("registro1925");
            IMongoCollection<Bloco1.Registro1926> tabela29 = database.GetCollection<Bloco1.Registro1926>("registro1926");
            IMongoCollection<Bloco1.Registro1990> tabela30 = database.GetCollection<Bloco1.Registro1990>("registro1990");


            Bloco1.Registro1001 registro1001 = new Bloco1.Registro1001();
            Bloco1.Registro1010 registro1010 = new Bloco1.Registro1010();
            Bloco1.Registro1100 registro1100 = new Bloco1.Registro1100();
            Bloco1.Registro1105 registro1105 = new Bloco1.Registro1105();
            Bloco1.Registro1110 registro1110 = new Bloco1.Registro1110();
            Bloco1.Registro1200 registro1200 = new Bloco1.Registro1200();
            Bloco1.Registro1210 registro1210 = new Bloco1.Registro1210();
            Bloco1.Registro1300 registro1300 = new Bloco1.Registro1300();
            Bloco1.Registro1310 registro1310 = new Bloco1.Registro1310();
            Bloco1.Registro1320 registro1320 = new Bloco1.Registro1320();
            Bloco1.Registro1350 registro1350 = new Bloco1.Registro1350();
            Bloco1.Registro1360 registro1360 = new Bloco1.Registro1360();
            Bloco1.Registro1370 registro1370 = new Bloco1.Registro1370();
            Bloco1.Registro1390 registro1390 = new Bloco1.Registro1390();
            Bloco1.Registro1391 registro1391 = new Bloco1.Registro1391();
            Bloco1.Registro1400 registro1400 = new Bloco1.Registro1400();
            Bloco1.Registro1500 registro1500 = new Bloco1.Registro1500();
            Bloco1.Registro1510 registro1510 = new Bloco1.Registro1510();
            Bloco1.Registro1600 registro1600 = new Bloco1.Registro1600();
            Bloco1.Registro1700 registro1700 = new Bloco1.Registro1700();
            Bloco1.Registro1710 registro1710 = new Bloco1.Registro1710();
            Bloco1.Registro1800 registro1800 = new Bloco1.Registro1800();
            Bloco1.Registro1900 registro1900 = new Bloco1.Registro1900();
            Bloco1.Registro1910 registro1910 = new Bloco1.Registro1910();
            Bloco1.Registro1920 registro1920 = new Bloco1.Registro1920();
            Bloco1.Registro1921 registro1921 = new Bloco1.Registro1921();
            Bloco1.Registro1922 registro1922 = new Bloco1.Registro1922();
            Bloco1.Registro1923 registro1923 = new Bloco1.Registro1923();
            Bloco1.Registro1925 registro1925 = new Bloco1.Registro1925();
            Bloco1.Registro1926 registro1926 = new Bloco1.Registro1926();
            Bloco1.Registro1990 registro1990 = new Bloco1.Registro1990();
        }
    }
}
