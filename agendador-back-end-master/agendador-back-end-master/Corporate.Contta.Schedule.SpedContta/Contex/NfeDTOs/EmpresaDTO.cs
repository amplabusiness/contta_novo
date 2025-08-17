using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.SpedContta.Data.NfeDTOs
{
    public class EmpresaDTO
    {
    
        public string Cnpj { get; set; }
        public string Cpf { get; set; }
        public string RazaoSocial { get; set; }
        public string Fantasia { get; set; }
        public string IncrEstadual { get; set; }
        public string Cep { get; set; }
        public string Endereco { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Uf { get; set; }
        public string Cidade { get; set; }
        public bool Ativo { get; set; }
    }
}
