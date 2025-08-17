using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Threading;
using EmaloteContta.Servico;
using EmaloteContta.Models.CTeMod57;
using EmaloteContta.Storage;
using System.Threading.Tasks;
using DanfeSharp.Modelo;
using DanfeSharp;
using EmaloteContta.ListCfop;
using System.Security.Cryptography;
using EmaloteContta.Tool;
using Newtonsoft.Json;
using EmaloteContta.Models.NotaFiscalEletronicaMod55;

namespace EmaloteContta.Main
{
    public class XmlToClass
    {
        private string _path;
        private List<EmaloteContta.Models.CTeMod57.CteProc> NotaFiscalMod57Ctes;
        private List<EmaloteContta.Models.NotaFiscalConsumidorFinalMod65.NfeProc> NotasFiscaisConsumidorFinal65;
        private List<EmaloteContta.Models.NotaFiscalDeServico.GerarNfseResposta> NotasFicaisDeServico;
        private List<EmaloteContta.Models.NotaFiscalEletronicaMod55.NfeProc> NotasFicaisEletronicasMod55;
        private List<EmaloteContta.Models.Devolucao.NotaFiscalDevolucaoMod55.NfeProc> NotasFicaisEletronicasMod55Dev;
        private List<EmaloteContta.Models.ProcEventoNFe> NotasFiscalEletronicasCanc;

        private List<string> Erros;
        public string OutputFolder;
        private string msg = "Arquivo não Identificado";
        public XmlToClass(string input)
        {
            _path = input;
            NotaFiscalMod57Ctes = new List<EmaloteContta.Models.CTeMod57.CteProc>();
            NotasFiscaisConsumidorFinal65 = new List<EmaloteContta.Models.NotaFiscalConsumidorFinalMod65.NfeProc>();
            NotasFicaisDeServico = new List<Models.NotaFiscalDeServico.GerarNfseResposta>();
            NotasFicaisEletronicasMod55 = new List<Models.NotaFiscalEletronicaMod55.NfeProc>();
            NotasFicaisEletronicasMod55Dev = new List<EmaloteContta.Models.Devolucao.NotaFiscalDevolucaoMod55.NfeProc>();
            NotasFiscalEletronicasCanc = new List<EmaloteContta.Models.ProcEventoNFe>();
            Erros = new List<string>();
            ClearErros();
        }
        public void Start()
        {
            Config();
            //MoverXmlDiretorioCli();
            MoverXmlDiretorioCliDef();
            GravarNotaNaFilaRabbitmq();
        }

        private void GravarNotaNaFilaRabbitmq()
        {
            if (NotasFicaisEletronicasMod55.Count > 0)
            {
                try
                {
                    Thread t = new Thread(moverDiretorio);
                    t.Start();

                    XmlDocument doc = new XmlDocument();
                    RabbitmqXml rabbitmqXml = new RabbitmqXml();

                    rabbitmqXml.RabbitmqFiasMod55(NotasFicaisEletronicasMod55);

                }
                catch (Exception ex)
                {
                    Erros.Add(ex.Message);
                }

                void moverDiretorio()
                {
                    foreach (var item in NotasFicaisEletronicasMod55)
                    {
                        var dataEmit = "";
                        if (item.NFe.InfNFe.Ide.DhEmi != null)
                            dataEmit = item.NFe.InfNFe.Ide.DhEmi;
                        else
                        {
                            dataEmit = item.NFe.InfNFe.Ide.dEmi;
                        }

                        var t = Task.Run(() => AmazonStorage.UploadFileAsync(item.FileName, item.CnpjEmitente, "application/xml"));
                        t.Wait();
                        Thread.Sleep(200);
                        MoveFile(item.FileInfo, item.FileName, item.CnpjEmitente, DateTime.Parse(dataEmit), Models.ETipoNota.NotaFiscalEletronica, false);
                    }
                }

            }
            if (NotasFicaisEletronicasMod55Dev.Count > 0)
            {
                try
                {
                    Thread t = new Thread(moverDiretorioDev);
                    t.Start();

                    XmlDocument doc = new XmlDocument();
                    RabbitmqXml rabbitmqXml = new RabbitmqXml();

                    rabbitmqXml.RabbitmqFiasMod55Devolucao(NotasFicaisEletronicasMod55Dev);

                }
                catch (Exception ex)
                {
                    Erros.Add(ex.Message);
                }

                void moverDiretorioDev()
                {
                    foreach (var item in NotasFicaisEletronicasMod55Dev)
                    {
                        MoveFile(item.FileInfo, item.FileName, item.NFe.InfNFe.Emit.CNPJ, DateTime.Parse(item.NFe.InfNFe.Ide.DhEmi), Models.ETipoNota.Devolucao, false);
                    }
                }
            }
            if (NotaFiscalMod57Ctes.Count > 0)
            {
                try
                {
                    Thread t = new Thread(moverDiretorioMod57);
                    t.Start();

                    XmlDocument doc = new XmlDocument();
                    RabbitmqXml rabbitmqXml = new RabbitmqXml();

                    rabbitmqXml.RabbitmqFiasMod57(NotaFiscalMod57Ctes);

                }
                catch (Exception ex)
                {
                    Erros.Add(ex.Message);
                }

                void moverDiretorioMod57()
                {
                    foreach (var item in NotaFiscalMod57Ctes)
                    {
                        MoveFile(item.FileInfo, item.FileName, item.CTe.InfCte.Emit.CNPJ, DateTime.Parse(item.CTe.InfCte.Ide.DhEmi), Models.ETipoNota.CTe, false);
                    }
                }
            }
            if (NotasFiscalEletronicasCanc.Count > 0)
            {
                try
                {
                    Thread t = new Thread(moverDiretorioMod55Canceladas);
                    t.Start();

                    XmlDocument doc = new XmlDocument();
                    RabbitmqXml rabbitmqXml = new RabbitmqXml();

                    rabbitmqXml.RabbitmqFiasMod55Canceladas(NotasFiscalEletronicasCanc);

                }
                catch (Exception ex)
                {
                    Erros.Add(ex.Message);
                }

                void moverDiretorioMod55Canceladas()
                {
                    foreach (var item in NotasFiscalEletronicasCanc)
                    {
                        MoveFile(item.FileInfo, item.FileName, item.Evento.InfEvento.CNPJ, DateTime.Parse(item.Evento.InfEvento.DhEvento), Models.ETipoNota.Cancelada, false);
                    }
                }
            }
            if (NotasFiscaisConsumidorFinal65.Count > 0)
            {
                try
                {
                    Thread t = new Thread(moverDiretorioMod65);
                    t.Start();

                    XmlDocument doc = new XmlDocument();
                    RabbitmqXml rabbitmqXml = new RabbitmqXml();

                    rabbitmqXml.RabbitmqFiasMod65(NotasFiscaisConsumidorFinal65);

                }
                catch (Exception ex)
                {
                    Erros.Add(ex.Message);
                }

                void moverDiretorioMod65()
                {
                    foreach (var item in NotasFiscaisConsumidorFinal65)
                    {
                        MoveFile(item.FileInfo, item.FileName, item.NFe.InfNFe.Emit.CNPJ, DateTime.Parse(item.NFe.InfNFe.Ide.DhEmi), Models.ETipoNota.NotaFiscalConsumidorFinal, false);
                    }
                }
            }
            if (NotasFicaisDeServico.Count > 0)
            {
                try
                {
                    Thread t = new Thread(moverDiretorio);
                    t.Start();

                    XmlDocument doc = new XmlDocument();
                    RabbitmqXml rabbitmqXml = new RabbitmqXml();

                    rabbitmqXml.RabbitmqFiasNodServico(NotasFicaisDeServico);

                }
                catch (Exception ex)
                {
                    Erros.Add(ex.Message);
                }

                void moverDiretorio()
                {
                    foreach (var item in NotasFicaisDeServico)
                    {
                        MoveFile(item.FileInfo, item.FileName, item.ListaNfse.CompNfse.Nfse.InfNfse.DeclaracaoPrestacaoServico.InfDeclaracaoPrestacaoServico.Prestador.CpfCnpj.Cnpj, DateTime.Parse(item.ListaNfse.CompNfse.Nfse.InfNfse.DataEmissao), EmaloteContta.Models.ETipoNota.NotaFiscalDeServico, false);
                    }
                }
            }
        }

        private void MoverXmlDiretorioCli()
        {
            List<string> vs = new List<string>();
            RabbitmqXml rabbitmqXml = new RabbitmqXml();
            string cnpj = string.Empty;
            var diretorioGeral = Directory.GetFiles(_path, "*.xml", SearchOption.AllDirectories);
            foreach (var item in diretorioGeral)
            {
                var file = new FileInfo(item);

                if (!file.Name.Contains("CARTA"))
                {
                    var restul = VerificarTipoDoXml.RetornarTipoXml(file.FullName);

                    if (restul == Models.ETipoNota.CTe)
                    {
                        try
                        {
                            //Gerar fila de Cte
                            var cte = EmaloteContta.Adapter.XmlToClassAdapter.GetNotaFiscalCteMod57(file.FullName);

                            cte.FileInfo = file.FullName;
                            cte.FileName = file.Name;
                            cte.ETipoNota = Models.ETipoNota.CTe;

                            if (cte != null)
                                if (!NotaFiscalMod57Ctes.Any(c => c.ProtCTe.InfProt.ChCTe == cte.ProtCTe.InfProt.ChCTe))
                                {
                                    NotaFiscalMod57Ctes.Add(cte);
                                    cnpj = cte.CTe.InfCte.Emit.CNPJ;
                                }

                        }
                        catch (Exception ex)
                        {
                            Erros.Add($"Erro: Arquivo já adicionado a lista, (Dupliciade de CTE), arquivo {file.Name}");
                            MoveFile(file.FullName, file.Name, null, null, EmaloteContta.Models.ETipoNota.CTe, true);
                        }

                    }
                    else if (restul == Models.ETipoNota.NotaFiscalConsumidorFinal)
                    {
                        try
                        {
                            var nfc = EmaloteContta.Adapter.XmlToClassAdapter.GetNotaFiscalMod65(file.FullName);
                            nfc.FileInfo = file.FullName;
                            nfc.FileName = file.Name;
                            nfc.ETipoNota = Models.ETipoNota.NotaFiscalConsumidorFinal;
                            if (!NotasFiscaisConsumidorFinal65.Any(c => c.ProtNFe.InfProt.ChNFe == nfc.ProtNFe.InfProt.ChNFe))
                            {
                                NotasFiscaisConsumidorFinal65.Add(nfc);
                                //Gerar aki a thread para gerar a gravação do xml na amazon.
                                var t = Task.Run(() => AmazonStorage.UploadFileAsync(nfc.FileName, nfc.CnpjEmitente, "application/xml"));
                                t.Wait();
                            }

                        }
                        catch (Exception ex)
                        {
                            Erros.Add($"Erro: Arquivo já adicionado a lista, (Dupliciade de nota consumidor final), arquivo {file.Name}");
                            MoveFile(file.FullName, file.Name, null, null, Models.ETipoNota.NotaFiscalConsumidorFinal, true);
                        }

                    }
                    else if (restul == Models.ETipoNota.NotaFiscalDeServico)
                    {
                        try
                        {
                            var nfs = EmaloteContta.Adapter.XmlToClassAdapter.GetNotaFiscalDeServico(file.FullName);
                            nfs.FileInfo = file.FullName;
                            nfs.FileName = file.Name;
                            nfs.ETipoNota = Models.ETipoNota.NotaFiscalDeServico;

                            if (nfs != null)
                                if (!NotasFicaisDeServico.Any(c => c.ListaNfse.CompNfse.Nfse.InfNfse.Numero ==
                                                                   nfs.ListaNfse.CompNfse.Nfse.InfNfse.Numero &&
                                                                   c.ListaNfse.CompNfse.Nfse.InfNfse.CodigoVerificacao ==
                                                                   nfs.ListaNfse.CompNfse.Nfse.InfNfse.CodigoVerificacao &&
                                                                   c.ListaNfse.CompNfse.Nfse.InfNfse.DeclaracaoPrestacaoServico.InfDeclaracaoPrestacaoServico.Prestador.InscricaoMunicipal ==
                                                                   nfs.ListaNfse.CompNfse.Nfse.InfNfse.DeclaracaoPrestacaoServico.InfDeclaracaoPrestacaoServico.Prestador.InscricaoMunicipal))
                                {
                                    NotasFicaisDeServico.Add(nfs);
                                    cnpj = nfs.ListaNfse.CompNfse.Nfse.InfNfse.Numero;
                                }


                        }
                        catch (Exception ex)
                        {
                            Erros.Add($"Erro: Arquivo já adicionado a lista, (Dupliciade de nota de serviço), arquivo {file.Name}");
                            MoveFile(file.FullName, file.Name, null, null, Models.ETipoNota.NotaFiscalDeServico, true);
                        }

                    }
                    else if (restul == Models.ETipoNota.NotaFiscalEletronica)
                    {
                        GetCfop getCfop = new GetCfop();
                        List<double> listaCfop = new List<double>();
                        var nfe = EmaloteContta.Adapter.XmlToClassAdapter.GetNotaFiscalMod55(file.FullName);
                        nfe.FileInfo = file.FullName;
                        nfe.FileName = file.Name;
                        nfe.ETipoNota = Models.ETipoNota.NotaFiscalEletronica;


                        if (nfe != null)
                            if (!NotasFicaisEletronicasMod55.Any(c => c.ProtNFe.InfProt.ChNFe == nfe.ProtNFe.InfProt.ChNFe))
                            {
                                try
                                {
                                    NotasFicaisEletronicasMod55.Add(nfe);
                                    Console.WriteLine($"NotaAdicionada={nfe.CnpjEmitente}");

                                }
                                catch (Exception ex)
                                {
                                    Erros.Add($"Erro: {ex.Message}, arquivo: {file}");
                                }
                            }
                            else
                            {
                                Erros.Add($"Erro: Arquivo já adicionado a lista, (Dupliciade de nota), arquivo {file.Name}");
                                MoveFile(file.FullName, file.Name, null, null, Models.ETipoNota.NotaFiscalEletronica, true);
                            }

                    }
                    else if (restul == Models.ETipoNota.Devolucao)
                    {
                        var nfe = EmaloteContta.Adapter.XmlToClassAdapter.GetNotaFiscalModDev(file.FullName);
                        nfe.FileInfo = file.FullName;
                        nfe.FileName = file.Name;
                        nfe.ETipoNota = Models.ETipoNota.Devolucao;

                        if (nfe != null)
                            if (!NotasFicaisEletronicasMod55Dev.Any(c => c.ProtNFe.InfProt.ChNFe == nfe.ProtNFe.InfProt.ChNFe))
                            {
                                try
                                {
                                    NotasFicaisEletronicasMod55Dev.Add(nfe);
                                    cnpj = nfe.NFe.InfNFe.Emit.CNPJ;

                                }
                                catch (Exception ex)
                                {
                                    Erros.Add($"Erro: {ex.Message}, arquivo: {file}");
                                }
                            }
                            else
                            {
                                Erros.Add($"Erro: Arquivo já adicionado a lista, (Dupliciade de nota), arquivo {file.Name}");
                                MoveFile(file.FullName, file.Name, null, null, Models.ETipoNota.NotaFiscalEletronica, true);
                            }

                    }
                    else if (restul == EmaloteContta.Models.ETipoNota.Cancelada)
                    {
                        var nfe = EmaloteContta.Adapter.XmlToClassAdapter.GetNotaFiscalModCanc(file.FullName);
                        nfe.FileInfo = file.FullName;
                        nfe.FileName = file.Name;
                        nfe.ETipoNota = Models.ETipoNota.Cancelada;
                        if (!NotasFicaisEletronicasMod55.Any(c => c.ProtNFe.InfProt.ChNFe == nfe.Evento.InfEvento.ChNFe))
                        {
                            try
                            {
                                NotasFiscalEletronicasCanc.Add(nfe);
                                cnpj = nfe.Evento.InfEvento.CNPJ;
                            }
                            catch (Exception ex)
                            {
                                Erros.Add($"Erro: {ex.Message}, arquivo: {file}");
                            }
                        }
                        else
                        {
                            Erros.Add($"Erro: Arquivo já adicionado a lista, (Dupliciade de nota), arquivo {file.Name}");
                            MoveFile(file.FullName, file.Name, null, null, Models.ETipoNota.NotaFiscalEletronica, true);
                        }

                    }
                    else if (restul == EmaloteContta.Models.ETipoNota.CanceladaCte)
                    {
                        //var nfe = EmaloteContta.Adapter.XmlToClassAdapter.GetNotaFiscalModCanc(file.FullName);
                        //nfe.FileInfo = file.FullName;
                        //nfe.FileName = file.Name;
                        //nfe.ETipoNota = Models.ETipoNota.Cancelada;
                        //if (!NotasFicaisEletronicasMod55.Any(c => c.ProtNFe.InfProt.ChNFe == nfe.Evento.InfEvento.ChNFe))
                        //{
                        //    try
                        //    {
                        //        NotasFiscalEletronicasCanc.Add(nfe);
                        //        cnpj = nfe.Evento.InfEvento.CNPJ;
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        Erros.Add($"Erro: {ex.Message}, arquivo: {file}");
                        //    }
                        //}
                        //else
                        //{
                        //    Erros.Add($"Erro: Arquivo já adicionado a lista, (Dupliciade de nota), arquivo {file.Name}");
                        //    MoveFile(file.FullName, file.Name, null, null, Models.ETipoNota.NotaFiscalEletronica, true);
                        //}

                    }
                    else
                    {
                        Erros.Add($"Erro: {msg}, arquivo: {file}");
                    }
                }
            }
        }

        private void MoveFile(string file, string fileName, string cnpj, DateTime? data, Models.ETipoNota tipo, bool erro)
        {
            string pathRoot = string.Empty;
            pathRoot = erro ? Path.Combine(OutputFolder, $"Erro") : Path.Combine(OutputFolder, $"{cnpj}/{data.Value.Year}/{data.Value.Month}/{tipo.ToString()}");
            var pathMove = Path.Combine(pathRoot, fileName);
            try
            {
                if (!Directory.Exists(pathRoot))
                    Directory.CreateDirectory(pathRoot);
                File.Move(file, pathMove);
                //if (tipo != Models.ETipoNota.NaoIdentificada && tipo != Models.ETipoNota.NotaFiscalDeServico && tipo != Models.ETipoNota.Cancelada)
                //{
                //    var t = Task.Run(() => GerarDanfe(pathMove, file, cnpj));
                //    t.Wait();
                //}

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Could not find file") || ex.Message.Contains("Unable to find the specified file."))
                {

                }
                else
                {
                    File.Copy(file, pathMove, true);
                    File.Delete(file);

                }

            }
        }

        private void MoveFileMod55(List<NfeProc> listaNfeMod55)
        {
            foreach (var item in listaNfeMod55)
            {
                string pathRoot = string.Empty;
                var data = DateTime.Parse(item.NFe.InfNFe.Ide.DhEmi);
                pathRoot = Path.Combine(OutputFolder, $"{item.CnpjEmitente}/{data.Year}/{data.Month}/{item.ETipoNota.ToString()}");

                var pathMove = Path.Combine(pathRoot, item.FileName);
                try
                {
                    if (!Directory.Exists(pathRoot))
                        Directory.CreateDirectory(pathRoot);
                    File.Move(item.FileInfo, pathMove);

                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("Could not find file") || ex.Message.Contains("Unable to find the specified file."))
                    {

                    }
                    else
                    {
                        File.Copy(item.FileInfo, pathMove, true);
                        File.Delete(item.FileInfo);

                    }
                }
            }
        }

        private async Task<bool> GerarDanfe(string pathMove, string fileName, string cnpj)
        {
            try
            {
                if (!pathMove.Contains("desconhecido"))
                {
                    var outPdfFilePath = Path.Combine(Path.Combine(@"C:\XmlIntegrado", Path.GetFileNameWithoutExtension(pathMove) + ".pdf"));
                    var model = DanfeViewModelCreator.CriarDeArquivoXml(pathMove);
                    model.InformacoesComplementares = "";

                    using (Danfe danfe = new Danfe(model))
                    {
                        GerarDanfe(outPdfFilePath, danfe);
                    }
                    await AmazonStorage.UploadFileAsync(outPdfFilePath, cnpj, "application/pdf");
                    //File.Delete(outPdfFilePath);
                    Console.WriteLine("Pdf Enviado para amazon");

                }
                return true;
            }
            catch (Exception ex)
            {
                if (!ex.Message.Contains("Object reference not set to an instance of an object."))
                    throw;
                return false;
            }
        }

        private static void GerarDanfe(string outPdfFilePath, Danfe danfe)
        {
            try
            {
                danfe.Gerar();
                danfe.Salvar(outPdfFilePath);
            }
            catch (Exception ex)
            {
                GerarDanfe(outPdfFilePath, danfe);

            }
        }

        private void Config()
        {
            if (string.IsNullOrEmpty(OutputFolder))
                Directory.CreateDirectory(OutputFolder);
            //throw new Exception("Configuração de saída inválida");
            if (!Directory.Exists(OutputFolder))
                Directory.CreateDirectory(OutputFolder);
            //throw new Exception("Diretório de saída não encontrado. por favor verificar caminho de saída informado.");
        }
        public List<CteProc> GetCtes()
        {
            return NotaFiscalMod57Ctes;
        }
        public List<EmaloteContta.Models.NotaFiscalConsumidorFinalMod65.NfeProc> GetNotaFiscalMod65()
        {
            return NotasFiscaisConsumidorFinal65;
        }
        public List<Models.NotaFiscalDeServico.GerarNfseResposta> GetNotasFicaisDeServico()
        {
            return NotasFicaisDeServico;
        }
        public List<EmaloteContta.Models.NotaFiscalEletronicaMod55.NfeProc> GetNotasFicaisEletronicas()
        {
            return NotasFicaisEletronicasMod55;
        }
        public List<EmaloteContta.Models.Devolucao.NotaFiscalDevolucaoMod55.NfeProc> GetNotasFicaisEletronicasDev()
        {
            return NotasFicaisEletronicasMod55Dev;
        }
        public List<EmaloteContta.Models.ProcEventoNFe> GetNotaFiscalModCanc()
        {
            return NotasFiscalEletronicasCanc;
        }
        public List<EmaloteContta.Models.CTeMod57.CteProc> GetNotaFiscalCteMod57()
        {
            return NotaFiscalMod57Ctes;
        }

        public List<string> GetErros()
        {
            if (EmaloteContta.Adapter.XmlToClassAdapter.Erros.Count > 0)
                EmaloteContta.Adapter.XmlToClassAdapter.Erros.ForEach(c => Erros.Add(c));
            return Erros;
        }
        public void ClearErros()
        {
            EmaloteContta.Adapter.XmlToClassAdapter.Erros.Clear();
        }



        private void MoverXmlDiretorioCliDef()
        {
            var qdt = 0;
            XmlDocument doc = new XmlDocument();
            RabbitmqXml rabbitmqXml = new RabbitmqXml();

            var diretorioGeral = Directory.GetFiles(_path, "*.xml", SearchOption.AllDirectories);

            var nfe = Adapter.XmlToClassAdapter.GetNotaFiscalMod55Geral(diretorioGeral);

            var path = Path.Combine(Ferramenta.AppDataJsonPath, nameof(NfeProc), $"{nameof(NfeProc)}.json");
            List<NfeProc> listaAnteriorJSON = new List<NfeProc>();
            if (File.Exists(path))
            {
                var jsonRead = File.ReadAllText(path);
                listaAnteriorJSON = JsonConvert.DeserializeObject<List<NfeProc>>(jsonRead);
            }

            var adicionados = nfe.Except(listaAnteriorJSON, new DtoEqualityComparer<NfeProc>()).ToList();

            foreach (var item in adicionados)
            {
                if (item.ETipoNota == Models.ETipoNota.NotaFiscalEletronica)
                {
                    if (item.CnpjEmitente == null)
                    {
                        Console.WriteLine("CNPJ INVALIDO");
                    }
                    else
                    {
                        NotasFicaisEletronicasMod55.Add(item);
                    }

                }

                Console.WriteLine($"QANTIDADE GRAVADA = {qdt = qdt + 1}");
            }

            rabbitmqXml.RabbitmqFiasMod55Geral(NotasFicaisEletronicasMod55);

            //Console.WriteLine("Criando Auditoria...");
            //if (adicionados.Count > 0)
            //{
            //    listaAnteriorJSON.AddRange(adicionados);
            //    Ferramenta.Gravar(listaAnteriorJSON, path);
            //}

            MoveXmlNotasFicaisEletronicasMod55();
        }

        public void MoveXmlNotasFicaisEletronicasMod55()
        {
            if (NotasFicaisEletronicasMod55.Count > 0)
            {
                try
                {
                    Thread t = new Thread(CriarPdf);
                    t.Start();

                    foreach (var item in NotasFicaisEletronicasMod55)
                    {
                        var dataEmit = "";
                        if (item.NFe.InfNFe.Ide.DhEmi != null)
                            dataEmit = item.NFe.InfNFe.Ide.DhEmi;
                        else
                        {
                            dataEmit = item.NFe.InfNFe.Ide.dEmi;
                        }

                        Task.Run(() => AmazonStorage.UploadFileAsync(item.FileName, item.CnpjEmitente, "application/xml"));
                    }

                    Console.WriteLine("Movendo XML...");
                    MoveFileMod55(NotasFicaisEletronicasMod55);
                    Console.WriteLine("Movendo XML Movidos");
                    CriarPdf();

                }
                catch (Exception ex)
                {
                    Erros.Add(ex.Message);
                }
            }
        }


        public void CriarPdf()
        {
            foreach (var item in NotasFicaisEletronicasMod55)
            {
                string pathRoot = string.Empty;
                var data = DateTime.Parse(item.NFe.InfNFe.Ide.DhEmi);
                pathRoot = Path.Combine(OutputFolder, $"{item.CnpjEmitente}/{data.Year}/{data.Month}/{item.ETipoNota.ToString()}");
                var pathMove = Path.Combine(pathRoot, item.FileName);
                try
                {
                    if (item.ETipoNota != Models.ETipoNota.NaoIdentificada && item.ETipoNota != Models.ETipoNota.NotaFiscalDeServico && item.ETipoNota != Models.ETipoNota.Cancelada)
                    {
                        var result = GerarDanfe(pathMove, item.FileInfo, item.CnpjEmitente);
                        if (result.Result)
                        {
                            Console.WriteLine("Pdf Gerado Com Sucesso");
                        }
                        else
                        {
                            Console.WriteLine("Erro Ao Enviar Pdf");
                        }

                    }

                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("Could not find file") || ex.Message.Contains("Unable to find the specified file."))
                    {

                    }
                    else
                    {
                        File.Copy(item.FileInfo, pathMove, true);
                        File.Delete(item.FileInfo);

                    }
                }
            }
        }

        public class DtoEqualityComparer<T> : IEqualityComparer<T> where T : NfeProc
        {
            public bool Equals(T dto1, T dto2)
            {
                if (dto2 == null && dto1 == null)
                {
                    return true;
                }
                else if (dto1 == null || dto2 == null)
                {
                    return false;
                }
                else if (dto1.NFe.InfNFe.Id == dto2.NFe.InfNFe.Id)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public int GetHashCode(T dto)
            {
                return 1;// dto.CodigoERP.GetHashCode();
            }
        }
    }
}
