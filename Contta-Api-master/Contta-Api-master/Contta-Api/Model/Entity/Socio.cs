using MongoDB.Bson;
using System;

namespace Contta.Inteligencia.Cnpj.Model.Entity
{
    public class Socio
    {
        public ObjectId Id { get; set; }
        public string cnpj { get; set; }
        public string matriz_filial { get; set; }
        public string nome_fantasia { get; set; }
        public string razao_social { get; set; }
        public int tipo_socio { get; set; }
        public string cargo { get; set; }
        public string tipo { get; set; }
        public string nome_socio { get; set; }
        public string cnpj_cpf_socio { get; set; }
        public int cod_qualificacao { get; set; }
        public double perc_capital { get; set; }
        public string data_entrada { get; set; }
        public string cod_pais_ext { get; set; }
        public string nome_pais_ext { get; set; }
        public string cpf_repres { get; set; }
        public string nome_repres { get; set; }
        public string cod_qualif_repres { get; set; }
        public string situacao { get; set; }
    }
}


//"","tipo_socio","nome_socio","cnpj_cpf_socio","cod_qualificacao","perc_capital","data_entrada","cod_pais_ext","nome_pais_ext","cpf_repres","nome_repres","cod_qualif_repres"