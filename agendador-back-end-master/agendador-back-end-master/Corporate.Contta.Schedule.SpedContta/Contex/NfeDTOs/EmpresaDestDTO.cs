using System;
using System.Collections.Generic;

namespace Corporate.Contta.Schedule.SpedContta.Contex.NfeDTOs
{
    public class EmpresaDestDTO
    {
        public List<ProdutosDTO> Produtos { get; set; }
        public List<ImportosDTO> Impostos { get; set; }

        public Guid Id { get; set; }
        public string InscEstSubTribu { get; set; }
        public bool? Estrangeiro { get; set; }
        public string CPF { get; set; }
        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public string Fantasia { get; set; }
        public string IncrEstadual { get; set; }
        public string InscricaoMunicipal { get; set; }
        public string CodigoMunicipio { get; set; }
        public string Cep { get; set; }
        public string Endereco { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Uf { get; set; }
        public string Cidade { get; set; }
        public bool Ativo { get; set; }
        public string CodPais { get; set; }
        public string Sugrama { get; set; }

    }
}
