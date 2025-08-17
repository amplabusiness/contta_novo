//using ConttaComsumidor.Models.CTeMod57;
//using Corporate.Contta.Schedule.Domain.Entities.NfeAgg;
//using System;
//using System.Collections.Generic;

//namespace ConttaComsumidor.Adapter
//{
//    public class EntidadeXmlToEntidadeMongodbMod65
//    {
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
//                Complemento = emitente.EnderEmit != null ? emitente.EnderEmit.XCpl : string.Empty,
//                Logradouro = emitente.EnderEmit != null ? emitente.EnderEmit.XLgr : string.Empty,
//                Numero = emitente.EnderEmit != null ? emitente.EnderEmit.Nro : string.Empty,
//                Uf = emitente.EnderEmit != null ? emitente.EnderEmit.UF : string.Empty,
//                Cnpj = emitente.CNPJ.Replace("-", "").Replace("/", "").Replace("-", ""),
//                Fantasia = emitente.XFant,
//                RazaoSocial = emitente.XNome,
//                IncrEstadual = emitente.IE,
//                RegTrib = emitente.CRT

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
//                Complemento = dest.EnderDest != null ? dest.EnderDest.XCpl : string.Empty,
//                Logradouro = dest.EnderDest != null ? dest.EnderDest.XLgr : string.Empty,
//                Numero = dest.EnderDest != null ? dest.EnderDest.Nro : string.Empty,
//                Uf = dest.EnderDest != null ? dest.EnderDest.UF : string.Empty,
//                Cnpj = !string.IsNullOrEmpty(dest.CNPJ) ? dest.CNPJ.Replace("-", "").Replace("/", "").Replace("-", "") : string.Empty,              
//                RazaoSocial = dest.XNome

//            };
//        }
//        public static NFE CreateEntidadeMongoNotaFiscal(Models.NotaFiscalConsumidorFinalMod65.NfeProc notaFiscalEletronicaMod65)
//        {
//            if (notaFiscalEletronicaMod65.NFe == null ||
//                notaFiscalEletronicaMod65.NFe.InfNFe == null ||
//                notaFiscalEletronicaMod65.NFe.InfNFe.Total == null
//                )
//                throw new Exception("Erro no processamento do xml");

//            var nfe = new NFE
//            {
//                BaseCAlIcms = (float.Parse( notaFiscalEletronicaMod65.NFe.InfNFe.Total.ICMSTot.VBC)),
//                BaseCalIcmsSt =(float.Parse( notaFiscalEletronicaMod65.NFe.InfNFe.Total.ICMSTot.VBCST)),
//                CodBarra = notaFiscalEletronicaMod65.NFe.InfNFe.Id.Substring(3),
//                DesOperacao = notaFiscalEletronicaMod65.NFe.InfNFe.Ide.NatOp,
//                DhEmi = ExtraindoData(notaFiscalEletronicaMod65.NFe.InfNFe.Ide.DhEmi), 
//                Modelo = notaFiscalEletronicaMod65.NFe.InfNFe.Ide.Mod,
//                NatOperacao = notaFiscalEletronicaMod65.NFe.InfNFe.Ide.NatOp,               
//                Serie =(int.Parse( notaFiscalEletronicaMod65.NFe.InfNFe.Ide.Serie)),
//                Nnfe = (int.Parse( notaFiscalEletronicaMod65.NFe.InfNFe.Ide.NNF)),
//                TipAten = notaFiscalEletronicaMod65.NFe.InfNFe.Ide.IndPres,
//                TipNfe =(int.Parse( notaFiscalEletronicaMod65.NFe.InfNFe.Ide.TpNF)),               
//                VlCofins = (double.Parse(notaFiscalEletronicaMod65.NFe.InfNFe.Total.ICMSTot.VCOFINS)),
//                VlIpi = (double.Parse(notaFiscalEletronicaMod65.NFe.InfNFe.Total.ICMSTot.VIPI)),
//                VlOutDes = (double.Parse(notaFiscalEletronicaMod65.NFe.InfNFe.Total.ICMSTot.VOutro)),
//                VlPis = (double.Parse(notaFiscalEletronicaMod65.NFe.InfNFe.Total.ICMSTot.VPIS)),
//                VlTotalDesc = (double.Parse(notaFiscalEletronicaMod65.NFe.InfNFe.Total.ICMSTot.VDesc)),
//                VlTotalFrete = (double.Parse(notaFiscalEletronicaMod65.NFe.InfNFe.Total.ICMSTot.VFrete)),
//                VlTotalPro = (double.Parse(notaFiscalEletronicaMod65.NFe.InfNFe.Total.ICMSTot.VProd)),
//                VlTotalSeguro = (double.Parse(notaFiscalEletronicaMod65.NFe.InfNFe.Total.ICMSTot.VSeg)),
//                VtIcms = (double.Parse(notaFiscalEletronicaMod65.NFe.InfNFe.Total.ICMSTot.VICMS)),
//                VtIcmsSt = (double.Parse(notaFiscalEletronicaMod65.NFe.InfNFe.Total.ICMSTot.VST)),
//                VtTotalNfe = (double.Parse(notaFiscalEletronicaMod65.NFe.InfNFe.Total.ICMSTot.VNF)),
//                ETipoNota = notaFiscalEletronicaMod65.ETipoNota
//            };
//            ObterProdutosImposto(notaFiscalEletronicaMod65.NFe.InfNFe.Det, nfe);
//            return nfe;
//        }

//        private static void ObterProdutosImposto(List<Models.NotaFiscalConsumidorFinalMod65.Det> det, NFE nfe)
//        {
//            foreach (var item in det)
//            {
//                var imp = new Impostos();

//                var importoCsosn = item.Imposto.ICMS;

//                // colocar o id do produto e do imposto pra ser populado dentro da classe ou aqui.
//                var produtos = new Produtos();
//                //nfe.Produtos.Add(new Produtos
//                //{
//                produtos.CodProduto = item.Prod.CProd;
//                produtos.DescProduto = item.Prod.XProd;
//                produtos.NcmProd = item.Prod.NCM;
//                produtos.Ean = item.Prod.CEAN;
//                produtos.Cfop =(double.Parse( item.Prod.CFOP));
//                produtos.UnidMedida = item.Prod.UCom;
//                produtos.Quantidade =(float.Parse(item.Prod.QCom));
//                produtos.VlUnitario = (double.Parse(item.Prod.VUnCom));
//                produtos.VlProduto =(double.Parse(item.Prod.VProd));             
         
               
//                produtos.QtdTributaria =(float.Parse(item.Prod.QTrib));
               
//                produtos.UniMedTributado = item.Prod.UTrib;               
//                //ToDo:Modificar para NcmMono como false 
//                produtos.NcmMono = true;
//                produtos.IcmsSt = false;
//                produtos.Beneficios = false;
//                produtos.VlUnitTributado =(double.Parse(item.Prod.VUnTrib));
//                if (importoCsosn.ICMSSN102 != null)
//                    produtos.Csons = Convert.ToInt32(importoCsosn.ICMSSN102.CSOSN);
//                else
//                    produtos.Csons = 0;

//                //nfe.Produtos.Add(produtos);                

//                var imposto = item.Imposto;
//                if (imposto != null)
//                {
//                    imp.ProdutoId = produtos._id.Value;
//                    if (imposto.ICMS != null)
//                    {
//                        var icmsSn102 = imposto.ICMS.ICMSSN102;
//                        if (icmsSn102 != null)
//                        {
//                            imp.Origem = icmsSn102.Orig;
//                            imp.Csosn = icmsSn102.CSOSN;

//                        }
                     
//                    }
//                    if (imposto.PIS != null)
//                    {
//                        var pis = imposto.PIS.PISOutr;
//                        if (pis != null)
//                        {
//                            imp.Ipi = (double.Parse(pis.PPIS));                            
//                            imp.CST = pis.CST;
//                            imp.VBC = pis.VBC;
//                            imp.VPIS = pis.VPIS;
//                        }

//                        var confins = imposto.COFINS.COFINSOutr;
//                        if(confins != null)
//                        {
//                            imp.CstCofins = confins.CST;
//                            imp.CvBC = confins.VBC;
//                            imp.PCOFINS = confins.PCOFINS;
//                            imp.VCOFINS = confins.VCOFINS;
//                        }

//                    }
//                    //nfe.Impostos.Add(imp);
//                }
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
