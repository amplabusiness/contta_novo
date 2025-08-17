using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using Corporate.Contta.Schedule.SpedContta.Contex.NfeDTOs;
using System;

namespace Corporate.Contta.Schedule.SpedContta.Data.Repository
{
    public class EmpresaEmitCollection
    {
        internal MongoDBRespository _repo = new MongoDBRespository();
        public IMongoCollection<EmpresaEmitenteDTO> Collection;

        public EmpresaEmitCollection()
        {
            // here we were created new Collection with Products name
            this.Collection = _repo.db.GetCollection<EmpresaEmitenteDTO>("EmpresaEmit");
        }

        public void InsertContact(EmpresaEmitenteDTO contact)
        {
            this.Collection.InsertOneAsync(contact);
        }

        public List<EmpresaEmitenteDTO> GetAllEmpresaEmit()
        {
            var query = this.Collection
                .Find(new BsonDocument())
                .ToListAsync();
            return query.Result;
        }

        public EmpresaEmitenteDTO GetAllEmpresaEmitById(string id)
        {
            var StatusTrayder = this.Collection.Find(
                    new BsonDocument { { "_id", new ObjectId(id) } })
                    .FirstAsync()
                    .Result;
            return StatusTrayder;
        }

        public void DeleteContact(string EmpresaEmitente)
        {
            EmpresaEmitenteDTO contact = new EmpresaEmitenteDTO();
            contact.Id = new Guid(EmpresaEmitente);
            var filter = Builders<EmpresaEmitenteDTO>.Filter.Eq(s => s.Id, contact.Id);
            this.Collection.DeleteOneAsync(filter);

        }
    }
}