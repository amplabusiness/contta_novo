using Contta.Common;
using Contta.SpedFiscal;
using Microsoft.VisualBasic;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using System;

namespace Corporate.Contta.Schedule.SpedContta.MapBlocos
{
    public class MapBlocoD
    {
        public void GetBlocoD()
        {
            IMongoClient mongoClient = new MongoClient("mongodb://localhost");
            IMongoDatabase database = mongoClient.GetDatabase("Bloco0");
            IMongoCollection<BlocoD.RegistroD001> tabela01 = database.GetCollection<BlocoD.RegistroD001>("registroD001");
            IMongoCollection<BlocoD.RegistroD100> tabela02 = database.GetCollection<BlocoD.RegistroD100>("registroD100");
            IMongoCollection<BlocoD.RegistroD110> tabela03 = database.GetCollection<BlocoD.RegistroD110>("registroD110");
            IMongoCollection<BlocoD.RegistroD120> tabela04 = database.GetCollection<BlocoD.RegistroD120>("registroD120");
            IMongoCollection<BlocoD.RegistroD130> tabela05 = database.GetCollection<BlocoD.RegistroD130>("registroD130");
            IMongoCollection<BlocoD.RegistroD140> tabela06 = database.GetCollection<BlocoD.RegistroD140>("registroD140");
            IMongoCollection<BlocoD.RegistroD150> tabela07 = database.GetCollection<BlocoD.RegistroD150>("registroD150");
            IMongoCollection<BlocoD.RegistroD160> tabela08 = database.GetCollection<BlocoD.RegistroD160>("registroD160");
            IMongoCollection<BlocoD.RegistroD190> tabela09 = database.GetCollection<BlocoD.RegistroD190>("registroD190");
            IMongoCollection<BlocoD.RegistroD990> tabela10 = database.GetCollection<BlocoD.RegistroD990>("registroD990");

            BlocoD.RegistroD001 registroD001 = new BlocoD.RegistroD001();
            BlocoD.RegistroD100 registroD100 = new BlocoD.RegistroD100();
            BlocoD.RegistroD110 registroD110 = new BlocoD.RegistroD110();//ToDo melhorar valor das entidades
            BlocoD.RegistroD120 registroD120 = new BlocoD.RegistroD120();//ToDo melhorar valor das entidades
            BlocoD.RegistroD130 registroD130 = new BlocoD.RegistroD130();//ToDo melhorar valor das entidades
            BlocoD.RegistroD140 registroD140 = new BlocoD.RegistroD140();//ToDo melhorar valor das entidades
            BlocoD.RegistroD150 registroD150 = new BlocoD.RegistroD150();//ToDo melhorar valor das entidades
            BlocoD.RegistroD160 registroD160 = new BlocoD.RegistroD160();//ToDo melhorar valor das entidades
            BlocoD.RegistroD190 registroD190 = new BlocoD.RegistroD190();
            BlocoD.RegistroD990 registroD990 = new BlocoD.RegistroD990();


            registroD001.IndMov = IndMovimento.BlocoComDados;

            //Este registro deve ser apresentado por todos os contribuintes adquirentes ou prestadores dos serviços que utilizem os documentos especificados.
            //O campo CHV_CTE passa a ser de preenchimento obrigatório a partir de abril de 2012 em todas as situações, exceto para COD_SIT = 5(numeração inutilizada).
            //IMPORTANTE: para documentos de entrada, os campos de valor de imposto / contribuição, base de cálculo e alíquota só devem ser informados se o adquirente tiver direito à apropriação do crédito(enfoque do declarante).
            registroD100.IndOper = 0;
            registroD100.IndEmit = 0;
            registroD100.CodPart = "";
            registroD100.CodMod = "";
            registroD100.CodSit = 0;
            registroD100.Ser = "";
            registroD100.Sub = "";
            registroD100.NumDoc = "";
            registroD100.ChvCte = "";
            registroD100.DtDoc = DateTime.Now;
            registroD100.DtAP = DateTime.Now;
            registroD100.TpCte = 0;
            registroD100.ChvCteRef = "";
            registroD100.VlDoc = 0;
            registroD100.VlDesc = 0;
            registroD100.IndFrt = 0;
            registroD100.VlServ = 0;
            registroD100.VlBcIcms = 0;
            registroD100.VlIcms = 0;
            registroD100.VlNt = 0;
            registroD100.CodInf = "";
            registroD100.CodCta = "";

            //Este Registro deve ser apresentado para informar os itens das Notas Fiscais de Serviços de Transporte (Código 07) fornecidas no Registro D100
            registroD110.NumItem = 0;
            registroD110.CodigoItem = 0;
            registroD110.ValorServico = 0;
            registroD110.OutroValores = 0;

            //Este Registro deve ser apresentado para informar o complemento das Notas Fiscais de Serviços de Transporte (Código 07), com Municípios de origem e destino do transporte.
            registroD120.CodigoMunicipio = 0;
            registroD120.CodigoMunicipioDes = 0;
            registroD120.IndentificacaoVeiculo = 0;
            registroD120.UFVeiculo = 0;

            //Este Registro tem por objetivo informar o complemento do Conhecimento de Transporte Rodoviário de Cargas (Código 08) e Conhecimento de Transporte de Cargas Avulso (Código 8B).
            registroD130.TipoNavegação = 0;
            registroD130.NumViagem = 0;
            registroD130.ValorDespPortuaria = 0;
            registroD130.ValorCargaDescarga = 0;
            registroD130.ValorAdicionalFrete = 0;

            //O Registro D140 tem por objetivo informar o complemento do Conhecimento de Transporte Aquaviário de Cargas (Código 09).
            registroD140.CodigoParticipante = 0;
            registroD140.CodigoParticipanteResdespacho = 0;
            registroD140.IndicadorTipoFrete = 0;
            registroD140.CodigoMuniOrigem = 0;
            registroD140.CodigoMuniDestino = 0;
            registroD140.SomaValorServ = 0;
            registroD140.SomaValorDesp = 0;
            registroD140.SomaValorPed = 0;
            registroD140.TotalFrete = 0;
            registroD140.UfVeiculo = 0;

            //O Registro D150 tem por objetivo informar o complemento do Conhecimento de Transporte Aéreo de Cargas (Código 10).
            registroD150.IndTipoTarifa = 0;
            registroD150.PesoTaxado = 0;
            registroD150.ValorTaxaTerrestre = 0;
            registroD150.ValorTaxaResp = 0;
            registroD150.ValorTaxaAD = 0;

            //No registro D160 devem ser apresentados dados sobre o transporte da carga, objeto dos conhecimentos de transporte aqui especificados.
            registroD160.Despacho = 0;
            registroD160.CpfCnpjRemetente = 0;
            registroD160.InscricaoEstaRemet = 0;
            registroD160.CodMunicipioOrigem = 0;
            registroD160.CnfCnpjDest = 0;
            registroD160.InscrDestinatario = 0;
            registroD160.CodMunDestino = 0;


            //Este Registro tem por objetivo informar as Notas Fiscais de Serviço de Transporte (Código 07) e demais documentos elencados no título deste registro e especificados no Registro D100,
            //totalizados pelo agrupamento das combinações dos valores de CST, CFOP e Alíquota dos itens de cada documento.
            registroD190.CstIcms = 0;
            registroD190.Cfop = 0;
            registroD190.AliqIcms = 0;
            registroD190.VlOpr = 0;
            registroD190.VlBcIcms = 0;
            registroD190.VlIcms = 0;
            registroD190.VlRedBc = 0 ;
            registroD190.CodObs = 0;

            registroD990.QtdLinD = 0;
        }
    }
}
