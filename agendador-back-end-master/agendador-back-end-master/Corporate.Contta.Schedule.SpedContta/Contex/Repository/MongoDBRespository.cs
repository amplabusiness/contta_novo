using MongoDB.Driver;

namespace Corporate.Contta.Schedule.SpedContta.Data.Repository
{
    internal class MongoDBRespository
    {
       
       public MongoClient client;

        public IMongoDatabase db;

        public MongoDBRespository()
        {

            this.client = new MongoClient("mongodb+srv://thiago:thiago@agendador.f65ge.mongodb.net/conttadb?connect=replicaSet&retryWrites=true&w=majority");

            this.db = this.client.GetDatabase("conttadb");
        }
    }
}
