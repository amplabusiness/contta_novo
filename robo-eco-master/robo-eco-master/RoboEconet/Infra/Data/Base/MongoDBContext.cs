using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace XmlCteNfeNfsToClass.Infra.Base
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
                // BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;

                // new MongoClient("mongodb://contta:contta123456@173.255.209.202:27017/?authSource=admin&readPreference=primary&appname=POC&ssl=false");
                // DatabaseName = "conttadb";

                // //ConnectionString = "mongodb://localhost:27017/";
                // //DatabaseName = "conttadb";

                // MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(ConnectionString));
                // //comentar essa linha se for executar no banco local.
                //// settings.SslSettings = new SslSettings { EnabledSslProtocols = System.Security.Authentication.SslProtocols.None };


                var client = new MongoClient("mongodb://localhost:27017/");
                Session = client;
                _database = client.GetDatabase("tbGeralImp");
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possivel acessar o servidor", ex);
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
