using MongoDB.Bson;
using Newtonsoft.Json;
using System;

namespace Corporate.Contta.Schedule.Domain.Entities.CompanyInformationAgg
{
    public class Socios<TKey>
        where TKey : struct
    {
        public ObjectId Id { get; set; }
        public TKey? nome_socio1 { get; set; }
        public string nome_socio { get; set; }
        public TKey? cnpj_cpf_socio1 { get; set; }
        public string cnpj_cpf_socio { get; set; }
        public string cnpjMatriz { get; set; }
        public string cnpj { get; set; }
        public string qualificacao_socio { get; set; }
        public string tipo_socio { get; set; }
        public string cod_qualificacao { get; set; }
        public string perc_capital { get; set; }
        public string data_entrada { get; set; }
        public string cod_pais_ext { get; set; }
        public string nome_pais_ext { get; set; }
        public string cpf_repres { get; set; }
        public string nome_repres { get; set; }
        public string cod_qualif_repres { get; set; }
    }
}
