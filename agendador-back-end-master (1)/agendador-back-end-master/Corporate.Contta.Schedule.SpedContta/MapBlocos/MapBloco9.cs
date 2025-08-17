using Contta.SpedFiscal;
using MongoDB.Driver;

namespace Corporate.Contta.Schedule.SpedContta.MapBlocos
{
    public class MapBloco9
    {
        public void GetBloco9()
        {
            IMongoClient mongoClient = new MongoClient("mongodb://localhost");
            IMongoDatabase database = mongoClient.GetDatabase("Bloco9");
            IMongoCollection<Bloco9.Registro9001> tabela901 = database.GetCollection<Bloco9.Registro9001>("registro9001");
            IMongoCollection<Bloco9.Registro9900> tabela902 = database.GetCollection<Bloco9.Registro9900>("registro9900");
            IMongoCollection<Bloco9.Registro9990> tabela903 = database.GetCollection<Bloco9.Registro9990>("registro9990");
            IMongoCollection<Bloco9.Registro9999> tabela904 = database.GetCollection<Bloco9.Registro9999>("registro9999");

            Bloco9.Registro9001 registro9001 = new Bloco9.Registro9001();
            Bloco9.Registro9900 registro9900 = new Bloco9.Registro9900();
            Bloco9.Registro9990 registro9990 = new Bloco9.Registro9990();
            Bloco9.Registro9999 registro9999 = new Bloco9.Registro9999();
        }
    }
}
