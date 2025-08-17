using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Application.Mapping.Dto.ImpostosProduct
{
   public class ImpostoInsentoDto
    {
        public Guid? _id { get; set; }
        public string NCM { get; set; }
        public string Descricao { get; set; }

        public string Status { get; set; }

        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
       
    }
}
