//using System;
//using System.Collections.Generic;
//using ConttaComsumidor.Models.Speed;

//namespace ConttaComsumidor.Adapter
//{
//    public class EntidadeXmlToEntidadeMongodbModDev
//    {
//        public static EmpresaEmit CretaEntidadeMongoEmpresaEmitente(ConttaComsumidor.Models.Devolucao.NotaFiscalDevolucaoMod55.Emit emitente)
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
//                IncrEstadual = emitente.IE,
//                RegTrib = emitente.CRT

//            };
//        }

//        public static EmpresaDest CretaEntidadeMongoEmpresaDestinatario(ConttaComsumidor.Models.Devolucao.NotaFiscalDevolucaoMod55.Dest dest)
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
//                RazaoSocial = dest.XNome,
//                IncrEstadual = dest.IE,
//                CPF = !string.IsNullOrEmpty(dest.CNPJ) ? dest.CNPJ.Replace("-", "").Replace("-", "") : string.Empty,

//            };
//        }
//        public static NFE CreateEntidadeMongoNotaFiscal(Models.Devolucao.NotaFiscalDevolucaoMod55.NfeProc notaFiscalEletronicaModDev)
//        {

//            if (notaFiscalEletronicaModDev.NFe == null ||
//                notaFiscalEletronicaModDev.NFe.InfNFe == null ||
//                notaFiscalEletronicaModDev.NFe.InfNFe.Ide.NFref.RefNFe == null ||
//                notaFiscalEletronicaModDev.NFe.InfNFe.Total == null
//                )
//                throw new Exception("Erro no processamento do xml");

//            try
//            {
//                var nfe = new NFE
//                {
//                    BaseCAlIcms = (float.Parse(notaFiscalEletronicaModDev.NFe.InfNFe.Total.ICMSTot.VBC)),
//                    BaseCalIcmsSt = (float.Parse(notaFiscalEletronicaModDev.NFe.InfNFe.Total.ICMSTot.VBCST)),
//                    CodBarra = notaFiscalEletronicaModDev.ProtNFe.InfProt.ChNFe,
//                    DesOperacao = notaFiscalEletronicaModDev.NFe.InfNFe.Ide.NatOp,
//                    DhEmi = ExtraindoData(notaFiscalEletronicaModDev.NFe.InfNFe.Ide.DhEmi),
//                    Modelo = notaFiscalEletronicaModDev.NFe.InfNFe.Ide.Mod,
//                    NatOperacao = notaFiscalEletronicaModDev.NFe.InfNFe.Ide.NatOp,
//                    NFRef = notaFiscalEletronicaModDev.NFe.InfNFe.Ide.NFref.RefNFe,
//                    Serie = (int.Parse(notaFiscalEletronicaModDev.NFe.InfNFe.Ide.Serie)),
//                    Nnfe = (int.Parse(notaFiscalEletronicaModDev.NFe.InfNFe.Ide.NNF)),
//                    TipAten = notaFiscalEletronicaModDev.NFe.InfNFe.Ide.IndPres,
//                    TipNfe = (int.Parse(notaFiscalEletronicaModDev.NFe.InfNFe.Ide.TpNF)),
//                    VlAproxTributos = (double.Parse(notaFiscalEletronicaModDev.NFe.InfNFe.Total.ICMSTot.VTotTrib)),
//                    VlCofins = (double.Parse(notaFiscalEletronicaModDev.NFe.InfNFe.Total.ICMSTot.VCOFINS)),
//                    VlIpi = (double.Parse(notaFiscalEletronicaModDev.NFe.InfNFe.Total.ICMSTot.VIPI)),
//                    VlOutDes = (double.Parse(notaFiscalEletronicaModDev.NFe.InfNFe.Total.ICMSTot.VOutro)),
//                    VlPis = (double.Parse(notaFiscalEletronicaModDev.NFe.InfNFe.Total.ICMSTot.VPIS)),
//                    VlTotalDesc = (double.Parse(notaFiscalEletronicaModDev.NFe.InfNFe.Total.ICMSTot.VDesc)),
//                    VlTotalFrete = (double.Parse(notaFiscalEletronicaModDev.NFe.InfNFe.Total.ICMSTot.VFrete)),
//                    VlTotalPro = (double.Parse(notaFiscalEletronicaModDev.NFe.InfNFe.Total.ICMSTot.VProd)),
//                    VlTotalSeguro = (double.Parse(notaFiscalEletronicaModDev.NFe.InfNFe.Total.ICMSTot.VSeg)),
//                    VtIcms = (double.Parse(notaFiscalEletronicaModDev.NFe.InfNFe.Total.ICMSTot.VICMS)),
//                    VtIcmsSt = (double.Parse(notaFiscalEletronicaModDev.NFe.InfNFe.Total.ICMSTot.VST)),
//                    VtTotalNfe = (double.Parse(notaFiscalEletronicaModDev.NFe.InfNFe.Total.ICMSTot.VNF)),
//                    ETipoNota = notaFiscalEletronicaModDev.ETipoNota,
//                    ModeloTipo = notaFiscalEletronicaModDev.ModeloNota,
//                    CNPJEmitente = notaFiscalEletronicaModDev.CnpjEmitente

//                };

//                return nfe;
//            }
//            catch (Exception ex)
//            {

//                throw;
//            }
//        }

//        private static void ObterProdutosImposto(List<ConttaComsumidor.Models.Devolucao.NotaFiscalDevolucaoMod55.Det> det, NFE nfe)
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
//                produtos.Cfop = (double.Parse(item.Prod.CFOP));
//                produtos.UnidMedida = item.Prod.UCom;
//                produtos.Quantidade = (float.Parse(item.Prod.QCom));
//                produtos.VlUnitario = (double.Parse(item.Prod.VUnCom));
//                produtos.VlProduto = (double.Parse(item.Prod.VProd));
//                produtos.QtdTributaria = (float.Parse(item.Prod.QTrib));
//                produtos.Tributos = item.Prod.UTrib;
//                produtos.UniMedTributado = item.Prod.UTrib;
//                produtos.VlTlFrete = (double.Parse(item.Prod.VFrete));
//                produtos.VlUnitTributado = (double.Parse(item.Prod.VUnTrib));
//                if (importoCsosn.ICMSSN900 != null)
//                    produtos.Csons = Convert.ToInt32(importoCsosn.ICMSSN900.CSOSN);
//                else
//                    produtos.Csons = 0;

//                //nfe.Produtos.Add(produtos);

//                var imposto = item.Imposto;
//                if (imposto != null)
//                {
//                    imp.ProdutoId = produtos._id.Value;

//                    if (imposto.IPI != null)
//                    {
//                        var ipi = imposto.IPI;
//                        if (ipi != null)
//                        {
//                            imp.CSelo = ipi.CSelo;
//                            imp.CEnq = ipi.CEnq;
//                            imp.CST = ipi.IPINT.CST;
//                        }
//                    }

//                    if (imposto.PIS != null)
//                    {
//                        var pis = imposto.PIS;
//                        if (pis != null)
//                        {
//                            imp.CstPis = imposto.PIS.PISNT.CST;
//                        }
//                    }

//                    if (imposto.COFINS != null)
//                    {
//                        var cofins = imposto.COFINS;
//                        if (cofins != null)
//                        {
//                            imp.CstCofins = imposto.COFINS.COFINSNT.CST;
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

//        public static List<Produtos> CriateProdutos(List<ConttaComsumidor.Models.Devolucao.NotaFiscalDevolucaoMod55.Det> dets, DateTime? DhEmt)
//        {
//            List<Produtos> listaProdudos = new List<Produtos>();

//            foreach (var item in dets)
//            {
//                var importoCsosn = item.Imposto.ICMS;

//                var produtos = new Produtos();
//                produtos.CodProduto = item.Prod.CProd;
//                produtos.DescProduto = item.Prod.XProd;
//                produtos.NcmProd = item.Prod.NCM;
//                produtos.Ean = item.Prod.CEAN;
//                produtos.Cfop = (double.Parse(item.Prod.CFOP));
//                produtos.UnidMedida = item.Prod.UCom;
//                produtos.Quantidade =(float.Parse(item.Prod.QCom));
//                produtos.VlUnitario = (double.Parse(item.Prod.VUnCom));
//                produtos.VlProduto = (double.Parse(item.Prod.VProd));                            
//                produtos.UniMedTributado = item.Prod.UTrib;                            
//                produtos.VlTlFrete = (double.Parse(item.Prod.VFrete));
//                produtos.VlTlSeguro = (double.Parse(item.Prod.VFrete));
//                produtos.NcmMono = false;
//                produtos.IcmsSt = false;
//                produtos.Beneficios = false;
//                produtos.Isento = false;
//                produtos.Imune = false;
//                produtos.ExigSuspensa = false;
//                produtos.LancamentoOficio = false;
//                produtos.IsencaoReducao = false;
//                produtos.IsencaoReducaoCestaBasica = false;
//                produtos.AntEncTributacao = false;
//                produtos.ExigSuspensaMono = false;
//                produtos.SubTributariaMono = false;
//                produtos.LancamentoOficioMono = false;
//                produtos.VlUnitTributado = (double.Parse(item.Prod.VUnTrib));
//                produtos.DhEmt = DhEmt;
//                if (importoCsosn.ICMSSN900 != null)
//                    produtos.Csons = Convert.ToInt32(importoCsosn.ICMSSN900.CSOSN);
//                else
//                    produtos.Csons = 0;

//                listaProdudos.Add(produtos);
//            }

//            return listaProdudos;
//        }

//        public static List<Impostos> CriateImpostos(List<ConttaComsumidor.Models.Devolucao.NotaFiscalDevolucaoMod55.Det> impostos)
//        {
//            List<Impostos> listaImpost = new List<Impostos>();

//            foreach (var item in impostos)
//            {
//                var imp = new Impostos();
//                if (item.Imposto.ICMS != null)
//                {
//                    if (item.Imposto.IPI != null)
//                    {
//                        var ipi = item.Imposto.IPI;
//                        if (ipi != null)
//                        {
//                            imp.CSelo = ipi.CSelo;
//                            imp.CEnq = ipi.CEnq;
//                            imp.CST = ipi.IPINT.CST;
//                        }
//                    }

//                    if (item.Imposto.PIS != null)
//                    {
//                        var pis = item.Imposto.PIS;
//                        if (pis != null)
//                        {
//                            imp.CstPis = item.Imposto.PIS.PISNT.CST;
//                        }
//                    }

//                    if (item.Imposto.COFINS != null)
//                    {
//                        var cofins = item.Imposto.COFINS;
//                        if (cofins != null)
//                        {
//                            imp.CstCofins = item.Imposto.COFINS.COFINSNT.CST;
//                        }
//                    }
//                }

//                listaImpost.Add(imp);
//            }

//            return listaImpost;
//        }
//    }
//}
