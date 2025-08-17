using AngleSharp;
using ConsultaJUridica.Extensions;
using Corporate.Contta.Schedule.Domain.Entities.Imporsto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Infra.Tools
{
    public class CrawlerConsultaNcm
    {
        public async Task<string> GetAnexo()
        {
            var request = (HttpWebRequest)WebRequest.Create($"https://www.contabilizei.com.br/contabilidade-online/ncm/");
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

            List<TabelaNcm> tabelaNcms = new List<TabelaNcm>();

            var document = await context.OpenAsync(req => req.Content(html));

            var resultSelector = "body";
            var result = document.QuerySelector(resultSelector);

            var tabela = document.QuerySelector("#wtr-content > table");

            var totalizador = tabela.QuerySelector("#wtr-content > table > tbody > tr:nth-child(994) > td:nth-child(3)");

            for (int i = 2; i < 995; i++)
            {
                tabelaNcms.Add(new TabelaNcm
                {

                    Id = Guid.NewGuid(),
                    Ncm = tabela.QuerySelector($"#wtr-content > table > tbody > tr:nth-child({i}) > td:nth-child(2)").TextContent.Replace("\n  ", "").Trim().ToString(),
                    Descricao = tabela.QuerySelector($"#wtr-content > table > tbody > tr:nth-child({i}) > td:nth-child(3)").TextContent.Replace("\n", "").Replace("\n", "").Trim().ToString()

                });
            }


            return anexo;
        }
    }
}
