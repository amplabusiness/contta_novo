using EmaloteContta.Data.Padrao;
using System.Collections.Generic;

namespace EmaloteContta.Data
{
    public class PdfClienteViewModel
    {
        private RepositorioPdfCliente _repositorio;

        private RepositorioPdfCliente PdfCliente
        {
            get
            {
                if (_repositorio == null)
                {
                    _repositorio = new RepositorioPdfCliente();
                }

                return _repositorio;
            }
        }

        public IList<PdfCliente> ListaDiretorios { get; set; }

        public PdfClienteViewModel()
        {
            ListaDiretorios = ConsulteLista();
        }

        public IList<PdfCliente> ConsulteLista()
        {
            return PdfCliente.ConsulteLista();
        }

        public void Cadastre(PdfCliente pdfCliente)
        {
            PdfCliente.Cadastre(pdfCliente);
        }
    }
}

