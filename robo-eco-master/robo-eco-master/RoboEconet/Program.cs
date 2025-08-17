using AngleSharp;
using Emalote;
using MongoDB.Bson;
using Newtonsoft.Json;
using RoboEconet.Helpers;
using RoboEconet.Infra;
using RoboEconet.Infra.Adapter;
using RoboEconet.Infra.Data.Interface;
using RoboEconet.LitDb;
using RoboEconet.Models;
using RoboEconet.Services;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace RoboEconet
{
    class Program
    {
        #region ST


        static ICMS ObterICMS(CookieContainer cookies, string token)
        {
            string retorno = string.Empty;
            string url = $"http://www.econeteditora.com.br/icms_st/index.php?acao=Abrir&i={HttpUtility.UrlEncode(token)}";

            HttpWebRequest primeiroRequest = (HttpWebRequest)WebRequest.Create(url);
            primeiroRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0; SLCC1)";
            primeiroRequest.Method = "GET";

            primeiroRequest.CookieContainer = cookies;

            HttpWebResponse primeiroResponse = (HttpWebResponse)primeiroRequest.GetResponse();

            Stream responseStream = primeiroResponse.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding(primeiroResponse.CharacterSet));

            retorno = reader.ReadToEnd()
                .CorrigeEncoding()
                .RemoveContentType();

            reader.Close();
            responseStream.Close();

            if (retorno.IndexOf("titulo_pagina") > -1)
            {
                var config = Configuration.Default;
                var context = BrowsingContext.New(config);

                var document = context.OpenAsync(req => req.Content(retorno)).Result;

                var tables = document.QuerySelectorAll("table");
                var icms = new ICMS();


                foreach (var table in tables)
                {
                    if (table.InnerHtml.Contains("Base Legal da Substituição Tributária") &&
                        table.InnerHtml.Contains("Segmento"))
                    {
                        icms.BaseLegal = table.QuerySelector("tbody > tr.textos > td:nth-child(1)")?.TextContent;
                        icms.Segmento = table.QuerySelector("tbody > tr.textos > td:nth-child(2)")?.TextContent;
                    }
                    else if (table.InnerHtml.Contains("NCM") &&
                        table.InnerHtml.Contains("Descrição") &&
                        !table.InnerHtml.Contains("Código Especificador da Substituição Tributária - CEST") &&
                        !table.InnerHtml.Contains("Alíquotas do IPI"))
                    {
                        icms.NCM = table.QuerySelector("tbody > tr.textos > td:nth-child(1)")?.TextContent;
                        icms.Descricao = table.QuerySelector("tbody > tr.textos > td:nth-child(2)")?.TextContent?.Trim();
                    }
                    else if (table.InnerHtml.Contains("MVA Original") &&
                        table.InnerHtml.Contains("MVA Ajustada 4%") &&
                        table.InnerHtml.Contains("MVA Ajustada 7%") &&
                        table.InnerHtml.Contains("MVA Ajustada 12%") &&
                        table.InnerHtml.Contains("Alíquota interna"))
                    {
                        icms.MVAOriginal = table.QuerySelector("tbody > tr.textos > td:nth-child(1)")?.TextContent?.ParaDecimais();
                        icms.MVAAjustada4 = table.QuerySelector("tbody > tr.textos > td:nth-child(2)")?.TextContent?.ParaDecimais();
                        icms.MVAAjustada7 = table.QuerySelector("tbody > tr.textos > td:nth-child(3)")?.TextContent?.ParaDecimais();
                        icms.MVAAjustada12 = table.QuerySelector("tbody > tr.textos > td:nth-child(4)")?.TextContent?.ParaDecimais();
                        icms.AliquotaInterna = table.QuerySelector("tbody > tr.textos > td:nth-child(5)")?.TextContent?.ParaDecimais();
                    }
                    else if (table.InnerHtml.Contains("Código Especificador da Substituição Tributária - CEST"))
                    {
                        icms.CESTNCM = table.QuerySelector("div:nth-child(4) > table > tbody > tr > td:nth-child(1)")?.TextContent;
                        icms.CESTDescricao = table.QuerySelector("div:nth-child(4) > table > tbody > tr > td:nth-child(2)")?.TextContent;
                        icms.CEST = table.QuerySelector("div:nth-child(4) > table > tbody > tr > td:nth-child(3)")?.TextContent;

                        icms.CESTAnexo = table.QuerySelector("div.div_table_cest > table > tbody > tr:nth-child(2) > td:nth-child(1)")?.TextContent;
                        icms.CESTSegmento = table.QuerySelector("div.div_table_cest > table > tbody > tr:nth-child(2) > td:nth-child(2)")?.TextContent;
                        icms.CESTItem = table.QuerySelector("div.div_table_cest > table > tbody > tr:nth-child(2) > td:nth-child(3)")?.TextContent;
                    }
                    else if (table.InnerHtml.Contains("CONVÊNIOS E PROTOCOLOS") && table.InnerHtml.Contains("SIGNATÁRIOS"))
                    {
                        icms.ConvenioProtocolo = table.QuerySelector("tbody > tr:nth-child(2) > td:nth-child(1)")?.TextContent;
                        icms.Signatarios = table.QuerySelector("tbody > tr:nth-child(2) > td:nth-child(2)")?.TextContent;

                        icms.Texto1 = table.QuerySelector("tbody > tr:nth-child(3) > td")?.TextContent;
                        icms.Texto2 = table.QuerySelector("tbody > tr:nth-child(4) > td")?.TextContent;
                    }
                    else if (table.InnerHtml.Contains("Alíquotas do IPI"))
                    {
                        var linhas = document.QuerySelectorAll("tr.textos");

                        var aliquotasIPI = new List<AliquotaIPI>();

                        foreach (var linha in linhas)
                        {
                            var colunas = linha.QuerySelectorAll("td");

                            if (colunas.Length == 3)
                            {
                                string ncm = colunas[0]?.TextContent;
                                string descricao = colunas[1]?.TextContent?.Trim();
                                decimal? aliquota = colunas[2]?.TextContent?.ParaDecimais();

                                icms.AliquotasIPI.Add(new AliquotaIPI
                                {
                                    NCM = ncm,
                                    Descricao = descricao,
                                    Aliquota = aliquota
                                });
                            }
                        }
                    }
                }


                return icms;
            }

            return null;
        }

        static List<Mercadoria> ObterMercadorias(string ufPesquisa)
        {
            CookieContainer cookies = new CookieContainer();
            NameValueCollection postData = new NameValueCollection();

            string url = "http://www.econeteditora.com.br/user/ver_log.asp";
            string urlEnvio = string.Empty;
            string retorno = string.Empty;

            postData.Add("Log", "onu17027");
            postData.Add("Sen", "9979Sc3");

            HttpWebRequest primeiroRequest = (HttpWebRequest)WebRequest.Create(url);
            primeiroRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0; SLCC1)";
            primeiroRequest.Method = "POST";
            primeiroRequest.ContentType = "application/x-www-form-urlencoded";

            primeiroRequest.CookieContainer = cookies;

            string strPost = Util.FormataPost(postData);
            byte[] encodedData = Encoding.ASCII.GetBytes(strPost);
            primeiroRequest.ContentLength = encodedData.Length;

            Stream requestStream = primeiroRequest.GetRequestStream();
            requestStream.Write(encodedData, 0, encodedData.Length);

            urlEnvio = string.Format("{0}?{1}", url, strPost);

            HttpWebResponse primeiroResponse = (HttpWebResponse)primeiroRequest.GetResponse();

            Stream responseStream = primeiroResponse.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding(primeiroResponse.CharacterSet));
            retorno = reader.ReadToEnd();
            reader.Close();
            responseStream.Close();

            if (!string.IsNullOrEmpty(retorno))
            {
                url = "http://www.econeteditora.com.br/index.asp?url=inicial.php";

                HttpWebRequest segundoRequest = (HttpWebRequest)WebRequest.Create(url);
                segundoRequest.AllowAutoRedirect = true;
                segundoRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0; SLCC1)";
                segundoRequest.Method = "GET";

                segundoRequest.CookieContainer = cookies;

                urlEnvio = string.Format("{0}", url);

                HttpWebResponse segundoResponse = (HttpWebResponse)segundoRequest.GetResponse();

                responseStream = segundoResponse.GetResponseStream();
                reader = new StreamReader(responseStream, Encoding.GetEncoding(segundoResponse.CharacterSet));
                retorno = reader.ReadToEnd();
                reader.Close();
                responseStream.Close();

                if (retorno.IndexOf("Login Efetuado com Sucesso") > -1)
                {
                    string uf = ufPesquisa.ToUpper();
                    url = $"http://www.econeteditora.com.br/icms_st/index.php?form%5Buf%5D={uf}&form%5Bncm%5D=&form%5Bcest%5D=&form%5Bpalavra%5D=%25%25%25&acao=Buscar";

                    HttpWebRequest terceiroRequest = (HttpWebRequest)WebRequest.Create(url);
                    terceiroRequest.AllowAutoRedirect = true;
                    terceiroRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0; SLCC1)";
                    terceiroRequest.Method = "GET";

                    terceiroRequest.CookieContainer = cookies;

                    urlEnvio = string.Format("{0}", url);

                    HttpWebResponse terceiroResponse = (HttpWebResponse)terceiroRequest.GetResponse();

                    responseStream = terceiroResponse.GetResponseStream();
                    reader = new StreamReader(responseStream, Encoding.GetEncoding(terceiroResponse.CharacterSet));
                    var enc = terceiroResponse.ContentEncoding;

                    retorno = reader.ReadToEnd()
                        .CorrigeEncoding()
                        .RemoveContentType();

                    reader.Close();
                    responseStream.Close();

                    if (retorno.IndexOf("RESULTADOS DA BUSCA") > -1)
                    {
                        var mercadorias = new List<Mercadoria>();

                        var config = Configuration.Default;
                        var context = BrowsingContext.New(config);

                        var document = context.OpenAsync(req => req.Content(retorno)).Result;

                        var linhas = document.QuerySelectorAll("tr.textos");

                        foreach (var linha in linhas)
                        {
                            var colunas = linha.QuerySelectorAll("td");

                            if (colunas.Length == 3)
                            {
                                string ncm = colunas[0].TextContent;
                                string descricao = colunas[1].TextContent?.Trim();
                                string token = Util.PesquisaHtml(colunas[2].InnerHtml, "name=\"i\" value=\"");

                                mercadorias.Add(new Mercadoria
                                {
                                    Descricao = descricao,
                                    NCM = ncm,
                                    UF = ufPesquisa.ToUpper(),
                                    Token = token,
                                    ICMS = ObterICMS(cookies, token)
                                });
                            }
                        }

                        return mercadorias;
                    }
                }
            }

            return null;
        }

        #endregion

        #region Aliquotas e Benefícios Fiscais

        static string ObterDetalhes(CookieContainer cookies, string uf, string id)
        {
            var postData = new NameValueCollection
            {
                { "form[acao]", "detalhes" },
                { "form[uf]", uf },
                { "form[id]", id }
            };

            string retorno = string.Empty;
            string url = $"http://www.econeteditora.com.br/links_pagina_inicial/aliq_icms/aliquotas/index.php";

            HttpWebRequest primeiroRequest = (HttpWebRequest)WebRequest.Create(url);
            primeiroRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0; SLCC1)";
            primeiroRequest.Method = "POST";
            primeiroRequest.ContentType = "application/x-www-form-urlencoded";

            primeiroRequest.CookieContainer = cookies;

            string strPost = Util.FormataPost(postData);
            byte[] encodedData = Encoding.ASCII.GetBytes(strPost);
            primeiroRequest.ContentLength = encodedData.Length;

            Stream requestStream = primeiroRequest.GetRequestStream();
            requestStream.Write(encodedData, 0, encodedData.Length);

            HttpWebResponse primeiroResponse = (HttpWebResponse)primeiroRequest.GetResponse();

            Stream responseStream = primeiroResponse.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding(primeiroResponse.CharacterSet));

            retorno = reader.ReadToEnd()
                .CorrigeEncoding()
                .RemoveContentType();

            reader.Close();
            responseStream.Close();

            if (retorno.IndexOf("Base Legal") > -1)
            {
                var config = Configuration.Default;
                var context = BrowsingContext.New(config);

                var document = context.OpenAsync(req => req.Content(retorno)).Result;
                retorno = document.QuerySelector("body > div > p > a:nth-child(3)")?.TextContent;
            }

            return retorno;
        }

        static List<AliquotaBeneficioFiscal> ObterAliquotasBeneficiosFiscais(string uf)
        {
            uf = uf.ToUpper();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            CookieContainer cookies = new CookieContainer();
            NameValueCollection postData = new NameValueCollection();

            string url = "http://www.econeteditora.com.br/user/ver_log.asp";
            string urlEnvio = string.Empty;
            string retorno = string.Empty;

            postData.Add("Log", "onu17027");
            postData.Add("Sen", "9979Sc3");

            HttpWebRequest primeiroRequest = (HttpWebRequest)WebRequest.Create(url);
            primeiroRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0; SLCC1)";
            primeiroRequest.Method = "POST";
            primeiroRequest.ContentType = "application/x-www-form-urlencoded";

            primeiroRequest.CookieContainer = cookies;

            string strPost = Util.FormataPost(postData);
            byte[] encodedData = Encoding.ASCII.GetBytes(strPost);
            primeiroRequest.ContentLength = encodedData.Length;

            Stream requestStream = primeiroRequest.GetRequestStream();
            requestStream.Write(encodedData, 0, encodedData.Length);

            urlEnvio = string.Format("{0}?{1}", url, strPost);

            HttpWebResponse primeiroResponse = (HttpWebResponse)primeiroRequest.GetResponse();

            Stream responseStream = primeiroResponse.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding(primeiroResponse.CharacterSet));
            retorno = reader.ReadToEnd();
            reader.Close();
            responseStream.Close();

            if (!string.IsNullOrEmpty(retorno))
            {
                url = "http://www.econeteditora.com.br/index.asp?url=inicial.php";

                HttpWebRequest segundoRequest = (HttpWebRequest)WebRequest.Create(url);
                segundoRequest.AllowAutoRedirect = true;
                segundoRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0; SLCC1)";
                segundoRequest.Method = "GET";

                segundoRequest.CookieContainer = cookies;

                urlEnvio = string.Format("{0}", url);

                HttpWebResponse segundoResponse = (HttpWebResponse)segundoRequest.GetResponse();

                responseStream = segundoResponse.GetResponseStream();
                reader = new StreamReader(responseStream, Encoding.GetEncoding(segundoResponse.CharacterSet));
                retorno = reader.ReadToEnd();
                reader.Close();
                responseStream.Close();

                if (retorno.IndexOf("Login Efetuado com Sucesso") > -1)
                {
                    url = $"http://www.econeteditora.com.br/links_pagina_inicial/aliq_icms/index.php?form[acao]=estado&form[uf]={uf}&form[painel]=0";

                    HttpWebRequest terceiroRequest = (HttpWebRequest)WebRequest.Create(url);
                    terceiroRequest.AllowAutoRedirect = true;
                    terceiroRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0; SLCC1)";
                    terceiroRequest.Method = "GET";

                    terceiroRequest.CookieContainer = cookies;

                    urlEnvio = string.Format("{0}", url);

                    HttpWebResponse terceiroResponse = (HttpWebResponse)terceiroRequest.GetResponse();

                    responseStream = terceiroResponse.GetResponseStream();
                    reader = new StreamReader(responseStream, Encoding.GetEncoding(terceiroResponse.CharacterSet));
                    var enc = terceiroResponse.ContentEncoding;

                    retorno = reader.ReadToEnd()
                        .CorrigeEncoding()
                        .RemoveContentType();

                    reader.Close();
                    responseStream.Close();

                    if (retorno.IndexOf("Al&iacute;quotas Internas e Benef&iacute;cios Fiscais") > -1)
                    {
                        var aliquotasBeneficiosFiscais = new List<AliquotaBeneficioFiscal>();

                        var config = Configuration.Default;
                        var context = BrowsingContext.New(config);

                        var document = context.OpenAsync(req => req.Content(retorno)).Result;

                        var linhas = document.QuerySelectorAll("tr");

                        foreach (var linha in linhas)
                        {
                            var colunas = linha.QuerySelectorAll("td");

                            if (colunas.Length == 8)
                            {
                                var aliquota = colunas[0].TextContent?.ParaDecimais();
                                var protege = colunas[1].TextContent?.Trim()?.ParaDecimais();
                                var aliquotaEfetiva = colunas[2].TextContent?.ParaDecimais();
                                var ncm = colunas[3].TextContent?.Trim();
                                var descricao = colunas[4].TextContent?.Trim()?.Replace("+ Detalhes", string.Empty);

                                var id = Util.PesquisaHtml(colunas[4].InnerHtml, $"javascript:detalhes_aliquota('{uf}',", ")");
                                var detalhes = ObterDetalhes(cookies, uf, id);

                                aliquotasBeneficiosFiscais.Add(new AliquotaBeneficioFiscal
                                {
                                    UF = uf,
                                    Aliquota = aliquota,
                                    Protege = protege,
                                    AliquotaEfetiva = aliquotaEfetiva,
                                    NCM = ncm,
                                    Descricao = $"{descricao} {detalhes}"
                                });
                            }
                        }

                        return aliquotasBeneficiosFiscais;
                    }
                }
            }

            return null;
        }

        #endregion

        #region PIS E Confins

        static List<NCM> ObterNCMS(string retorno)
        {
            var config = Configuration.Default;
            var context = BrowsingContext.New(config);

            var document = context.OpenAsync(req => req.Content(retorno)).Result;

            var ncms = new List<NCM>();
            var linhas = document.QuerySelectorAll("#abas_internas > div > div:nth-child(1) > div.fixa > table > tbody > tr");

            foreach (var linha in linhas)
            {
                var colunas = linha.QuerySelectorAll("td");

                if (colunas.Length == 2)
                {
                    if (colunas[0]?.TextContent != "NCM")
                    {
                        ncms.Add(new NCM
                        {
                            Codigo = colunas[0]?.TextContent,
                            Descricao = colunas[1]?.TextContent
                        });
                    }
                }
            }

            return ncms;
        }

        static List<AliquotaPisConfins> ObterAliquotaPisConfins(string retorno)
        {
            var config = Configuration.Default;
            var context = BrowsingContext.New(config);

            var document = context.OpenAsync(req => req.Content(retorno)).Result;

            var aliquotas = new List<AliquotaPisConfins>();
            var linhas = document.QuerySelectorAll("#abas_internas > div > div:nth-child(1) > div.fixa2 > table > tbody > tr");

            foreach (var linha in linhas)
            {
                var colunas = linha.QuerySelectorAll("td");

                if (colunas.Length == 4)
                {
                    if (colunas[0]?.TextContent != "Regime de Tributação")
                    {
                        aliquotas.Add(new AliquotaPisConfins
                        {
                            RegimeTributacao = colunas[0]?.TextContent,
                            PIS = colunas[1]?.TextContent,
                            CONFINS = colunas[2]?.TextContent,
                            DispositivoLegal = colunas[3]?.TextContent,
                        });
                    }
                }
            }

            return aliquotas;
        }

        static List<Observacao> ObterObservacoes(string retorno)
        {
            var config = Configuration.Default;
            var context = BrowsingContext.New(config);

            var document = context.OpenAsync(req => req.Content(retorno)).Result;

            var observacoes = new List<Observacao>();

            var linhas = document.QuerySelectorAll("#abas_internas > div > div:nth-child(1) > div.relativa > table:nth-child(1) > tbody > tr");

            foreach (var linha in linhas)
            {
                var colunas = linha.QuerySelectorAll("td");

                if (colunas.Length == 1)
                {
                    if (colunas[0]?.TextContent != "Observações")
                    {
                        observacoes.Add(new Observacao
                        {
                            Descricao = colunas[0]?.TextContent
                        });
                    }
                }

            }

            return observacoes;
        }

        static List<CST> ObterCSTS(string retorno)
        {
            var config = Configuration.Default;
            var context = BrowsingContext.New(config);

            var document = context.OpenAsync(req => req.Content(retorno)).Result;

            var csts = new List<CST>();

            var linhas = document.QuerySelectorAll("#abas_internas > div > div:nth-child(1) > div.relativa > table:nth-child(3) > tbody > tr");

            foreach (var linha in linhas)
            {
                var colunas = linha.QuerySelectorAll("td");

                if (colunas.Length == 3)
                {
                    csts.Add(new CST
                    {
                        Nome = colunas[0]?.TextContent,
                        Codigo = colunas[1]?.TextContent,
                        Descricao = colunas[2]?.TextContent
                    });
                }
            }

            return csts;
        }

        static List<EFD> ObterEFDS(string retorno)
        {
            var config = Configuration.Default;
            var context = BrowsingContext.New(config);

            var document = context.OpenAsync(req => req.Content(retorno)).Result;

            var efds = new List<EFD>();

            var linhas = document.QuerySelectorAll("#abas_internas > div > div:nth-child(1) > div.relativa > table:nth-child(5) > tbody > tr");

            foreach (var linha in linhas)
            {
                var colunas = linha.QuerySelectorAll("td");

                if (colunas.Length == 1)
                {
                    if (colunas[0]?.TextContent != "EFD-Contribuições" && colunas[0]?.TextContent != "Escrituração na saída da mercadoria/produto")
                    {
                        efds.Add(new EFD
                        {
                            Descricao = colunas[0]?.TextContent
                        });
                    }
                }
            }

            return efds;
        }

        static List<PisConfins> ObterPisEConfins(string ncmPesquisa)
        {
            var listaDePisConfins = new List<PisConfins>();

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            CookieContainer cookies = new CookieContainer();
            NameValueCollection postData = new NameValueCollection();

            string url = "http://www.econeteditora.com.br/user/ver_log.asp";
            string urlEnvio = string.Empty;
            string retorno = string.Empty;

            postData.Add("Log", "onu17027");
            postData.Add("Sen", "9979Sc3");

            HttpWebRequest primeiroRequest = (HttpWebRequest)WebRequest.Create(url);
            primeiroRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0; SLCC1)";
            primeiroRequest.Method = "POST";
            primeiroRequest.ContentType = "application/x-www-form-urlencoded";

            primeiroRequest.CookieContainer = cookies;

            string strPost = Util.FormataPost(postData);
            byte[] encodedData = Encoding.ASCII.GetBytes(strPost);
            primeiroRequest.ContentLength = encodedData.Length;

            Stream requestStream = primeiroRequest.GetRequestStream();
            requestStream.Write(encodedData, 0, encodedData.Length);

            urlEnvio = string.Format("{0}?{1}", url, strPost);

            HttpWebResponse primeiroResponse = (HttpWebResponse)primeiroRequest.GetResponse();

            Stream responseStream = primeiroResponse.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding(primeiroResponse.CharacterSet));
            retorno = reader.ReadToEnd();
            reader.Close();
            responseStream.Close();

            if (!string.IsNullOrEmpty(retorno))
            {
                url = "http://www.econeteditora.com.br/index.asp?url=inicial.php";

                HttpWebRequest segundoRequest = (HttpWebRequest)WebRequest.Create(url);
                segundoRequest.AllowAutoRedirect = true;
                segundoRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0; SLCC1)";
                segundoRequest.Method = "GET";

                segundoRequest.CookieContainer = cookies;

                urlEnvio = string.Format("{0}", url);

                HttpWebResponse segundoResponse = (HttpWebResponse)segundoRequest.GetResponse();

                responseStream = segundoResponse.GetResponseStream();
                reader = new StreamReader(responseStream, Encoding.GetEncoding(segundoResponse.CharacterSet));
                retorno = reader.ReadToEnd();
                reader.Close();
                responseStream.Close();

                if (retorno.IndexOf("Login Efetuado com Sucesso") > -1)
                {
                    var token = Util.ObterTokenBuscaPisConfins(ncmPesquisa);
                    url = $"http://www.econeteditora.com.br/pis_cofins/pis_cofins.php?i={token}";

                    HttpWebRequest terceiroRequest = (HttpWebRequest)WebRequest.Create(url);
                    terceiroRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0; SLCC1)";
                    terceiroRequest.Method = "GET";

                    terceiroRequest.CookieContainer = cookies;

                    urlEnvio = string.Format("{0}", url);

                    HttpWebResponse terceiroResponse = (HttpWebResponse)terceiroRequest.GetResponse();

                    responseStream = terceiroResponse.GetResponseStream();
                    reader = new StreamReader(responseStream, Encoding.GetEncoding(terceiroResponse.CharacterSet));
                    retorno = reader.ReadToEnd()
                        .CorrigeEncoding()
                        .RemoveContentType();

                    reader.Close();
                    responseStream.Close();

                    var time = Util.PesquisaHtml2(retorno, "name=\"form[time]\" value=\"", "\" />");
                    bool monofasico = retorno.IndexOf("Monofásico") > -1;

                    var config = Configuration.Default;
                    var context = BrowsingContext.New(config);

                    var document = context.OpenAsync(req => req.Content(retorno)).Result;

                    var linhas = document.QuerySelectorAll("tr");

                    foreach (var linha in linhas)
                    {
                        if (linha.InnerHtml.IndexOf("form[ncm]") > -1)
                        {
                            var colunas = linha.QuerySelectorAll("td");

                            if (colunas.Length == 3)
                            {
                                var key = Util.PesquisaHtml2(colunas[0].InnerHtml, "value=\"", "\" required=\"\">");
                                var ncm = colunas[1].TextContent;
                                var descricao = colunas[2].TextContent;

                                url = "http://www.econeteditora.com.br/pis_cofins/pis_cofins.php";

                                postData.Clear();
                                postData.Add("form[ncm]", key);
                                postData.Add("form[acao]", "abrir");
                                postData.Add("form[time]", time);

                                HttpWebRequest quartoRequest = (HttpWebRequest)WebRequest.Create(url);
                                quartoRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0; SLCC1)";
                                quartoRequest.Method = "POST";
                                quartoRequest.ContentType = "application/x-www-form-urlencoded";

                                quartoRequest.CookieContainer = cookies;

                                strPost = Util.FormataPost(postData);
                                encodedData = Encoding.ASCII.GetBytes(strPost);
                                quartoRequest.ContentLength = encodedData.Length;

                                requestStream = quartoRequest.GetRequestStream();
                                requestStream.Write(encodedData, 0, encodedData.Length);

                                urlEnvio = string.Format("{0}?{1}", url, strPost);

                                HttpWebResponse quartoResponse = (HttpWebResponse)quartoRequest.GetResponse();

                                responseStream = quartoResponse.GetResponseStream();
                                reader = new StreamReader(responseStream, Encoding.GetEncoding(quartoResponse.CharacterSet));

                                retorno = reader.ReadToEnd()
                                                .CorrigeEncoding()
                                                .RemoveContentType();

                                reader.Close();
                                responseStream.Close();

                                var pisConfins = new PisConfins
                                {
                                    NCM = ncm,
                                    Descricao = descricao,
                                    NCMS = ObterNCMS(retorno),
                                    Aliquotas = ObterAliquotaPisConfins(retorno),
                                    Observacoes = ObterObservacoes(retorno),
                                    CSTS = ObterCSTS(retorno),
                                    EFDS = ObterEFDS(retorno),
                                    Monofasico = monofasico,
                                    Id = Guid.NewGuid()

                                };

                                listaDePisConfins.Add(pisConfins);
                            }
                        }
                    }
                }
            }

            return listaDePisConfins;
        }

        #endregion

        static void Main(string[] args)
        {
            new Nucleo().Start();
            List<PisConfins> listPisConfins = new List<PisConfins>();
            Log.SetAppName(ToolsEmalote.APP_NAME);
            var qtd = 0;

            //TODO: Colocar no HangFire pesquisando por estado e colocando 2 minutos de tempo entre um e outro.

            // var mercadorias = ObterMercadorias("GO");
            // var json = JsonConvert.SerializeObject(mercadorias);
            // var pisMercadoriaDto = JsonConvert.DeserializeObject<Mercadoria[]>(json);

            //var aliquotas = ObterAliquotasBeneficiosFiscais("GO");
            //var json2 = JsonConvert.SerializeObject(aliquotas);
            //var pisAliquataDto = JsonConvert.DeserializeObject<AliquotaBeneficioFiscal[]>(json2);13234578

            qtd = GetPisConfins(listPisConfins, qtd);
        }

        private static int GetPisConfins(List<PisConfins> listPisConfins, int qtd)
        {
            //string[] lines = System.IO.File.ReadAllLines(@"C:\contta\robo-econet\RoboEconet\Files\ListaNCMGeral.txt");
            string[] lines = System.IO.File.ReadAllLines(@"C:\robo-econet\RoboEconet\Files\ListaNCMGeral.txt");

            foreach (var item in lines)
            {
                qtd = qtd + 1;
                String ncmModified;

                if (item.Length <= 4)
                {
                    if (item.Length == 3)
                        ncmModified = item.Insert(0, "0");
                    else
                    {
                        ncmModified = item;
                    }

                    var getpisConfins = ObterPisEConfins(ncmModified);
                    var pisConfins = JsonConvert.SerializeObject(getpisConfins);

                    var pisConfinDto = JsonConvert.DeserializeObject<PisConfins[]>(pisConfins);

                    EntidadePisConfinsToEntidadeMongodb entidade = new EntidadePisConfinsToEntidadeMongodb();

                    for (int i = 0; i < pisConfinDto.Length; i++)
                    {
                        var dataPisConfins = pisConfinDto[i];

                        if (dataPisConfins.NCM != string.Empty)
                        {
                            if (item.Length <= 4)
                            {
                                dataPisConfins.NcmPai = item;
                                listPisConfins.Add(dataPisConfins);
                            }
                        }
                    }                   

                    var adaptadorPisConfins = entidade.CretaEntidadeMongoPisConfins(listPisConfins, ncmModified);
                    Console.WriteLine($"Quantidade integradas={qtd}");
                    var result = InsertPisConfins(adaptadorPisConfins);
                }
            }

            return qtd;
        }

        static async Task<bool> InsertPisConfins(List<PisConfinsDto> pisConfins)
        {
            PisConfinsService _pisConfinsService = new PisConfinsService();

            try
            {
                if (pisConfins.Count > 0)
                {
                    var result = await _pisConfinsService.Create(pisConfins);
                    if (!result)
                    {
                        Console.WriteLine($"\n\nNcm não foi gravado em nossas base={pisConfins.Find(c => c.NCM != null).NCM}\n\n");
                    }

                    if (result)
                        return true;
                    else
                        return false;

                }

                return false;

            }
            catch (System.Exception ex)
            {
                return false;
            }

        }
    }
}
