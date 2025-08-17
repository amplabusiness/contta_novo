using Corporate.Contta.Schedule.Domain.Enum;
using Corporate.Contta.Schedule.Infra.Models.ModeloXml.NotaFiscalEletronicaMod55;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Corporate.Contta.Schedule.Api.EmaloteXml
{
    public class GerenciamentoXml
    {
        public async Task<bool> MoverXmlDiretorioCliDef(List<string> diretorioGeral)
        {            
            List<NfeProc> listaNfeMod55 = new List<NfeProc>();
            List<string> listaNfeCanceladas = new List<string>();
            List<string> listaNfeDevolucao = new List<string>();
            List<string> listaNfeServico = new List<string>();
            List<string> listaNfeCte = new List<string>();
            List<string> listaNaoIntentificada = new List<string>();
            List<string> listaNfeConsumidorFinal = new List<string>();
            List<string> listaCartaCorrecao = new List<string>();
            List<string> listaCienciaOperacao = new List<string>();
            List<string> listaEvendoCte = new List<string>();
            List<string> listaConfirmacaoOperaca = new List<string>();
            List<string> listaDesconhecimentoOperacao = new List<string>();

            try
            {
                foreach (var item in diretorioGeral)
                {
                    var file = new FileInfo(item);
                    var restul = VerificarTipoDoXml.RetornarTipoXml(file.FullName);

                    if (restul == ETipoNota.NotaFiscalEletronica)
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(NfeProc));
                        StreamReader stringReader = new StreamReader(file.FullName);
                        var nfes = (NfeProc)serializer.Deserialize(stringReader);                       
                        listaNfeMod55.Add(nfes);
                        
                    }
                    //else if (restul == ETipoNota.Cancelada)
                    //{
                    //    var result = JsonConvert.DeserializeObject<ConttaComsumidor.Models.NotaFiscalCanceladas.ProcEventoNFe>(item);
                    //    listaNfeCanceladas.Add(item);
                    //}
                    //else if (restul == ETipoNota.Devolucao)
                    //{
                    //    var result = JsonConvert.DeserializeObject<ConttaComsumidor.Models.Devolucao.NotaFiscalDevolucaoMod55.NfeProc>(item);
                    //    listaNfeDevolucao.Add(item);
                    //}
                    //else if (restul == ETipoNota.NotaFiscalDeServico)
                    //{
                    //    var result = JsonConvert.DeserializeObject<ConttaComsumidor.Models.NotaFiscalDeServico.GerarNfseResposta>(item);
                    //    listaNfeServico.Add(item);
                    //}
                    //else if (restul == ETipoNota.CTe)
                    //{
                    //    var result = JsonConvert.DeserializeObject<ConttaComsumidor.Models.CTeMod57.CteProc>(item);
                    //    listaNfeCte.Add(item);
                    //}
                    //else if (restul == ETipoNota.NotaFiscalConsumidorFinal)
                    //{
                    //    listaNfeConsumidorFinal.Add(item);
                    //}
                    //else if (restul == ETipoNota.CartaCorrecao)
                    //{
                    //    listaCartaCorrecao.Add(item);
                    //}
                    //else if (restul == ETipoNota.CienciaOperacao)
                    //{
                    //    listaCienciaOperacao.Add(item);
                    //}
                    //else if (restul == ETipoNota.CienciaOperacao)
                    //{
                    //    listaEvendoCte.Add(item);
                    //}
                    //else if (restul == ETipoNota.ConfirmacaoOperaca)
                    //{
                    //    listaConfirmacaoOperaca.Add(item);
                    //}
                    //else if (restul == ETipoNota.DesconhecimentoOperacao)
                    //{
                    //    listaDesconhecimentoOperacao.Add(item);
                    //}
                    //else if (restul == ETipoNota.NaoIdentificada)
                    //{
                    //    listaNaoIntentificada.Add(item);
                    //}

                }
                

                return true;
            }
            catch (Exception ex )
            {

                throw;
            }
            return false;

        }
    }
}
