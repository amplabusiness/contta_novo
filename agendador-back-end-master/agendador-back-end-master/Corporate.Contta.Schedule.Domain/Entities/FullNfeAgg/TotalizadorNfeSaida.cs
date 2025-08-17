using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.Contta.Schedule.Domain.Entities.FullNfeAgg
{
    public class TotalizadorNfeSaida
    {
        public TotalizadorNfeSaida(int? notaInicial, int? notaFinal, int notaCanceladas, int notasFaltante,List<int> listNfeCancel, List<int> listNfeSaida)
        {
            NotaInicial = notaInicial;
            NotaFinal = notaFinal;
            NotaCanceladas = notaCanceladas;
            NotasFaltante = notasFaltante;
            NotasInutilizada = 0;
            ListaNfCancel = listNfeCancel;
            ListaNfSaida = listNfeSaida;
            TotalNotas = ListaNfCancel?.Count + ListaNfSaida?.Count;
        }
        public TotalizadorNfeSaida()
        {

        }
        public int? NotaInicial { get; set; }
        public int? NotaFinal { get; set; }
        public int NotaCanceladas { get; set; }
        public int NotasFaltante { get; set; }
        public int NotasInutilizada { get; set; }
        public int? TotalNotas { get; set; }
        public List<int> ListaNfCancel { get; set; }
        public List<int> ListaNfSaida { get; set; }

    }
}
