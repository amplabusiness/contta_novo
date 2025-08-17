using Corporate.Contta.Schedule.Domain.Entities.Base;
using System;

namespace Corporate.Contta.Schedule.Domain.Entities.Criticas
{
    public class CriticasAntigas:  Entity
    {
        public int St { get; set; }
        public int Ncm { get; set; }
        public int Cfop { get; set; }
        public int Cnae { get; set; }
        public int Estoque { get; set; }
        public DateTime DataCadastro { get; set; }
        public Guid CompanyInformation { get; set; }
    }
}
