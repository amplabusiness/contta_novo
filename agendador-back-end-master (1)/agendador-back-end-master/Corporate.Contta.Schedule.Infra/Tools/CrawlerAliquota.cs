using AngleSharp;
using ConsultaJUridica.Extensions;
using Corporate.Contta.Schedule.Domain.Entities.NfeAgg;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Infra.Tools
{
    public class CrawlerAliquota
    {        
        public async Task<List<TabelaExterna>> GetAnexo(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create($"{url}");
            List<TabelaExterna> tabelaExternas = new List<TabelaExterna>();

            try
            {
                request.Method = "GET";
                request.UserAgent = Proxies.UserAgent();
                request.ContentType = "text/html;charset=UTF-8";
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                var response = (HttpWebResponse)request.GetResponse();

                var stream = response.GetResponseStream();
                var streamReader = new StreamReader(stream, Encoding.GetEncoding(response.CharacterSet));

                var html = streamReader.ReadToEnd();

                response.Dispose();
                stream.Dispose();
                streamReader.Dispose();

                var anexo = "";

                var config = Configuration.Default.WithDefaultLoader();
                var context = BrowsingContext.New(config);

                var document = await context.OpenAsync(req => req.Content(html));

                var resultSelector = "body";
                var result = document.QuerySelector(resultSelector);

                var resultTabelaAnexo = result.QuerySelector("body > table:nth-child(61)");//body > table:nth-child(61)

                var tabelaRow01 = resultTabelaAnexo.QuerySelector("body > table:nth-child(61) > tbody > tr:nth-child(2)");

                var resultadoReparticao = result.QuerySelector("body > table:nth-child(63)");

                Anexo01(tabelaExternas, resultTabelaAnexo, tabelaRow01, resultadoReparticao);

            }
            catch (Exception)
            {
                var cont = 0;     
                cont = cont + 1;

                if (cont < 3)
                {
                    CrawlerAliquota loop = new CrawlerAliquota();
                    var valores = loop.GetAnexo(url);
                }
                else
                {
                    throw;
                }
            }

            return tabelaExternas;
        }

        private static void Anexo01(List<TabelaExterna> tabelaExternas, AngleSharp.Dom.IElement resultTabelaAnexo, AngleSharp.Dom.IElement tabelaRow01, AngleSharp.Dom.IElement resultadoReparticao)
        {
            var faixa01Anexo01 = new TabelaExterna()
            {
                Id = Guid.NewGuid(),
                Faixa = Convert.ToInt32(tabelaRow01.QuerySelector("body > table:nth-child(61) > tbody > tr:nth-child(2) > td:nth-child(1)").TextContent.Replace("\n\t\t\n\t\t1a\n\t\tFaixa", "1")),
                ValorInicial = "0",
                ValorFinal = String.Format("{0:#.#,##}", tabelaRow01.QuerySelector("body > table:nth-child(61) > tbody > tr:nth-child(2) > td:nth-child(2)").TextContent.Replace("\n\t\t\n\t\tAté", "")),
                Aliquota = Convert.ToDecimal(tabelaRow01.QuerySelector("body > table:nth-child(61) > tbody > tr:nth-child(2) > td:nth-child(3)").TextContent.Replace("\n\t\t", "").Replace("%", "")),
                Deduzir = Convert.ToDecimal(tabelaRow01.QuerySelector("body > table:nth-child(61) > tbody > tr:nth-child(2) > td:nth-child(4)").TextContent.Replace("\n\t\t-", "0")),
                IRPJ = Convert.ToDecimal(resultadoReparticao.QuerySelector("body > table:nth-child(63) > tbody > tr:nth-child(3) > td:nth-child(2)").TextContent.Replace("\n\t\t", "").Replace("%", "")),
                CSLL = Convert.ToDecimal(resultadoReparticao.QuerySelector("body > table:nth-child(63) > tbody > tr:nth-child(3) > td:nth-child(3)").TextContent.Replace("\n\t\t", "").Replace("%", "")),
                Cofins = Convert.ToDecimal(resultadoReparticao.QuerySelector("body > table:nth-child(63) > tbody > tr:nth-child(3) > td:nth-child(4)").TextContent.Replace("\n\t\t", "").Replace("%", "")),
                PISPasep = Convert.ToDecimal(resultadoReparticao.QuerySelector("body > table:nth-child(63) > tbody > tr:nth-child(3) > td:nth-child(5)").TextContent.Replace("\n\t\t", "").Replace("%", "")),
                CPP = Convert.ToDecimal(resultadoReparticao.QuerySelector("body > table:nth-child(63) > tbody > tr:nth-child(3) > td:nth-child(6)").TextContent.Replace("\n\t\t", "").Replace("%", "")),
                ICMS = Convert.ToDecimal(resultadoReparticao.QuerySelector("body > table:nth-child(63) > tbody > tr:nth-child(3) > td:nth-child(7)").TextContent.Replace("\n\t\t", "").Replace("%", ""))
            };

            tabelaExternas.Add(faixa01Anexo01);

            var tabelaRow02 = resultTabelaAnexo.QuerySelector("body > table:nth-child(61) > tbody > tr:nth-child(3)");
            var valoresFaxa02 = tabelaRow02.QuerySelector("body > table:nth-child(61) > tbody > tr:nth-child(3) > td:nth-child(2)").TextContent.Replace("\n\t\t\n\t\tDe", "").Split();

            var faixa02Anexo01 = new TabelaExterna()
            {
                Id = Guid.NewGuid(),
                Faixa = Convert.ToInt32(tabelaRow02.QuerySelector("body > table:nth-child(61) > tbody > tr:nth-child(3) > td:nth-child(1)").TextContent.Replace("\n\t\t\n\t\t2a \n\t\tFaixa", "2")),
                ValorInicial = String.Format("{0:#.#,##}", valoresFaxa02[1]),
                ValorFinal = String.Format("{0:#.#,##}", valoresFaxa02[3]),
                Aliquota = Convert.ToDecimal(tabelaRow02.QuerySelector("body > table:nth-child(61) > tbody > tr:nth-child(3) > td:nth-child(3)").TextContent.Replace("\n\t\t", "").Replace("%", "")),
                Deduzir = Convert.ToDecimal(tabelaRow02.QuerySelector("body > table:nth-child(61) > tbody > tr:nth-child(3) > td:nth-child(4)").TextContent.Replace("\n\t\t", "")),
                IRPJ = Convert.ToDecimal(resultadoReparticao.QuerySelector("body > table:nth-child(63) > tbody > tr:nth-child(4) > td:nth-child(2)").TextContent.Replace("\n\t\t", "").Replace("%", "")),
                CSLL = Convert.ToDecimal(resultadoReparticao.QuerySelector("body > table:nth-child(63) > tbody > tr:nth-child(4) > td:nth-child(3)").TextContent.Replace("\n\t\t", "").Replace("%", "")),
                Cofins = Convert.ToDecimal(resultadoReparticao.QuerySelector("body > table:nth-child(63) > tbody > tr:nth-child(4) > td:nth-child(4)").TextContent.Replace("\n\t\t", "").Replace("%", "")),
                PISPasep = Convert.ToDecimal(resultadoReparticao.QuerySelector("body > table:nth-child(63) > tbody > tr:nth-child(4) > td:nth-child(5)").TextContent.Replace("\n\t\t", "").Replace("%", "")),
                CPP = Convert.ToDecimal(resultadoReparticao.QuerySelector("body > table:nth-child(63) > tbody > tr:nth-child(4) > td:nth-child(6)").TextContent.Replace("\n\t\t", "").Replace("%", "")),
                ICMS = Convert.ToDecimal(resultadoReparticao.QuerySelector("body > table:nth-child(63) > tbody > tr:nth-child(4) > td:nth-child(7)").TextContent.Replace("\n\t\t", "").Replace("%", ""))
            };

            tabelaExternas.Add(faixa02Anexo01);

            var tabelaRow03 = resultTabelaAnexo.QuerySelector("body > table:nth-child(61) > tbody > tr:nth-child(4)");
            var valoresFaxa03 = tabelaRow03.QuerySelector("body > table:nth-child(61) > tbody > tr:nth-child(4) > td:nth-child(2)").TextContent.Replace("\n\t\t\n\t\tDe", "").Split();

            var faixa03Anexo01 = new TabelaExterna()
            {
                Id = Guid.NewGuid(),
                Faixa = Convert.ToInt32(tabelaRow03.QuerySelector("body > table:nth-child(61) > tbody > tr:nth-child(4) > td:nth-child(1)").TextContent.Replace("\n\t\t\n\t\t3a \n\t\tFaixa", "3")),
                ValorInicial = String.Format("{0:#.#,##}", valoresFaxa03[1]),
                ValorFinal = String.Format("{0:#.#,##}", valoresFaxa03[3]),
                Aliquota = Convert.ToDecimal(tabelaRow03.QuerySelector("body > table:nth-child(61) > tbody > tr:nth-child(4) > td:nth-child(3)").TextContent.Replace("\n\t\t", "").Replace("%", "")),
                Deduzir = Convert.ToDecimal(tabelaRow03.QuerySelector("body > table:nth-child(61) > tbody > tr:nth-child(4) > td:nth-child(4)").TextContent.Replace("\n\t\t", "")),
                IRPJ = Convert.ToDecimal(resultadoReparticao.QuerySelector("body > table:nth-child(63) > tbody > tr:nth-child(5) > td:nth-child(2)").TextContent.Replace("\n\t\t", "").Replace("%", "")),
                CSLL = Convert.ToDecimal(resultadoReparticao.QuerySelector("body > table:nth-child(63) > tbody > tr:nth-child(5) > td:nth-child(3)").TextContent.Replace("\n\t\t", "").Replace("%", "")),
                Cofins = Convert.ToDecimal(resultadoReparticao.QuerySelector("body > table:nth-child(63) > tbody > tr:nth-child(5) > td:nth-child(4)").TextContent.Replace("\n\t\t", "").Replace("%", "")),
                PISPasep = Convert.ToDecimal(resultadoReparticao.QuerySelector("body > table:nth-child(63) > tbody > tr:nth-child(5) > td:nth-child(5)").TextContent.Replace("\n\t\t", "").Replace("%", "")),
                CPP = Convert.ToDecimal(resultadoReparticao.QuerySelector("body > table:nth-child(63) > tbody > tr:nth-child(5) > td:nth-child(6)").TextContent.Replace("\n\t\t", "").Replace("%", "")),
                ICMS = Convert.ToDecimal(resultadoReparticao.QuerySelector("body > table:nth-child(63) > tbody > tr:nth-child(5) > td:nth-child(7)").TextContent.Replace("\n\t\t", "").Replace("%", ""))

            };

            tabelaExternas.Add(faixa03Anexo01);

            var tabelaRow04 = resultTabelaAnexo.QuerySelector("body > table:nth-child(61) > tbody > tr:nth-child(5)");
            var valoresFaxa04 = tabelaRow04.QuerySelector("body > table:nth-child(61) > tbody > tr:nth-child(5) > td:nth-child(2)").TextContent.Replace("\n\t\t\n\t\tDe ", "").Replace("a \n\t\t", "").Split();

            var faixa04Anexo01 = new TabelaExterna()
            {
                Id = Guid.NewGuid(),
                Faixa = Convert.ToInt32(tabelaRow04.QuerySelector("body > table:nth-child(61) > tbody > tr:nth-child(5) > td:nth-child(1)").TextContent.Replace("\n\t\t\n\t\t4a \n\t\tFaixa", "4")),
                ValorInicial = String.Format("{0:#.#,##}", valoresFaxa04[0]),
                ValorFinal = String.Format("{0:#.#,##}", valoresFaxa04[1]),
                Aliquota = Convert.ToDecimal(tabelaRow04.QuerySelector("body > table:nth-child(61) > tbody > tr:nth-child(5) > td:nth-child(3)").TextContent.Replace("\n\t\t", "").Replace("%", "")),
                Deduzir = Convert.ToDecimal(tabelaRow04.QuerySelector("body > table:nth-child(61) > tbody > tr:nth-child(5) > td:nth-child(4)").TextContent.Replace("\n\t\t", "")),
                IRPJ = Convert.ToDecimal(resultadoReparticao.QuerySelector("body > table:nth-child(63) > tbody > tr:nth-child(6) > td:nth-child(2)").TextContent.Replace("\n\t\t", "").Replace("%", "")),
                CSLL = Convert.ToDecimal(resultadoReparticao.QuerySelector("body > table:nth-child(63) > tbody > tr:nth-child(6) > td:nth-child(3)").TextContent.Replace("\n\t\t", "").Replace("%", "")),
                Cofins = Convert.ToDecimal(resultadoReparticao.QuerySelector("body > table:nth-child(63) > tbody > tr:nth-child(6) > td:nth-child(4)").TextContent.Replace("\n\t\t", "").Replace("%", "")),
                PISPasep = Convert.ToDecimal(resultadoReparticao.QuerySelector("body > table:nth-child(63) > tbody > tr:nth-child(6) > td:nth-child(5)").TextContent.Replace("\n\t\t", "").Replace("%", "")),
                CPP = Convert.ToDecimal(resultadoReparticao.QuerySelector("body > table:nth-child(63) > tbody > tr:nth-child(6) > td:nth-child(6)").TextContent.Replace("\n\t\t", "").Replace("%", "")),
                ICMS = Convert.ToDecimal(resultadoReparticao.QuerySelector("body > table:nth-child(63) > tbody > tr:nth-child(6) > td:nth-child(7)").TextContent.Replace("\n\t\t", "").Replace("%", ""))
            };

            tabelaExternas.Add(faixa04Anexo01);

            var tabelaRow05 = resultTabelaAnexo.QuerySelector("body > table:nth-child(61) > tbody > tr:nth-child(6)");
            var valoresFaxa05 = tabelaRow05.QuerySelector("body > table:nth-child(61) > tbody > tr:nth-child(6) > td:nth-child(2)").TextContent.Replace("\n\t\t\n\t\tDe ", "").Replace("a \n\t\t", "").Split();


            var faixa05Anexo01 = new TabelaExterna()
            {
                Id = Guid.NewGuid(),
                Faixa = Convert.ToInt32(tabelaRow05.QuerySelector("body > table:nth-child(61) > tbody > tr:nth-child(6) > td:nth-child(1)").TextContent.Replace("\n\t\t\n\t\t5a \n\t\tFaixa", "5")),
                ValorInicial = String.Format("{0:#.#,##}", valoresFaxa05[0]),
                ValorFinal = String.Format("{0:#.#,##}", valoresFaxa05[1]),
                Aliquota = Convert.ToDecimal(tabelaRow05.QuerySelector("body > table:nth-child(61) > tbody > tr:nth-child(6) > td:nth-child(3)").TextContent.Replace("\n\t\t", "").Replace("%", "")),
                Deduzir = Convert.ToDecimal(tabelaRow05.QuerySelector("body > table:nth-child(61) > tbody > tr:nth-child(6) > td:nth-child(4)").TextContent.Replace("\n\t\t", "")),
                IRPJ = Convert.ToDecimal(resultadoReparticao.QuerySelector("body > table:nth-child(63) > tbody > tr:nth-child(7) > td:nth-child(2)").TextContent.Replace("\n\t\t", "").Replace("%", "")),
                CSLL = Convert.ToDecimal(resultadoReparticao.QuerySelector("body > table:nth-child(63) > tbody > tr:nth-child(7) > td:nth-child(3)").TextContent.Replace("\n\t\t", "").Replace("%", "")),
                Cofins = Convert.ToDecimal(resultadoReparticao.QuerySelector("body > table:nth-child(63) > tbody > tr:nth-child(7) > td:nth-child(4)").TextContent.Replace("\n\t\t", "").Replace("%", "")),
                PISPasep = Convert.ToDecimal(resultadoReparticao.QuerySelector("body > table:nth-child(63) > tbody > tr:nth-child(7) > td:nth-child(5)").TextContent.Replace("\n\t\t", "").Replace("%", "")),
                CPP = Convert.ToDecimal(resultadoReparticao.QuerySelector("body > table:nth-child(63) > tbody > tr:nth-child(7) > td:nth-child(6)").TextContent.Replace("\n\t\t", "").Replace("%", "")),
                ICMS = Convert.ToDecimal(resultadoReparticao.QuerySelector("body > table:nth-child(63) > tbody > tr:nth-child(7) > td:nth-child(7)").TextContent.Replace("\n\t\t", "").Replace("%", ""))
            };

            tabelaExternas.Add(faixa05Anexo01);


            var tabelaRow06 = resultTabelaAnexo.QuerySelector("body > table:nth-child(61) > tbody > tr:nth-child(7)");
            var valoresFaxa06 = tabelaRow06.QuerySelector("body > table:nth-child(61) > tbody > tr:nth-child(7) > td:nth-child(2)").TextContent.Replace("\n\t\t\n\t\tDe ", "").Replace("a \n\t\t", "").Split();

            var faixa06Anexo01 = new TabelaExterna()
            {
                Id = Guid.NewGuid(),
                Faixa = Convert.ToInt32(tabelaRow06.QuerySelector("body > table:nth-child(61) > tbody > tr:nth-child(7) > td:nth-child(1)").TextContent.Replace("\n\t\t\n\t\t6a \n\t\tFaixa", "6")),
                ValorInicial = String.Format("{0:#.#,##}", valoresFaxa06[0]),
                ValorFinal = String.Format("{0:#.#,##}", valoresFaxa06[1]),
                Aliquota = Convert.ToDecimal(tabelaRow06.QuerySelector("body > table:nth-child(61) > tbody > tr:nth-child(7) > td:nth-child(3)").TextContent.Replace("\n\t\t", "").Replace("%", "")),
                Deduzir = Convert.ToDecimal(tabelaRow06.QuerySelector("body > table:nth-child(61) > tbody > tr:nth-child(7) > td:nth-child(4)").TextContent.Replace("\n\t\t", "")),
                IRPJ = Convert.ToDecimal(resultadoReparticao.QuerySelector("body > table:nth-child(63) > tbody > tr:nth-child(8) > td:nth-child(2)").TextContent.Replace("\n\t\t", "").Replace("%", "")),
                CSLL = Convert.ToDecimal(resultadoReparticao.QuerySelector("body > table:nth-child(63) > tbody > tr:nth-child(8) > td:nth-child(3)").TextContent.Replace("\n\t\t", "").Replace("%", "")),
                Cofins = Convert.ToDecimal(resultadoReparticao.QuerySelector("body > table:nth-child(63) > tbody > tr:nth-child(8) > td:nth-child(4)").TextContent.Replace("\n\t\t", "").Replace("%", "")),
                PISPasep = Convert.ToDecimal(resultadoReparticao.QuerySelector("body > table:nth-child(63) > tbody > tr:nth-child(8) > td:nth-child(5)").TextContent.Replace("\n\t\t", "").Replace("%", "")),
                CPP = Convert.ToDecimal(resultadoReparticao.QuerySelector("body > table:nth-child(63) > tbody > tr:nth-child(8) > td:nth-child(6)").TextContent.Replace("\n\t\t", "").Replace("%", "")),
                ICMS = 0
            };

            tabelaExternas.Add(faixa06Anexo01);
        }
    }
}
