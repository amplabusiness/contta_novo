using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using Corporate.Contta.Schedule.SpedContta.Contex.NfeDTOs;
using System;

namespace Corporate.Contta.Schedule.SpedContta.Data.Repository
{
    public class NfeCollection
    {
        internal MongoDBRespository _repo = new MongoDBRespository();
        public IMongoCollection<NfeDTO> Collection;

        public NfeCollection()
        {
            // here we were created new Collection with Products name
            this.Collection = _repo.db.GetCollection<NfeDTO>("NFE");
        }

        public void InsertContact(NfeDTO contact)
        {
            this.Collection.InsertOneAsync(contact);
        }

        public List<NfeDTO> GetAllNfe()
        {
            var query = this.Collection
                .Find(new BsonDocument())
                .ToListAsync();
            return query.Result;
        }

        public NfeDTO GetAllEmpresaEmitById(string id)
        {
            var StatusTrayder = this.Collection.Find(
                    new BsonDocument { { "_id", new ObjectId(id) } })
                    .FirstAsync()
                    .Result;
            return StatusTrayder;
        }

        public void DeleteContact(string EmpresaEmitente)
        {
            NfeDTO contact = new NfeDTO();
            contact.Id = new Guid(EmpresaEmitente);
            var filter = Builders<NfeDTO>.Filter.Eq(s => s.Id, contact.Id);
            this.Collection.DeleteOneAsync(filter);

        }
    }
}