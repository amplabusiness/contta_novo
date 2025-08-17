using eMalote.Models;
using EmaloteContta.Data.Padrao;
using System.Collections.Generic;

namespace EmaloteContta.Data
{
    public class DiretorioViewModel
    {
        private RepositorioDiretorio _repositorio;

        private RepositorioDiretorio RepositorioDiretorio
        {
            get
            {
                if (_repositorio == null)
                {
                    _repositorio = new RepositorioDiretorio();
                }

                return _repositorio;
            }
        }

        public IList<Diretorio> ListaDiretorios { get; set; }

        public DiretorioViewModel()
        {
            ListaDiretorios = ConsulteLista();
        }

        public IList<Diretorio> ConsulteLista()
        {
            return RepositorioDiretorio.ConsulteLista();
        }
     
    }
}

