using AngleSharp;
using ConsultaJUridica.Extensions;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Infra.Tools
{
    public class CrawlerConsultaAnexo
    {
        public async Task<string> GetAnexo(string cnae)
        {
            var request = (HttpWebRequest)WebRequest.Create($"https://www.contabeis.com.br/ferramentas/simples-nacional/{cnae}/");
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

            var resultCnae = document.QuerySelector("body > div.centro.ferramentaSimples.detalhe > div.boxEsquerda > div:nth-child(6) > div > div.item.simples > ul > li:nth-child(3) > a:nth-child(2)");

            if (resultCnae != null)
                anexo = resultCnae.InnerHtml;

            return anexo;
        }
    }
}
