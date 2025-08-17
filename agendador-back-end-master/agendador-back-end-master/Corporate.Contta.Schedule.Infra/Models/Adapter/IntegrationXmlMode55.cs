using Corporate.Contta.Schedule.Domain.Entities.NfeAgg;
using Corporate.Contta.Schedule.Domain.Entities.Product;
using Corporate.Contta.Schedule.Infra.Models.ModeloXml.NotaFiscalEletronicaMod55;
using System;
using System.Collections.Generic;

namespace Corporate.Contta.Schedule.Infra.Models.Adapter
{
    public class IntegrationXmlMode55
    {
        public static EmpresaEmit CretaEntidadeMongoEmpresaEmitente(Emit emitente)
        {
            if (emitente == null)
                throw new Exception("Emitente nao informado.");
            if (string.IsNullOrEmpty(emitente.CNPJ))
                throw new Exception("Cnpj do emitente nao informado.");

            return new EmpresaEmit
            {
                Ativo = true,
                Bairro = emitente.EnderEmit != null ? emitente.EnderEmit.XBairro : string.Empty,
                Cep = emitente.EnderEmit != null ? emitente.EnderEmit.CEP : string.Empty,
                Cidade = emitente.EnderEmit != null ? emitente.EnderEmit.XMun : string.Empty,
                Complemento = emitente.EnderEmit != null ? emitente.EnderEmit.XCpl : string.Empty,
                Logradouro = emitente.EnderEmit != null ? emitente.EnderEmit.XLgr : string.Empty,
                Numero = emitente.EnderEmit != null ? emitente.EnderEmit.Nro : string.Empty,
                Uf = emitente.EnderEmit != null ? emitente.EnderEmit.UF : string.Empty,
                Cnpj = emitente.CNPJ.Replace("-", "").Replace("/", "").Replace("-", ""),
                Fantasia = emitente.XFant,
                RazaoSocial = emitente.XNome,
                IncrEstadual = emitente.IE,
                RegTrib = emitente.CRT
            };
        }

        public static EmpresaDest CretaEntidadeMongoEmpresaDestinatario(Dest dest)
        {
            if (dest == null)
                throw new Exception("Destinatário nao informado.");
            if (string.IsNullOrEmpty(dest.CPF) && string.IsNullOrEmpty(dest.CNPJ))
                throw new Exception("Cnpj ou cpf do destinatário nao informado.");

            return new EmpresaDest
            {
                Ativo = true,
                Bairro = dest.EnderDest != null ? dest.EnderDest.XBairro : string.Empty,
                Cep = dest.EnderDest != null ? dest.EnderDest.CEP : string.Empty,
                Cidade = dest.EnderDest != null ? dest.EnderDest.XMun : string.Empty,
                Complemento = dest.EnderDest != null ? dest.EnderDest.XCpl : string.Empty,
                Logradouro = dest.EnderDest != null ? dest.EnderDest.XLgr : string.Empty,
                Numero = dest.EnderDest != null ? dest.EnderDest.Nro : string.Empty,
                Uf = dest.EnderDest != null ? dest.EnderDest.UF : string.Empty,
                Cnpj = !string.IsNullOrEmpty(dest.CNPJ) ? dest.CNPJ.Replace("-", "").Replace("/", "").Replace("-", "") : string.Empty,
                Fantasia = dest.XFant,
                RazaoSocial = dest.XNome,
                IncrEstadual = dest.IE,
                InscEstSubTribu = dest.IEST,
                Estrangeiro = dest.IdEstrangeiro != null ? true : false,
                CPF = !string.IsNullOrEmpty(dest.CPF) ? dest.CPF.Replace("-", "").Replace("-", "") : string.Empty,

            };
        }
        public static NFE CreateEntidadeMongoNotaFiscal(NfeProc notaFiscalEletronicaMod55)
        {
            string uf = "";
            string tipoNfe = "";
            var nfe = new NFE();
            var tranMercadoria = false;
            var tipoPagamento = "";
            var valorPagamento = "";
            var tranferenciaMercadoria = notaFiscalEletronicaMod55.NFe.InfNFe.Ide.NatOp;
            if (tranferenciaMercadoria == "TRANSFERENCIAPARAFILIAL")
                tranMercadoria = true;
            else if (notaFiscalEletronicaMod55.ModeloNota == "Saida")
                tipoNfe = "Venda";
            else if (notaFiscalEletronicaMod55.ModeloNota == "Entrada")
                tipoNfe = "Entrada";
            else if (tranferenciaMercadoria.Contains("Devolucao") || tranferenciaMercadoria.Contains("DEVOLUCAO") && notaFiscalEletronicaMod55.ModeloNota == "Saida")
                tipoNfe = "DevolucaoSaida";
            else
            {
                tipoNfe = "DevolucaoCompra";
            }
            if (notaFiscalEletronicaMod55.NFe == null ||
                notaFiscalEletronicaMod55.NFe.InfNFe == null ||
                notaFiscalEletronicaMod55.NFe.InfNFe.Total == null
                )
                throw new Exception("Erro no processamento do xml");

            uf = GetUf(notaFiscalEletronicaMod55, uf);

            if (notaFiscalEletronicaMod55.NFe.InfNFe.Ide.DhEmi != null)
            {
                if (notaFiscalEletronicaMod55.NFe.InfNFe.Versao == "4.00")
                {
                    tipoPagamento = notaFiscalEletronicaMod55.NFe.InfNFe.Pag.DetPag.TPag;
                    valorPagamento = notaFiscalEletronicaMod55.NFe.InfNFe.Pag.DetPag.VPag;
                }

                nfe = new NFE
                {
                    UFEnv = uf,
                    TPag = tipoPagamento,
                    VPag = valorPagamento,
                    BaseCAlIcms = notaFiscalEletronicaMod55.NFe.InfNFe.Total.ICMSTot.VBC,
                    BaseCalIcmsSt = notaFiscalEletronicaMod55.NFe.InfNFe.Total.ICMSTot.VBCST,
                    CodBarra = notaFiscalEletronicaMod55.NFe.InfNFe.Id.Substring(3),
                    DesOperacao = notaFiscalEletronicaMod55.NFe.InfNFe.Ide.NatOp,
                    DhEmi = Convert.ToDateTime(notaFiscalEletronicaMod55.NFe.InfNFe.Ide.DhEmi),
                    DhSaida = ExtraindoData(notaFiscalEletronicaMod55.NFe.InfNFe.Ide.DhSaiEnt),
                    FormPag = notaFiscalEletronicaMod55.NFe.InfNFe.Ide.IndPag,
                    Modelo = notaFiscalEletronicaMod55.NFe.InfNFe.Ide.Mod,
                    NatOperacao = notaFiscalEletronicaMod55.NFe.InfNFe.Ide.NatOp,
                    NfConFInal = notaFiscalEletronicaMod55.NFe.InfNFe.Ide.IndFinal,
                    Serie = notaFiscalEletronicaMod55.NFe.InfNFe.Ide.Serie,
                    Nnfe = notaFiscalEletronicaMod55.NFe.InfNFe.Ide.NNF,
                    TipAten = notaFiscalEletronicaMod55.NFe.InfNFe.Ide.IndPres,
                    TipNfe = notaFiscalEletronicaMod55.NFe.InfNFe.Ide.TpNF,
                    VlAproxTributos = notaFiscalEletronicaMod55.NFe.InfNFe.Total.ICMSTot.VTotTrib,
                    VlCofins = notaFiscalEletronicaMod55.NFe.InfNFe.Total.ICMSTot.VCOFINS,
                    VlIpi = notaFiscalEletronicaMod55.NFe.InfNFe.Total.ICMSTot.VIPI,
                    VlOutDes = notaFiscalEletronicaMod55.NFe.InfNFe.Total.ICMSTot.VOutro,
                    VlPis = notaFiscalEletronicaMod55.NFe.InfNFe.Total.ICMSTot.VPIS,
                    VlTotalDesc = notaFiscalEletronicaMod55.NFe.InfNFe.Total.ICMSTot.VDesc,
                    VlTotalFrete = notaFiscalEletronicaMod55.NFe.InfNFe.Total.ICMSTot.VFrete,
                    VlTotalPro = notaFiscalEletronicaMod55.NFe.InfNFe.Total.ICMSTot.VProd,
                    VlTotalSeguro = notaFiscalEletronicaMod55.NFe.InfNFe.Total.ICMSTot.VSeg,
                    VtIcms = notaFiscalEletronicaMod55.NFe.InfNFe.Total.ICMSTot.VICMS,
                    VtIcmsSt = notaFiscalEletronicaMod55.NFe.InfNFe.Total.ICMSTot.VST,
                    VtTotalNfe = notaFiscalEletronicaMod55.NFe.InfNFe.Total.ICMSTot.VNF,
                    ETipoNota = notaFiscalEletronicaMod55.ETipoNota,
                    FinNFe = (int.Parse(notaFiscalEletronicaMod55.NFe.InfNFe.Ide.FinNFe)),
                    ModeloTipo = tipoNfe,
                    TranMercadoria = tranMercadoria,
                    CNPJEmitente = notaFiscalEletronicaMod55.NFe.InfNFe.Emit.CNPJ,
                    Status = ""

                };
            }

            return nfe;
        }

        private static string GetUf(NfeProc notaFiscalEletronicaMod55, string uf)
        {
            if (notaFiscalEletronicaMod55.NFe.InfNFe.Ide.CUF == "11")
                uf = "RO";
            else if (notaFiscalEletronicaMod55.NFe.InfNFe.Ide.CUF == "12")
                uf = "AC";
            else if (notaFiscalEletronicaMod55.NFe.InfNFe.Ide.CUF == "13")
                uf = "AM";
            else if (notaFiscalEletronicaMod55.NFe.InfNFe.Ide.CUF == "14")
                uf = "RR";
            else if (notaFiscalEletronicaMod55.NFe.InfNFe.Ide.CUF == "15")
                uf = "PA";
            else if (notaFiscalEletronicaMod55.NFe.InfNFe.Ide.CUF == "16")
                uf = "AP";
            else if (notaFiscalEletronicaMod55.NFe.InfNFe.Ide.CUF == "17")
                uf = "TO";
            else if (notaFiscalEletronicaMod55.NFe.InfNFe.Ide.CUF == "21")
                uf = "MA";
            else if (notaFiscalEletronicaMod55.NFe.InfNFe.Ide.CUF == "22")
                uf = "PI";
            else if (notaFiscalEletronicaMod55.NFe.InfNFe.Ide.CUF == "23")
                uf = "CE";
            else if (notaFiscalEletronicaMod55.NFe.InfNFe.Ide.CUF == "24")
                uf = "RN";
            else if (notaFiscalEletronicaMod55.NFe.InfNFe.Ide.CUF == "25")
                uf = "PB";
            else if (notaFiscalEletronicaMod55.NFe.InfNFe.Ide.CUF == "26")
                uf = "PE";
            else if (notaFiscalEletronicaMod55.NFe.InfNFe.Ide.CUF == "27")
                uf = "AL";
            else if (notaFiscalEletronicaMod55.NFe.InfNFe.Ide.CUF == "28")
                uf = "SE";
            else if (notaFiscalEletronicaMod55.NFe.InfNFe.Ide.CUF == "29")
                uf = "BA";
            else if (notaFiscalEletronicaMod55.NFe.InfNFe.Ide.CUF == "31")
                uf = "MG";
            else if (notaFiscalEletronicaMod55.NFe.InfNFe.Ide.CUF == "32")
                uf = "ES";
            else if (notaFiscalEletronicaMod55.NFe.InfNFe.Ide.CUF == "33")
                uf = "RJ";
            else if (notaFiscalEletronicaMod55.NFe.InfNFe.Ide.CUF == "35")
                uf = "SP";
            else if (notaFiscalEletronicaMod55.NFe.InfNFe.Ide.CUF == "41")
                uf = "PR";
            else if (notaFiscalEletronicaMod55.NFe.InfNFe.Ide.CUF == "42")
                uf = "SC";
            else if (notaFiscalEletronicaMod55.NFe.InfNFe.Ide.CUF == "43")
                uf = "RS";
            else if (notaFiscalEletronicaMod55.NFe.InfNFe.Ide.CUF == "50")
                uf = "MS";
            else if (notaFiscalEletronicaMod55.NFe.InfNFe.Ide.CUF == "51")
                uf = "MT";
            else if (notaFiscalEletronicaMod55.NFe.InfNFe.Ide.CUF == "52")
                uf = "GO";
            else if (notaFiscalEletronicaMod55.NFe.InfNFe.Ide.CUF == "53")
                uf = "DF";
            return uf;
        }

        public static List<Produtos> CriateProdutos(List<Det> dets, DateTime? DhEmt)
        {
            List<Produtos> listaProdudos = new List<Produtos>();

            foreach (var item in dets)
            {
                var importoCsosn = item.Imposto.ICMS;

                var produtos = new Produtos();
                produtos.CodProduto = item.Prod.CProd;
                produtos.DescProduto = item.Prod.XProd;
                produtos.NcmProd = item.Prod.NCM;
                produtos.Ean = item.Prod.CEAN;
                produtos.Cfop = item.Prod.CFOP;
                produtos.UnidMedida = item.Prod.UCom;
                produtos.Quantidade = item.Prod.QCom;
                produtos.VlUnitario = item.Prod.VUnCom;
                produtos.VlProduto = item.Prod.VProd;
                produtos.NItemPedido = item.Prod.NItemPed;
                produtos.Origem = item.Prod.Orig;
                produtos.OutrasDespesas = item.Prod.VOutro;
                produtos.PedCompra = item.Prod.XPed;
                produtos.QtdTributaria = item.Prod.QTrib;
                produtos.UniMedTributado = item.Prod.UTrib;
                produtos.VlAproxTributos = item.Prod.VTotTrib;
                produtos.VlTlDesconto = item.Prod.VDesc;
                produtos.VlTlFrete = item.Prod.VFrete;
                produtos.VlTlSeguro = item.Prod.VFrete;
                produtos.NcmMono = false;
                produtos.IcmsSt = false;
                produtos.Beneficios = false;
                produtos.Isento = false;
                produtos.Imune = false;
                produtos.ExigSuspensa = false;
                produtos.LancamentoOficio = false;
                produtos.IsencaoReducao = false;
                produtos.IsencaoReducaoCestaBasica = false;
                produtos.AntEncTributacao = false;
                produtos.ExigSuspensaMono = false;
                produtos.SubTributariaMono = false;
                produtos.LancamentoOficioMono = false;
                produtos.VlUnitTributado = item.Prod.VUnTrib;
                produtos.DhEmt = DhEmt;
                if (importoCsosn.ICMSSN102 != null)
                    produtos.Csons = Convert.ToInt32(importoCsosn.ICMSSN102.CSOSN);
                else
                    produtos.Csons = 0;

                listaProdudos.Add(produtos);
            }

            return listaProdudos;
        }

        public static List<Impostos> CriateImpostos(List<Det> impostos)
        {
            List<Impostos> listaImpost = new List<Impostos>();

            foreach (var item in impostos)
            {
                var imp = new Impostos();
                if (item.Imposto.ICMS != null)
                {
                    var icms20 = item.Imposto.ICMS.ICMS20;
                    if (icms20 != null)
                    {
                        imp.VlIcms = icms20.VICMS;
                        imp.BCIcms = icms20.VBC;
                        imp.AliqIcms = icms20.PICMS;
                        imp.SituTributaria = icms20.CST;
                        imp.Origem = icms20.Orig;
                        imp.ModBcIcms = icms20.ModBC;
                        imp.PerRedBcIcmsSt = icms20.PRedBC;

                    }
                    var icmsSn102 = item.Imposto.ICMS.ICMSSN102;
                    if (icmsSn102 != null)
                    {
                        imp.Origem = icmsSn102.Orig;
                        imp.Csosn = icmsSn102.CSOSN;

                    }
                    var icms60 = item.Imposto.ICMS.ICMS60;
                    if (icms60 != null)
                    {
                        imp.Origem = icms60.Orig;
                        imp.SituTributaria = icms60.CST;
                        imp.VlBcIcmsSt = icms60.VICMSSTRet;
                    }
                }
                if (item.Imposto.IPI != null)
                {
                    var ipi = item.Imposto.IPI.IPITrib;
                    if (ipi != null)
                    {
                        imp.Ipi = ipi.VIPI;
                        imp.AliquotaIpi = ipi.PIPI;
                    }
                }

                listaImpost.Add(imp);
            }

            return listaImpost;
        }

        public static List<ProdutosFornecedor> CriateProdutosFornecedor(List<Det> dets)
        {
            List<ProdutosFornecedor> listaProdudos = new List<ProdutosFornecedor>();

            foreach (var item in dets)
            {
                var importoCsosn = item.Imposto.ICMS;

                var produtos = new ProdutosFornecedor();
                produtos.CodProduto = item.Prod.CProd;
                produtos.DescProduto = item.Prod.XProd;
                produtos.NcmProd = item.Prod.NCM;
                produtos.Ean = item.Prod.CEAN;
                produtos.Cfop = item.Prod.CFOP;
                produtos.UnidMedida = item.Prod.UCom;
                produtos.Quantidade = item.Prod.QCom;
                produtos.VlUnitario = item.Prod.VUnCom;
                produtos.VlProduto = item.Prod.VProd;
                produtos.NItemPedido = item.Prod.NItemPed;
                produtos.Origem = item.Prod.Orig;
                produtos.OutrasDespesas = item.Prod.VOutro;
                produtos.PedCompra = item.Prod.XPed;
                produtos.QtdTributaria = item.Prod.QTrib;
                produtos.UniMedTributado = item.Prod.UTrib;
                produtos.VlAproxTributos = item.Prod.VTotTrib;
                produtos.VlTlDesconto = item.Prod.VDesc;
                produtos.VlTlFrete = item.Prod.VFrete;
                produtos.VlTlSeguro = item.Prod.VFrete;
                produtos.VlUnitTributado = item.Prod.VUnTrib;
                if (importoCsosn.ICMSSN102 != null)
                    produtos.Csons = Convert.ToInt32(importoCsosn.ICMSSN102.CSOSN);
                else
                    produtos.Csons = 0;

                listaProdudos.Add(produtos);
            }

            return listaProdudos;
        }
        private static DateTime? ExtraindoData(string data)
        {
            var dataConvertida = new DateTime();
            if (!string.IsNullOrEmpty(data))
            {
                var result = DateTime.TryParse(data, out dataConvertida);
                if (result)
                    return dataConvertida;
                else
                    return null;
            }
            return null;
        }
    }
}
