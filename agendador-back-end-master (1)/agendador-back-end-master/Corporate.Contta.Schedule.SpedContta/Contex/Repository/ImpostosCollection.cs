using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using Corporate.Contta.Schedule.SpedContta.Contex.NfeDTOs;
using System;

namespace Corporate.Contta.Schedule.SpedContta.Data.Repository
{
    public class ImpostosCollection
    {
        internal MongoDBRespository _repo = new MongoDBRespository();
        public IMongoCollection<ImportosDTO> Collection;

        public ImpostosCollection()
        {
            // here we were created new Collection with Products name
            this.Collection = _repo.db.GetCollection<ImportosDTO>("Impostos");
        }

        public void InsertContact(ImportosDTO contact)
        {
            this.Collection.InsertOneAsync(contact);
        }

        public List<ImportosDTO> GetAllImposto()
        {
            var query = this.Collection
                .Find(new BsonDocument())
                .ToListAsync();
            return query.Result;
        }

        public ImportosDTO GetAllProdutos(string id)
        {
            var StatusTrayder = this.Collection.Find(
                    new BsonDocument { { "_id", new ObjectId(id) } })
                    .FirstAsync()
                    .Result;
            return StatusTrayder;
        }

        public void DeleteContact(string EmpresaEmitente)
        {
            ImportosDTO contact = new ImportosDTO();
            contact.Id = new Guid(EmpresaEmitente);
            var filter = Builders<ImportosDTO>.Filter.Eq(s => s.Id, contact.Id);
            this.Collection.DeleteOneAsync(filter);

        }
    }
}