using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using Corporate.Contta.Schedule.SpedContta.Contex.NfeDTOs;
using System;

namespace Corporate.Contta.Schedule.SpedContta.Data.Repository
{
    public class ProdutosCollection
    {
        internal MongoDBRespository _repo = new MongoDBRespository();
        public IMongoCollection<ProdutosDTO> Collection;

        public ProdutosCollection()
        {
            // here we were created new Collection with Products name
            this.Collection = _repo.db.GetCollection<ProdutosDTO>("Produtos");
        }

        public void InsertContact(ProdutosDTO contact)
        {
            this.Collection.InsertOneAsync(contact);
        }

        public List<ProdutosDTO> GetAllProdutos()
        {
            var query = this.Collection
                .Find(new BsonDocument())
                .ToListAsync();
            return query.Result;
        }

        public ProdutosDTO GetAllProdutos(string id)
        {
            var StatusTrayder = this.Collection.Find(
                    new BsonDocument { { "_id", new ObjectId(id) } })
                    .FirstAsync()
                    .Result;
            return StatusTrayder;
        }

        public void DeleteContact(string EmpresaEmitente)
        {
            ProdutosDTO contact = new ProdutosDTO();
            contact.Id = new Guid(EmpresaEmitente);
            var filter = Builders<ProdutosDTO>.Filter.Eq(s => s.Id, contact.Id);
            this.Collection.DeleteOneAsync(filter);

        }
    }
}