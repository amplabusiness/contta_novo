using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Domain.Entities.CompanyInformationAgg
{
    public class CompanyInfo
    {
        //public ObjectId Id { get; set; }

        public string cnpj { get; set; }

        public string identificador_matriz_filial { get; set; }

        public string razao_social { get; set; }

        public string nome_fantasia { get; set; }

        public string situacao_cadastral { get; set; }

        public string data_situacao_cadastral { get; set; }

        public string motivo_situacao_cadastral { get; set; }
        public string descricao_tipo_logradouro { get; set; }
        public string codigo_natureza_juridica { get; set; }
        public string data_inicio_atividade { get; set; }
        public string cnae_fiscal { get; set; }
        public string logradouro { get; set; }
        public string numero { get; set; }
        public string complemento { get; set; }
        public string bairro { get; set; }
        public string cep { get; set; }
        public string uf { get; set; }
        public string codigo_municipio { get; set; }
        public string municipio { get; set; }
        public string ddd_telefone_1 { get; set; }
        public string ddd_fax { get; set; }
        public string qualificacao_do_responsavel { get; set; }
        public string nome_cidade_exterior { get; set; }
        public string ddd_telefone_2 { get; set; }
        public string capital_social { get; set; }
        public string porte { get; set; }
        public string opcao_pelo_simples { get; set; }
        public string data_opcao_pelo_simples { get; set; }
        public string data_exclusao_do_simples { get; set; }
        public string opcao_pelo_mei { get; set; }
        public string situacao_especial { get; set; }
        public string data_situacao_especial { get; set; }
    }
}
