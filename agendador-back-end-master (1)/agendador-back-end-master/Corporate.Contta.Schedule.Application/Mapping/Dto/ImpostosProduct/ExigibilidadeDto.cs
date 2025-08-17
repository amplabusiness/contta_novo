using System;

namespace Corporate.Contta.Schedule.Application.Mapping.Dto.ImpostosProduct
{
    public class ExigibilidadeDto
    {
        public Guid? _id { get; set; }
        public string Motivo { get; set; }
        public string NumPocesso { get; set; }
        public string Vara { get; set; }
        public string Uf { get; set; }
        public string Municipio { get; set; }
        public string NomeImposto { get; set; }       
        public string Status { get; set; }
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
    }
}
