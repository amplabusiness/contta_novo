using MongoDB.Driver;

namespace Corporate.Contta.Schedule.SpedContta.Data.Repository
{
    internal class MongoDBRespository
    {
       
       public MongoClient client;

        public IMongoDatabase db;

        public MongoDBRespository()
        {
            var connectionString = Environment.GetEnvironmentVariable("MONGODB_URI");
            var databaseName = Environment.GetEnvironmentVariable("MONGODB_DATABASE");
            if (string.IsNullOrEmpty(connectionString) || string.IsNullOrEmpty(databaseName))
            {
                throw new Exception("MongoDB connection string or database name not set in environment variables.");
            }
            this.client = new MongoClient(connectionString);
            this.db = this.client.GetDatabase(databaseName);
        }
    }
}
