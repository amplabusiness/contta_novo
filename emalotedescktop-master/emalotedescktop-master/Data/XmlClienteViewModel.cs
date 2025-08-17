using eMalote.Models;

using EmaloteContta.Data.Padrao;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmaloteContta.Data
{
    public class XmlClienteViewModel
    {
        private RepositorioXmlCliente _repositorio;

        private RepositorioXmlCliente _xmlCliente
        {
            get
            {
                if (_repositorio == null)
                {
                    _repositorio = new RepositorioXmlCliente();
                }

                return _repositorio;
            }
        }

        public IList<XmlCliente> ListaDiretorios { get; set; }

        public XmlClienteViewModel()
        {
            ListaDiretorios = ConsulteLista();
        }

        public IList<XmlCliente> ConsulteLista()
        {
            return _xmlCliente.ConsulteLista();
        }

        public void Cadastre(XmlCliente xmlCliente)
        {
            _xmlCliente.Cadastre(xmlCliente);
        }

    }
}

