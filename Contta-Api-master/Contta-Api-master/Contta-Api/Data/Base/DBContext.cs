using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConttaComsumidor.Infra.Base
{
    public class DBContext<T> where T : class
    {
        public static string ConnectionString { get; set; }
        public static string DatabaseName { get; set; }
        public static bool IsSSL { get; set; }

        public MongoClient Session { get; private set; }

        private IMongoDatabase _database { get; }

        public DBContext()
        {
            try
            {
                BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;
                //ConnectionString = "mongodb+srv://contta:j0noYr3DeOpbvX8U@cluster0.e6lgw.mongodb.net/conttadb?retryWrites=true&w=majority";
                ConnectionString = "mongodb://contta:contta123456@192.46.218.34:27017/?authSource=admin&readPreference=primary&ssl=false";
                DatabaseName = "conttadb";

                //ConnectionString = "mongodb://localhost:27017/";
                //DatabaseName = "conttadb";

                MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(ConnectionString));
                //comentar essa linha se for executar no banco local.
                settings.SslSettings = new SslSettings { EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 };

                var client = new MongoClient(settings);
                Session = client;
                _database = client.GetDatabase(DatabaseName);
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
