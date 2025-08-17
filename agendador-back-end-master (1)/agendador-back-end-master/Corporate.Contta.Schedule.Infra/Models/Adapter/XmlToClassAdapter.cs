//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Xml;
//using System.Xml.Serialization;
//using ConttaComsumidor.Models.CTeMod57;
//using ConttaComsumidor.Models.NotaFiscalDeServico;
//using ConttaComsumidor.Servico;

//namespace ConttaComsumidor.Adapter
//{
//    public class XmlToClassAdapter
//    {
//        public static List<string> Erros = new List<string>();

//        public static GerarNfseResposta GetNotaFiscalDeServico(string file)
//        {
//            try
//            {
//                XmlSerializer serializer = new XmlSerializer(typeof(GerarNfseResposta));
//                StreamReader stringReader = new StreamReader(file);
//                var nfs = (GerarNfseResposta)serializer.Deserialize(stringReader);
//                stringReader.Close();
//                return nfs;
//            }
//            catch (Exception ex)
//            {
//                Erros.Add($"Erro: {ex.Message}, arquivo: {file}");
//                return null;
//            }
//        }

//        public static GerarNfseResposta GetNotaFiscalMod65(string file)
//        {
//            try
//            {
//                XmlDocument doc = new XmlDocument();
//                RabbitmqXml rabbitmqXml = new RabbitmqXml();
//                XmlSerializer serializer = new XmlSerializer(typeof(ConttaComsumidor.Models.CTeMod57.CTe));
//                StreamReader stringReader = new StreamReader(file);
//                var nfeCF = (GerarNfseResposta)serializer.Deserialize(stringReader);
//                var nfesDto = JsonConvert.SerializeObject(nfeCF);
//                //rabbitmqXml.RabbitmqFiasMod57(nfesDto);
//                stringReader.Close();
//                return nfeCF;
//            }
//            catch (Exception ex)
//            {
//                Erros.Add($"Erro: {ex.Message}, arquivo: {file}");
//                return null;
//            }
//        }

//        public static CteProc GetNotaFiscalCteMod57(string file)
//        {
//            try
//            {
          
//                XmlSerializer serializer = new XmlSerializer(typeof(CteProc));
//                StreamReader stringReader = new StreamReader(file);
//                var cte = (CteProc)serializer.Deserialize(stringReader);               
//                stringReader.Close();
//                return cte;
//            }
//            catch (Exception ex)
//            {
//                Erros.Add($"Erro: {ex.Message}, arquivo: {file}");
//                return null;
//            }
//        }

//        public static Models.NotaFiscalEletronicaMod55.NfeProc GetNotaFiscalMod55(string file)
//        {
//            try
//            {
               
//                XmlSerializer serializer = new XmlSerializer(typeof(ConttaComsumidor.Models.NotaFiscalEletronicaMod55.NfeProc));
//                StreamReader stringReader = new StreamReader(file);
//                var nfes = (Models.NotaFiscalEletronicaMod55.NfeProc)serializer.Deserialize(stringReader);
              
//                stringReader.Close();
//                return nfes;
//            }
//            catch (Exception ex)
//            {
//                Erros.Add($"Erro: {ex.Message}, arquivo: {file}");
//                return null;
//            }
//        }

//        public static Models.Devolucao.NotaFiscalDevolucaoMod55.ProtNFe GetNotaFiscalModDev(string file)
//        {
//            try
//            {
//                XmlSerializer serializer = new XmlSerializer(typeof(Models.Devolucao.NotaFiscalDevolucaoMod55.ProtNFe));
//                StreamReader stringReader = new StreamReader(file);
//                var nfes = (Models.Devolucao.NotaFiscalDevolucaoMod55.ProtNFe)serializer.Deserialize(stringReader);
              
//                stringReader.Close();
//                return nfes;
//            }
//            catch (Exception ex)
//            {
//                Erros.Add($"Erro: {ex.Message}, arquivo: {file}");
//                return null;
//            }
//        }

//        public static ConttaComsumidor.Models.NotaFiscalCanceladas.ProcEventoNFe GetNotaFiscalModCanc(string file)
//        {
//            try
//            {               
//                XmlSerializer serializer = new XmlSerializer(typeof(ConttaComsumidor.Models.NotaFiscalCanceladas.ProcEventoNFe));
//                StreamReader stringReader = new StreamReader(file);
//                var nfes = (ConttaComsumidor.Models.NotaFiscalCanceladas.ProcEventoNFe)serializer.Deserialize(stringReader);             
//                stringReader.Close();
//                return nfes;
//            }
//            catch (Exception ex)
//            {
//                Erros.Add($"Erro: {ex.Message}, arquivo: {file}");
//                return null;
//            }
//        }

//    }
//}
