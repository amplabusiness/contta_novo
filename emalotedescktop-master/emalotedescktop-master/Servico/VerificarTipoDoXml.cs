using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using EmaloteContta.Adapter;
using EmaloteContta.Models;

namespace EmaloteContta.Servico
{
    public static class VerificarTipoDoXml
    {
        public static ETipoNota RetornarTipoXml(string file)
        {
            return ValidarXml(file);
        }

        private static ETipoNota ValidarXml(string file)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(file);

                XmlNodeList tipoNfe = doc.GetElementsByTagName("natOp");

                XmlNodeList dev = doc.GetElementsByTagName("natOp");
                if (dev.Count > 0)
                {
                    if (dev[0].InnerText == "DEVOLUÇAO DE MERCADORIA")
                        return GetTipoNota(dev[0].InnerText);
                }

                XmlNodeList servico = doc.GetElementsByTagName("ValorServicos");
                if (servico.Count > 0)
                    return GetTipoNota("Servico");

                XmlNodeList canc = doc.GetElementsByTagName("descEvento");
                XmlNodeList cancCte = doc.GetElementsByTagName("eventoCTe");
                if (canc.Count > 0 && cancCte.Count == 0)
                    return GetTipoNota(canc[0].InnerText);
                if(canc.Count == 0 && cancCte.Count > 0)
                    return GetTipoNota("CancelamentoCte");               

                XmlNodeList mod = doc.GetElementsByTagName("mod");
                if (mod.Count > 0)
                    return GetTipoNota(mod[0].InnerText);
                else
                {
                    XmlNodeList nfes = doc.GetElementsByTagName("Nfse");
                    if (nfes.Count > 0)
                        return ETipoNota.NotaFiscalDeServico;
                }
                return ETipoNota.NaoIdentificada;
            }
            catch (Exception ex)
            {
                //EmaloteContta.Adapter.Erros.Add($"Erro: {ex.Message}, arquivo: {file}");
                return ETipoNota.NaoIdentificada;
            }

        }

        private static ETipoNota GetTipoNota(string innerText)//NotaFiscalDeServico
        {
            switch (innerText)
            {
                case "55":
                    return ETipoNota.NotaFiscalEletronica;
                case "57":
                    return ETipoNota.CTe;
                case "65":
                    return ETipoNota.NotaFiscalConsumidorFinal;
                case "Cancelamento":
                    return ETipoNota.Cancelada;
                case "DEVOLUÇAO DE MERCADORIA":
                    return ETipoNota.Devolucao;
                case "Servico":
                    return ETipoNota.NotaFiscalDeServico;
                case "CancelamentoCte":
                    return ETipoNota.CanceladaCte;
                default:
                    return ETipoNota.NaoIdentificada;
            }
        }
    }
}
