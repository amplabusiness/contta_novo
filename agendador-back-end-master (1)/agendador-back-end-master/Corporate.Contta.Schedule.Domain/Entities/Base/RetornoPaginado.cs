using System.Collections.Generic;

namespace Corporate.Contta.Schedule.Domain.Entities.Base
{
    public abstract class RetornoPaginado<T> where T : class
    {
        public long TotalDeItens { get; set; }

        public int TotalDePaginas { get; set; }

        public int PaginaAtual { get; set; }

        public IList<T> Itens { get; set; }
    }
}
