using Corporate.Contta.Schedule.Domain.Entities.UserAgg;
using Corporate.Contta.Schedule.Domain.Enum;
using MongoDB.Bson;
using MongoDB.Driver;
using System;

namespace Corporate.Contta.Schedule.Infra.Repositories.Base
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
                _database = client.GetDatabase(DatabaseName);
                //ToDo:Ver se vai ser necessario validar usuario master
                //CreateUserDefault(_database);
            }
            catch (Exception ex)
            {
                throw new Exception("N\u00e3o foi possivel acessar o servidor", ex);
            }
        }

        //private void CreateUserDefault(IMongoDatabase database)
        //{
        //    if (!CheckUserExist(database))
        //    {
        //        var user = new User(UserGroup.Administrator, "admin", "00000000000", "admin@173.255.209.202", "manager", null, true, null, null);
        //        user.Id = Guid.NewGuid();
        //        database.GetCollection<User>("User").InsertOne(user);
        //    }           
        //}

        private bool CheckUserExist(IMongoDatabase database)
        {
            var result = database.GetCollection<User>("User").Find(c => c.Name.Equals("admin") && c.Email.Equals("admin@173.255.209.202"));
            return  result.Any();    

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
