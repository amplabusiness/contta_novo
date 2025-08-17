using System;
using System.Collections.Generic;
using System.Text;
using EmaloteContta.Models.NotaFiscalEletronicaMod55;
using EmaloteContta.Models.Speed;

namespace EmaloteContta.Adapter
{
    public class EntidadeXmlToEntidadeMongodb
    {
        public static CompanyInformation CretaEntidadeMongoEmpresaEmitente(Emit emitente)
        {
            if (emitente == null)
                throw new Exception("Emitente nao informado.");
            if (string.IsNullOrEmpty(emitente.CNPJ))
                throw new Exception("Cnpj do emitente nao informado.");

            return new CompanyInformation
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
            if (notaFiscalEletronicaMod55.NFe == null ||
                notaFiscalEletronicaMod55.NFe.InfNFe == null ||
                notaFiscalEletronicaMod55.NFe.InfNFe.Total == null
                )
                throw new Exception("Erro no processamento do xml");

            var nfe = new NFE
            {
                BaseCAlIcms = notaFiscalEletronicaMod55.NFe.InfNFe.Total.ICMSTot.VBC,
                BaseCalIcmsSt = notaFiscalEletronicaMod55.NFe.InfNFe.Total.ICMSTot.VBCST,
                CodBarra = notaFiscalEletronicaMod55.NFe.InfNFe.Id.Substring(3),
                DesOperacao = notaFiscalEletronicaMod55.NFe.InfNFe.Ide.NatOp,
                DhEmi = ExtraindoData(notaFiscalEletronicaMod55.NFe.InfNFe.Ide.DhEmi),
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
                VtTotalNfe = notaFiscalEletronicaMod55.NFe.InfNFe.Total.ICMSTot.VNF
            };
            ObterProdutosImposto(notaFiscalEletronicaMod55.NFe.InfNFe.Det, nfe);
            return nfe;
        }

        private static void ObterProdutosImposto(List<Models.NotaFiscalEletronicaMod55.Det> det, NFE nfe)
        {
            foreach (var item in det)
            {
                var imp = new Impostos();

                var importoCsosn = item.Imposto.ICMS;

                // colocar o id do produto e do imposto pra ser populado dentro da classe ou aqui.
                var produtos = new Produtos();
                //nfe.Produtos.Add(new Produtos
                //{
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
                //Tributos = item.Prod.UTrib,
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

                nfe.Produtos.Add(produtos);                

                var imposto = item.Imposto;
                if (imposto != null)
                {
                    imp.ProdutoId = produtos.Id.Value;
                    if (imposto.ICMS != null)
                    {
                        var icms20 = imposto.ICMS.ICMS20;
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
                        var icmsSn102 = imposto.ICMS.ICMSSN102;
                        if (icmsSn102 != null)
                        {
                            imp.Origem = icmsSn102.Orig;
                            imp.Csosn = icmsSn102.CSOSN;

                        }
                        var icms60 = imposto.ICMS.ICMS60;
                        if (icms60 != null)
                        {
                            imp.Origem = icms60.Orig;
                            imp.SituTributaria = icms60.CST;
                            imp.VlBcIcmsSt = icms60.VICMSSTRet;
                            //imp. = icms60.VBCSTRet;
                        }
                    }
                    if (imposto.IPI != null)
                    {
                        var ipi = imposto.IPI.IPITrib;
                        if (ipi != null)
                        {
                            imp.Ipi = ipi.VIPI;
                            imp.AliquotaIpi = ipi.PIPI;
                        }
                    }
                    nfe.Impostos.Add(imp);
                }
            }
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
