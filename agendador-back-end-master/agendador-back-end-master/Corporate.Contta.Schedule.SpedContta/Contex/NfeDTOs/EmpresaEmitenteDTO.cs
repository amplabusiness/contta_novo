using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Corporate.Contta.Schedule.SpedContta.Contex.NfeDTOs
{
    public class EmpresaEmitenteDTO
    {

        public Guid Id { get; set; }
        public string RegTrib { get; set; }

        [BsonElement("Cnpj")]
        public string Cnpj { get; set; }

        [BsonElement("Cpf")]
        public string Cpf { get; set; }

        [BsonElement("RazaoSocial")]
        public string RazaoSocial { get; set; }

        [BsonElement("Fantasia")]
        public string Fantasia { get; set; }

        [BsonElement("IncrEstadual")]
        public string IncrEstadual { get; set; }

        [BsonElement("CodigoMunicipio")]
        public string CodigoMunicipio { get; set; }

        [BsonElement("Cep")]
        public string Cep { get; set; }

        [BsonElement("Endereco")]
        public string Endereco { get; set; }

        [BsonElement("Logradouro")]
        public string Logradouro { get; set; }

        [BsonElement("Numero")]
        public string Numero { get; set; }

        [BsonElement("Complemento")]
        public string Complemento { get; set; }

        [BsonElement("Bairro")]
        public string Bairro { get; set; }

        [BsonElement("Uf")]
        public string Uf { get; set; }

        [BsonElement("Cidade")]
        public string Cidade { get; set; }

        [BsonElement("Ativo")]
        public bool Ativo { get; set; }

        public string Sugrama { get; set; }

        public int CodigoAtividade { get; set; }

        public string InscricaoMunicipal { get; set; }

        public string Fax { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }

    }
}
