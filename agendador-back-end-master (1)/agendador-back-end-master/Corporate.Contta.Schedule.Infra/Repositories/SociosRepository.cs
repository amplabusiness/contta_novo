using AutoMapper;
using Corporate.Contta.Schedule.Domain.Contracts.Repositories;
using Corporate.Contta.Schedule.Domain.Entities;
using Corporate.Contta.Schedule.Domain.Entities.CompanyInformationAgg;
using Corporate.Contta.Schedule.Infra.Repositories.Base;
using MongoDB.Bson;
using MongoDB.Driver;
using Sanatana.MongoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Infra.Repositories
{
    public class SociosRepository : ISociosRepository
    {

        private readonly IMongoDatabase _database = MongoClient();
        private readonly IMongoDatabase _databaseContta = MongoClientContta();
        private readonly IMapper _mapper;
        private MongoDbConnectionSettings _settings;

        public static IMongoDatabase MongoClient()
        {

            IMongoClient mongoClient = new MongoClient("mongodb://contta:contta123456@192.46.218.34:27017/?authSource=admin&readPreference=primary&ssl=false");
            IMongoDatabase database = mongoClient.GetDatabase("BaseCnpjContta");

            return database;
        }

        public static IMongoDatabase MongoClientContta()
        {

            IMongoClient mongoClient = new MongoClient("mongodb://contta:contta123456@192.46.218.34:27017/?authSource=admin&readPreference=primary&ssl=false");
            IMongoDatabase database = mongoClient.GetDatabase("conttadb");

            return database;
        }

        public SociosRepository(IMapper mapper)
        {
            _mapper = mapper;

        }

        public virtual IMongoCollection<TEntity> GetCollection<TEntity>()
        {
            Type entityType = typeof(TEntity);
            var entityMappings = _collectionNames.Where(x => x.type == entityType).ToArray();

            if (entityMappings.Length == 0)
            {
                throw new KeyNotFoundException($"Entity type [{entityType.FullName}] is not registered as a MongoDb collection. First add an entry to {nameof(_collectionNames)}");
            }
            if (entityMappings.Length > 1)
            {
                throw new KeyNotFoundException($"Entity type [{entityType.FullName}] is mapped to more than one collection. Use {nameof(GetCollection)} method with collectionName argument.");
            }

            string collectionName = entityMappings.First().collectionName;
            IMongoCollection<TEntity> collection = _database.GetCollection<TEntity>(
                _settings.CollectionsPrefix + collectionName);
            return collection;
        }

        public virtual void CreateSociosIndex()
        {
            ListDiretorios listDiretorios = new ListDiretorios();
            var listaDiretorio = listDiretorios.GetListaDiretorio();

            foreach (var item in listaDiretorio)
            {

                IMongoCollection<Socios<ObjectId>> _context = _database.GetCollection<Socios<ObjectId>>($"{item}");

                IndexKeysDefinition<Socios<ObjectId>> subscriberIndex = Builders<Socios<ObjectId>>.IndexKeys
                   .Ascending(p => p.cnpj_cpf_socio)
                   .Ascending(p => p.nome_socio);
                CreateIndexOptions subscriberOptions = new CreateIndexOptions()
                {
                    Unique = false
                };

                var model = new CreateIndexModel<Socios<ObjectId>>(subscriberIndex, subscriberOptions);

                IMongoCollection<Socios<ObjectId>> collection = _context;
                string subscriberName = collection.Indexes.CreateOne(model);
            }

        }

        public async Task InsertSocios(string cpf, string nome)
        {
            try
            {
                IMongoCollection<Socios<ObjectId>> _collectionSocios = _databaseContta.GetCollection<Socios<ObjectId>>("sociosCompany");
                var listSocios = new List<Socios<ObjectId>>();
                ListDiretorios listDiretorios = new ListDiretorios();
                var listaDiretorio = listDiretorios.GetListaDiretorio();
                //CreateSociosIndex();

                foreach (var item in listaDiretorio)
                {
                    IMongoCollection<Socios<ObjectId>> _collection = _database.GetCollection<Socios<ObjectId>>($"{item}");

                    var dadosSocios = _collection.Find(c => c.nome_socio.Equals(nome) && c.cnpj_cpf_socio.Equals(cpf)).ToList();

                    if (dadosSocios.Count > 0)
                    {
                        _collectionSocios.InsertMany(dadosSocios);
                    }
                }             
            }
            catch (Exception ex)
            {

                throw;
            }          
        }

        public async Task<List<Socios<ObjectId>>> GetAllSociosContta(string cpf, string nome)
        {
            CreateSociosConttaIndex();
            IMongoCollection<Socios<ObjectId>> _collectionSocios = _databaseContta.GetCollection<Socios<ObjectId>>("sociosCompany");

            var dadosSocios = _collectionSocios.Find(c => c.nome_socio.Equals(nome) && c.cnpj_cpf_socio.Equals(cpf)).ToList();

            return dadosSocios;
        }

        private (Type type, string collectionName)[] _collectionNames = new (Type type, string collectionName)[]
       {
            ( typeof(Socios<ObjectId>), "socios" ),
       };


        public virtual void CreateSociosConttaIndex()
        {
            ListDiretorios listDiretorios = new ListDiretorios();
            var listaDiretorio = listDiretorios.GetListaDiretorio();

            IMongoCollection<Socios<ObjectId>> _collectionSocios = _databaseContta.GetCollection<Socios<ObjectId>>("sociosCompany");

            IndexKeysDefinition<Socios<ObjectId>> subscriberIndex = Builders<Socios<ObjectId>>.IndexKeys
               .Ascending(p => p.cnpj_cpf_socio)
               .Ascending(p => p.nome_socio);
            CreateIndexOptions subscriberOptions = new CreateIndexOptions()
            {
                Unique = false
            };

            var model = new CreateIndexModel<Socios<ObjectId>>(subscriberIndex, subscriberOptions);

            IMongoCollection<Socios<ObjectId>> collection = _collectionSocios;
            string subscriberName = collection.Indexes.CreateOne(model);

        }
    }
}
