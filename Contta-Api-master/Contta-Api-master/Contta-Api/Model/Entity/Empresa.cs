using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contta.Inteligencia.Cnpj.Model.Entity
{
    public class Empresa
    {
        public ObjectId Id { get; set; }
        public string cnpj { get; set; }
        public string matriz_filial { get; set; }
        public string razao_social { get; set; }
        public string nome_fantasia { get; set; }
        public string data_situacao { get; set; }
        public string situacao { get; set; }
        public string motivo_situacao { get; set; }
        public string nm_cidade_exterior { get; set; }
        public string cod_pais { get; set; }
        public string nome_pais { get; set; }
        public string cod_nat_juridica { get; set; }
        public string data_inicio_ativ { get; set; }
        public string cnae_fiscal { get; set; }
        public string tipo_logradouro { get; set; }
        public string logradouro { get; set; }
        public string numero { get; set; }
        public string complemento { get; set; }
        public string bairro { get; set; }
        public string cep { get; set; }
        public string uf { get; set; }
        public string cod_municipio { get; set; }
        public string municipio { get; set; }
        public string ddd_1 { get; set; }
        public string telefone_1 { get; set; }
        public string ddd_2 { get; set; }
        public string telefone_2 { get; set; }
        public string ddd_fax { get; set; }
        public string num_fax { get; set; }
        public string email { get; set; }
        public string qualif_resp { get; set; }
        public string capital_social { get; set; }
        public string porte { get; set; }
        public string opc_simples { get; set; }
        public string data_opc_simples { get; set; }
        public string data_exc_simples { get; set; }
        public string opc_mei { get; set; }
        public string sit_especial { get; set; }
        public string data_sit_especial { get; set; }
        public List<Empresa> listFilias { get; set; }
        public List<Socio> listSocio { get; set; }
    }
}
