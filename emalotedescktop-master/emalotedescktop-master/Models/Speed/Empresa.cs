using System;
using System.Collections.Generic;
using System.Text;
using EmaloteContta.Models.Speed.Base;

namespace EmaloteContta.Models.Speed
{
    public class Empresa: Entity
    {
        public string Cnpj { get; set; }
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
