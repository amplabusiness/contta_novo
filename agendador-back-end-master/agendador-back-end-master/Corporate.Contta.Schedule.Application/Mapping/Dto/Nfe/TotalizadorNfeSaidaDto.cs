using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Application.Mapping.Dto.Nfe
{
    public class TotalizadorNfeSaidaDto
    {
        public int? NotaInicial { get; set; }
        public int? NotaFinal { get; set; }
        public int NotaCanceladas { get; set; }
        public int NotasInutilizada { get; set; }
        public int NotasFaltante { get; set; }
        public int? TotalNotas { get; set; }
        public List<int> ListaNfCancel { get; set; }
        public List<int> ListaNfSaida { get; set; }
    }
}
