//using System;
//using System.Collections.Generic;
//using ConttaComsumidor.Models.CTeMod57;
//using ConttaComsumidor.Models.Speed;

//namespace ConttaComsumidor.Adapter
//{
//    public class EntidadeXmlToEntidadeMongodbMod57
//    {
//        public static EmpresaRem CretaEntidadeMongoEmpresaRem(Rem remetente)
//        {
//            if (remetente == null)
//                throw new Exception("Emitente nao informado.");
//            if (string.IsNullOrEmpty(remetente.CNPJ))
//                throw new Exception("Cnpj do emitente nao informado.");

//            return new EmpresaRem
//            {
//                Ativo = true,
//                Bairro = remetente.EnderReme != null ? remetente.EnderReme.XBairro : string.Empty,
//                Cep = remetente.EnderReme != null ? remetente.EnderReme.CEP : string.Empty,
//                Cidade = remetente.EnderReme != null ? remetente.EnderReme.XMun : string.Empty,             
//                Logradouro = remetente.EnderReme != null ? remetente.EnderReme.XLgr : string.Empty,
//                Numero = remetente.EnderReme != null ? remetente.EnderReme.Nro : string.Empty,
//                Uf = remetente.EnderReme != null ? remetente.EnderReme.UF : string.Empty,
//                Cnpj = remetente.CNPJ.Replace("-", "").Replace("/", "").Replace("-", ""),              
//                RazaoSocial = remetente.XNome,
//                IncrEstadual = remetente.IE              

//            };
//        }

//        public static EmpresaEmit CretaEntidadeMongoEmpresaEmitente(Emit emitente)
//        {
//            if (emitente == null)
//                throw new Exception("Emitente nao informado.");
//            if (string.IsNullOrEmpty(emitente.CNPJ))
//                throw new Exception("Cnpj do emitente nao informado.");

//            return new EmpresaEmit
//            {
//                Ativo = true,
//                Bairro = emitente.EnderEmit != null ? emitente.EnderEmit.XBairro : string.Empty,
//                Cep = emitente.EnderEmit != null ? emitente.EnderEmit.CEP : string.Empty,
//                Cidade = emitente.EnderEmit != null ? emitente.EnderEmit.XMun : string.Empty,                
//                Logradouro = emitente.EnderEmit != null ? emitente.EnderEmit.XLgr : string.Empty,
//                Numero = emitente.EnderEmit != null ? emitente.EnderEmit.Nro : string.Empty,
//                Uf = emitente.EnderEmit != null ? emitente.EnderEmit.UF : string.Empty,
//                Cnpj = emitente.CNPJ.Replace("-", "").Replace("/", "").Replace("-", ""),
//                Fantasia = emitente.XFant,
//                RazaoSocial = emitente.XNome,
//                IncrEstadual = emitente.IE    
//            };
//        }

//        public static EmpresaDest CretaEntidadeMongoEmpresaDestinatario(Dest dest)
//        {
//            if (dest == null)
//                throw new Exception("Destinatário nao informado.");
//            if (string.IsNullOrEmpty(dest.CNPJ) && string.IsNullOrEmpty(dest.CNPJ))
//                throw new Exception("Cnpj ou cpf do destinatário nao informado.");

//            return new EmpresaDest
//            {
//                Ativo = true,
//                Bairro = dest.EnderDest != null ? dest.EnderDest.XBairro : string.Empty,
//                Cep = dest.EnderDest != null ? dest.EnderDest.CEP : string.Empty,
//                Cidade = dest.EnderDest != null ? dest.EnderDest.XMun : string.Empty,                
//                Logradouro = dest.EnderDest != null ? dest.EnderDest.XLgr : string.Empty,
//                Numero = dest.EnderDest != null ? dest.EnderDest.Nro : string.Empty,
//                Uf = dest.EnderDest != null ? dest.EnderDest.UF : string.Empty,
//                Cnpj = !string.IsNullOrEmpty(dest.CNPJ) ? dest.CNPJ.Replace("-", "").Replace("/", "").Replace("-", "") : string.Empty,                
//                RazaoSocial = dest.XNome,
//                IncrEstadual = dest.IE            

//            };
//        }
//        public static NFE CreateEntidadeMongoNotaFiscal(Models.CTeMod57.CteProc notaFiscalEletronicaMod57)
//        {
//            if (notaFiscalEletronicaMod57.CTe == null ||
//                notaFiscalEletronicaMod57.CTe.InfCte == null ||
//                notaFiscalEletronicaMod57.CTe.InfCte.VPrest.VTPrest == null
//                )
//                throw new Exception("Erro no processamento do xml");

//            var imp = new Impostos();

//            imp.Icms = (double.Parse(notaFiscalEletronicaMod57.CTe.InfCte.Imp.ICMS.ICMS45.CST));

//            var nfe = new NFE
//            {             
//                CodBarra = notaFiscalEletronicaMod57.CTe.InfCte.Id.Substring(3),
//                DesOperacao = notaFiscalEletronicaMod57.CTe.InfCte.Ide.NatOp,
//                DhEmi = ExtraindoData(notaFiscalEletronicaMod57.CTe.InfCte.Ide.DhEmi),
//                DhSaida = ExtraindoData(notaFiscalEletronicaMod57.ProtCTe.InfProt.DhRecbto),           
//                Modelo = notaFiscalEletronicaMod57.CTe.InfCte.Ide.Mod,
//                NatOperacao = notaFiscalEletronicaMod57.CTe.InfCte.Ide.NatOp,
//                Serie =(int.Parse(notaFiscalEletronicaMod57.CTe.InfCte.Ide.Serie)),
//                CMunEnv = notaFiscalEletronicaMod57.CTe.InfCte.Ide.CMunEnv,
//                XMunEnv = notaFiscalEletronicaMod57.CTe.InfCte.Ide.XMunEnv,
//                UFEnv = notaFiscalEletronicaMod57.CTe.InfCte.Ide.UFEnv,
//                CMunIni = notaFiscalEletronicaMod57.CTe.InfCte.Ide.CMunIni,
//                XMunFim = notaFiscalEletronicaMod57.CTe.InfCte.Ide.XMunFim,
//                UFIni = notaFiscalEletronicaMod57.CTe.InfCte.Ide.UFIni,
//                CMunFim = notaFiscalEletronicaMod57.CTe.InfCte.Ide.CMunFim,
//                VPrest = (double.Parse( notaFiscalEletronicaMod57.CTe.InfCte.VPrest.VTPrest)),
//                ETipoNota = notaFiscalEletronicaMod57.ETipoNota
//            };
//            ObterProdutosImposto(notaFiscalEletronicaMod57.CTe.InfCte.InfCTeNorm, nfe, imp);
//            return nfe;
//        }

//        private static void ObterProdutosImposto(List<InfCTeNorm> det, NFE nfe,Impostos impostos)
//        {

//            List<InfQuantidade> listInfDoc = new List<InfQuantidade>();

//            foreach (var item in det)
//            {
//                var imp = new Impostos();
//                var infDoc = new InfoCarga();               

//                foreach (var infCod in item.InfCarga.InfQ)
//                {
//                    listInfDoc.Add(new InfQuantidade()
//                    {
//                        CUnid = infCod.CUnid,
//                        tpMed = infCod.TpMed,
//                        QCarga  = infCod.QCarga
                       
//                    });
//                }

//                infDoc.VolCarga = item.InfCarga.VCarga;
//                infDoc.ProPred = item.InfCarga.ProPred;             
//                infDoc.IfDocMod = item.InfDoc.InfNF.Mod;             
//                infDoc.IfDocnum = item.InfDoc.InfNF.NDoc;              
//                infDoc.DEmi = item.InfDoc.InfNF.DEmi; 
//                infDoc.IfDocVBC = item.InfDoc.InfNF.VBC;              
//                infDoc.IfDocvICMS = item.InfDoc.InfNF.VICMS;              
//                infDoc.IfDocvBCST = item.InfDoc.InfNF.VBCST;              
//                infDoc.IfDocvvST = item.InfDoc.InfNF.VST;              
//                infDoc.IfDocvvProd = item.InfDoc.InfNF.VProd;              
//                infDoc.IfDocvvNF = item.InfDoc.InfNF.VNF;              
//                infDoc.IfDocvnCFOP = item.InfDoc.InfNF.NCFOP;              
//                infDoc.IfDocvnPeso = item.InfDoc.InfNF.NPeso;   

               
//                //if (impostos != null)
//                //{                    
//                //    nfe.Impostos.Add(imp);
//                //}
//            }
//        }

//        private static DateTime? ExtraindoData(string data)
//        {
//            var dataConvertida = new DateTime();
//            if (!string.IsNullOrEmpty(data))
//            {
//                var result = DateTime.TryParse(data, out dataConvertida);
//                if (result)
//                    return dataConvertida;
//                else
//                    return null;
//            }
//            return null;
//        }
//    }
//}
