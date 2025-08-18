using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConttaComsumidor.Infra.Base
{
    public class MongoDBContext<T> where T : class
    {
        public static string ConnectionString { get; set; }
        public static string DatabaseName { get; set; }
        public static bool IsSSL { get; set; }

        public MongoClient Session { get; private set; }

        private IMongoDatabase _database { get; }

        public MongoDBContext()
        {
            try
            {
                BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;
                ConnectionString = Environment.GetEnvironmentVariable("MONGODB_URI");
                DatabaseName = Environment.GetEnvironmentVariable("MONGODB_DATABASE");

                if (string.IsNullOrEmpty(ConnectionString) || string.IsNullOrEmpty(DatabaseName))
                {
                    throw new Exception("MongoDB connection string or database name not set in environment variables.");
                }

                MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(ConnectionString));
                settings.SslSettings = new SslSettings { EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 };

                var client = new MongoClient(settings);
                Session = client;
                _database = client.GetDatabase(DatabaseName);
            }
            catch (Exception ex)
            {
                throw new Exception("N\u00e3o foi possivel acessar o servidor", ex);
            }
        }

        public IMongoCollection<T> GetColection
        {
            get
            {
                return _database.GetCollection<T>(typeof(T).Name);
            }
        }
    }
}
