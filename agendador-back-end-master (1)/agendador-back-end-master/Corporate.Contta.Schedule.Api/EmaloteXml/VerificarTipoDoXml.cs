using Corporate.Contta.Schedule.Domain.Enum;
using System;
using System.Xml;

namespace Corporate.Contta.Schedule.Api.EmaloteXml
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

                XmlNodeList versao = doc.GetElementsByTagName("nfeProc");
                if (versao.Count > 0)
                {
                    var teste = versao[0].InnerXml.Split("<");
                    var teste1 = teste[2].Split("<");
                    var teste2 = teste1[0].Split($"\"");
                    var teste3 = teste2[1];

                    Console.WriteLine(teste3);
                }

                XmlNodeList dev = doc.GetElementsByTagName("natOp");
                if (dev.Count > 0)
                {
                    if (dev[0].InnerText == "DEVOLUÇAO DE MERCADORIA")
                        return GetTipoNota(dev[0].InnerText);
                }

                XmlNodeList carta = doc.GetElementsByTagName("detEvento");
                if (carta.Count > 0)
                {
                    if (carta[0].InnerText.Contains("Carta"))
                        return GetTipoNota("Carta de Correcao");
                    if (carta[0].InnerText.Contains("Confirmacao da Operacao"))
                        return GetTipoNota("ConfirmacaoOperaca");
                    if (carta[0].InnerText.Contains("Desconhecimento da Operacao"))
                        return GetTipoNota("DesconhecimentoOperacao");
                }

                XmlNodeList eventoCTe = doc.GetElementsByTagName("eventoCTe");
                if (eventoCTe.Count > 0)
                {
                    return GetTipoNota("eventoCTe");
                }

                XmlNodeList ciencia = doc.GetElementsByTagName("detEvento");
                if (ciencia.Count > 0)
                {
                    if (ciencia[0].InnerText.Contains("Ciencia"))
                        return GetTipoNota("Ciencia da Operacao");
                }

                XmlNodeList servico = doc.GetElementsByTagName("ValorServicos");
                if (servico.Count > 0)
                    return GetTipoNota("Servico");

                XmlNodeList canc = doc.GetElementsByTagName("descEvento");
                XmlNodeList cancCte = doc.GetElementsByTagName("eventoCTe");
                if (canc.Count > 0 && cancCte.Count == 0)
                    return GetTipoNota(canc[0].InnerText);
                if (canc.Count == 0 && cancCte.Count > 0)
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
                case "Carta de Correcao":
                    return ETipoNota.CartaCorrecao;
                case "Ciencia da Operacao":
                    return ETipoNota.CienciaOperacao;
                case "ConfirmacaoOperaca":
                    return ETipoNota.ConfirmacaoOperaca;
                case "DesconhecimentoOperacao":
                    return ETipoNota.ConfirmacaoOperaca;
                case "eventoCTe":
                    return ETipoNota.eventoCTe;
                default:
                    return ETipoNota.NaoIdentificada;

            }
        }
    }
}
