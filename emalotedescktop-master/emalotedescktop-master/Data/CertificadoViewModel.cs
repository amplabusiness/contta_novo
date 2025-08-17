using EmaloteContta.Data.Padrao;
using System.Collections.Generic;

namespace EmaloteContta.Data
{
    public class CertificadoViewModel
    {
        public IList<Certificado> ListaCertificados { get; set; }

        private RepositorioCertificado _repositorio;

        private RepositorioCertificado RepositorioCertificado
        {
            get
            {
                if (_repositorio == null)
                {
                    _repositorio = new RepositorioCertificado();
                }

                return _repositorio;
            }
        }

        public CertificadoViewModel()
        {
            ListaCertificados = ConsulteLista();
        }


        public IList<Certificado> ConsulteLista()
        {
            return RepositorioCertificado.ConsulteLista();
        }

    }
}

