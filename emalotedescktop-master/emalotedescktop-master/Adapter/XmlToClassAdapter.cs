using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using EmaloteContta.Infra.Repositorios;
using EmaloteContta.ListCfop;
using EmaloteContta.Models.CTeMod57;
using EmaloteContta.Models.NotaFiscalEletronicaMod55;
using EmaloteContta.Models.Respositories;
using EmaloteContta.Servico;

namespace EmaloteContta.Adapter
{
    public class XmlToClassAdapter
    {
        public static List<string> Erros = new List<string>();
     
        public static Models.NotaFiscalDeServico.GerarNfseResposta GetNotaFiscalDeServico(string file)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Models.NotaFiscalDeServico.GerarNfseResposta));
                StreamReader stringReader = new StreamReader(file);
                var nfs = (Models.NotaFiscalDeServico.GerarNfseResposta)serializer.Deserialize(stringReader);
                stringReader.Close();
                return nfs;
            }
            catch (Exception ex)
            {
                Erros.Add($"Erro: {ex.Message}, arquivo: {file}");
                return null;
            }
        }

        public static EmaloteContta.Models.NotaFiscalConsumidorFinalMod65.NfeProc GetNotaFiscalMod65(string file)
        {
            try
            {
                EmpresaEmitenteRepositorio empresaEmitenteRepositorio = new EmpresaEmitenteRepositorio();
                XmlSerializer serializer = new XmlSerializer(typeof(EmaloteContta.Models.NotaFiscalConsumidorFinalMod65.NfeProc));
                StreamReader stringReader = new StreamReader(file);
                var nfeCF = (Models.NotaFiscalConsumidorFinalMod65.NfeProc)serializer.Deserialize(stringReader);

                var empresa = empresaEmitenteRepositorio.ObterPorCnpj(nfeCF.NFe.InfNFe.Emit.CNPJ).Result;

                if (empresa == null)
                {
                    nfeCF.ModeloNota = "Entrada";
                    nfeCF.CnpjEmitente = nfeCF.NFe.InfNFe.Dest.CNPJ;
                }
                else
                {
                    nfeCF.ModeloNota = "Saida";
                    nfeCF.CnpjEmitente = empresa.Cnpj;
                }

                stringReader.Close();
                return nfeCF;
            }
            catch (Exception ex)
            {
                Erros.Add($"Erro: {ex.Message}, arquivo: {file}");
                return null;
            }
        }

        public static CteProc GetNotaFiscalCteMod57(string file)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(CteProc));
                StreamReader stringReader = new StreamReader(file);
                var cte = (CteProc)serializer.Deserialize(stringReader);

                stringReader.Close();
                return cte;
            }
            catch (Exception ex)
            {
                Erros.Add($"Erro: {ex.Message}, arquivo: {file}");
                return null;
            }
        }

        public static EmaloteContta.Models.NotaFiscalEletronicaMod55.NfeProc GetNotaFiscalMod55(string file)
        {
            try
            {
                EmpresaEmitenteRepositorio empresaEmitenteRepositorio = new EmpresaEmitenteRepositorio();
                XmlSerializer serializer = new XmlSerializer(typeof(EmaloteContta.Models.NotaFiscalEletronicaMod55.NfeProc));
                StreamReader stringReader = new StreamReader(file);
                var nfes = (EmaloteContta.Models.NotaFiscalEletronicaMod55.NfeProc)serializer.Deserialize(stringReader);

                var empresa = empresaEmitenteRepositorio.ObterPorCnpj(nfes.NFe.InfNFe.Emit.CNPJ).Result;

                if(empresa == null)
                {
                    nfes.ModeloNota = "Entrada";
                    nfes.CnpjEmitente = nfes.NFe.InfNFe.Dest.CNPJ;
                }
                else
                {
                    nfes.ModeloNota = "Saida";
                    nfes.CnpjEmitente = empresa.Cnpj;
                }

                stringReader.Close();
                return nfes;
            }
            catch (Exception ex)
            {
                Erros.Add($"Erro: {ex.Message}, arquivo: {file}");
                return null;
            }
        }

        public static List<NfeProc> GetNotaFiscalMod55Geral(string[] listaxml)
        {
            EmpresaEmitenteRepositorio empresaEmitenteRepositorio = new EmpresaEmitenteRepositorio();
            var listaNfe = new List<NfeProc>();
             var empresa = empresaEmitenteRepositorio.ObterTodasEmpresa().Result;
            var qtd = 0;

            try
            {
                foreach (var item in listaxml)
                {
                    var file = new FileInfo(item);
                    var restul = VerificarTipoDoXml.RetornarTipoXml(file.FullName);
                    if(restul == Models.ETipoNota.NotaFiscalEletronica)
                    {
                        
                        XmlSerializer serializer = new XmlSerializer(typeof(NfeProc));
                        StreamReader stringReader = new StreamReader(file.FullName);
                        var nfes = (NfeProc)serializer.Deserialize(stringReader);
                        nfes.FileInfo = file.FullName;
                        nfes.FileName = file.Name;
                        nfes.ETipoNota = Models.ETipoNota.NotaFiscalEletronica;                      

                        var empresaDto = empresa.FirstOrDefault(c => c.Cnpj.Equals(nfes.NFe.InfNFe.Emit.CNPJ));

                        if (empresaDto == null)
                        {
                            nfes.ModeloNota = "Entrada";
                            nfes.CnpjEmitente = nfes.NFe.InfNFe.Dest.CNPJ;
                        }
                        else
                        {
                            nfes.ModeloNota = "Saida";
                            nfes.CnpjEmitente = empresaDto.Cnpj;
                        }
                       
                        listaNfe.Add(nfes);
                        Console.WriteLine($"Nfe Add { qtd = qtd + 1}");
                    }  
                }

                return listaNfe;
            }
            catch (Exception ex)
            {
                Erros.Add($"Erro: {ex.Message}, arquivo:");
                return null;
            }
        }

        public static Models.Devolucao.NotaFiscalDevolucaoMod55.NfeProc GetNotaFiscalModDev(string file)
        {
            try
            {
                EmpresaEmitenteRepositorio empresaEmitenteRepositorio = new EmpresaEmitenteRepositorio();
                XmlSerializer serializer = new XmlSerializer(typeof(Models.Devolucao.NotaFiscalDevolucaoMod55.NfeProc));
                StreamReader stringReader = new StreamReader(file);
                var nfes = (Models.Devolucao.NotaFiscalDevolucaoMod55.NfeProc)serializer.Deserialize(stringReader);

                var empresa = empresaEmitenteRepositorio.ObterPorCnpj(nfes.NFe.InfNFe.Emit.CNPJ).Result;

                if (empresa == null)
                {
                    nfes.ModeloNota = "Entrada";
                    nfes.CnpjEmitente = nfes.NFe.InfNFe.Dest.CNPJ;
                }
                else
                {
                    nfes.ModeloNota = "Saida";
                    nfes.CnpjEmitente = empresa.Cnpj;
                }

                stringReader.Close();
                return nfes;
            }
            catch (Exception ex)
            {
                Erros.Add($"Erro: {ex.Message}, arquivo: {file}");
                return null;
            }
        }

        public static EmaloteContta.Models.ProcEventoNFe GetNotaFiscalModCanc(string file)
        {
            try
            {
                EmpresaEmitenteRepositorio empresaEmitenteRepositorio = new EmpresaEmitenteRepositorio();
                XmlSerializer serializer = new XmlSerializer(typeof(EmaloteContta.Models.ProcEventoNFe));
                StreamReader stringReader = new StreamReader(file);
                var nfes = (EmaloteContta.Models.ProcEventoNFe)serializer.Deserialize(stringReader);

          
                stringReader.Close();
                return nfes;
            }
            catch (Exception ex)
            {
                Erros.Add($"Erro: {ex.Message}, arquivo: {file}");
                return null;
            }
        }

    }
}
