using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using Corporate.Contta.Schedule.SpedContta.Contex.NfeDTOs;
using System;

namespace Corporate.Contta.Schedule.SpedContta.Data.Repository
{
    public class EmpresaDestCollection
    {
        internal MongoDBRespository _repo = new MongoDBRespository();
        public IMongoCollection<EmpresaDestDTO> Collection;

        public EmpresaDestCollection()
        {
            // here we were created new Collection with Products name
            this.Collection = _repo.db.GetCollection<EmpresaDestDTO>("EmpresaDest");
        }

        public void InsertContact(EmpresaDestDTO contact)
        {
            this.Collection.InsertOneAsync(contact);
        }

        public List<EmpresaDestDTO> GetAllEmpresaDest()
        {
            var query = this.Collection
                .Find(new BsonDocument())
                .ToListAsync();
            return query.Result;
        }

        public EmpresaDestDTO GetAllEmpresaEmitById(string id)
        {
            var StatusTrayder = this.Collection.Find(
                    new BsonDocument { { "_id", new ObjectId(id) } })
                    .FirstAsync()
                    .Result;
            return StatusTrayder;
        }

        public void DeleteContact(string EmpresaEmitente)
        {
            EmpresaDestDTO contact = new EmpresaDestDTO();
            contact.Id = new Guid(EmpresaEmitente);
            var filter = Builders<EmpresaDestDTO>.Filter.Eq(s => s.Id, contact.Id);
            this.Collection.DeleteOneAsync(filter);

        }
    }
}