using Jering.Javascript.NodeJS;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Specialized;
using System.Text;

namespace RoboEconet.Helpers
{
    public static class Util
    {
        public static string ObterTokenBuscaPisConfins(string ncm)
        {
            string form = $"form%5Bncm%5D={ncm}&amp;form%5Bpalavra_chave%5D=&amp;form%5Btipo_busca%5D=ncm&amp;form%5Bacao%5D=pesquisar";
            string javascriptModule = @"module.exports=((r,e,t)=>{var o={_keyStr:'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=',encode:function(r){var e,t,a,c,h,n,d,f='',i=0;for(r=o._utf8_encode(r);i<r.length;)c=(e=r.charCodeAt(i++))>>2,h=(3&e)<<4|(t=r.charCodeAt(i++))>>4,n=(15&t)<<2|(a=r.charCodeAt(i++))>>6,d=63&a,isNaN(t)?n=d=64:isNaN(a)&&(d=64),f=f+this._keyStr.charAt(c)+this._keyStr.charAt(h)+this._keyStr.charAt(n)+this._keyStr.charAt(d);return f},decode:function(r){var e,t,a,c,h,n,d='',f=0;for(r=r.replace(/[^A-Za-z0-9\+\/\=]/g,'');f<r.length;)e=this._keyStr.indexOf(r.charAt(f++))<<2|(c=this._keyStr.indexOf(r.charAt(f++)))>>4,t=(15&c)<<4|(h=this._keyStr.indexOf(r.charAt(f++)))>>2,a=(3&h)<<6|(n=this._keyStr.indexOf(r.charAt(f++))),d+=String.fromCharCode(e),64!=h&&(d+=String.fromCharCode(t)),64!=n&&(d+=String.fromCharCode(a));return d=o._utf8_decode(d)},_utf8_encode:function(r){r=r.replace(/\r\n/g,'\n');for(var e='',t=0;t<r.length;t++){var o=r.charCodeAt(t);o<128?e+=String.fromCharCode(o):o>127&&o<2048?(e+=String.fromCharCode(o>>6|192),e+=String.fromCharCode(63&o|128)):(e+=String.fromCharCode(o>>12|224),e+=String.fromCharCode(o>>6&63|128),e+=String.fromCharCode(63&o|128))}return e},_utf8_decode:function(r){for(var e='',t=0,o=c1=c2=0;t<r.length;)(o=r.charCodeAt(t))<128?(e+=String.fromCharCode(o),t++):o>191&&o<224?(c2=r.charCodeAt(t+1),e+=String.fromCharCode((31&o)<<6|63&c2),t+=2):(c2=r.charCodeAt(t+1),c3=r.charCodeAt(t+2),e+=String.fromCharCode((15&o)<<12|(63&c2)<<6|63&c3),t+=3);return e}},a=o.encode('" + form + "');r(null,a+='&form[acao]=pesquisar')});";

            var services = new ServiceCollection();
            services.AddNodeJS();
            ServiceProvider serviceProvider = services.BuildServiceProvider();
            INodeJSService nodeJSService = serviceProvider.GetRequiredService<INodeJSService>();

            return nodeJSService.InvokeFromStringAsync<string>(javascriptModule).Result;
        }

        public static decimal? ParaDecimais(this string retorno)
        {
            if (string.IsNullOrWhiteSpace(retorno)) return null;
            retorno = retorno.Replace("%", string.Empty).Trim();

            if (decimal.TryParse(retorno, out decimal valor))
                return valor;

            return null;
        }

        public static string CorrigeEncoding(this string retorno)
        {
            Encoding iso = Encoding.GetEncoding("ISO-8859-1");
            Encoding utf8 = Encoding.UTF8;
            byte[] utfBytes = utf8.GetBytes(retorno);
            byte[] isoBytes = Encoding.Convert(utf8, iso, utfBytes);
            return iso.GetString(isoBytes);
        }

        public static string RemoveContentType(this string retorno) =>
            retorno
            .Replace("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=windows-1252\" />", string.Empty)
            .Replace("<meta http-equiv=\"content-type\" content=\"text/html;charset=windows-1252\" />", string.Empty)
            .Replace("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=windows-1252\">", string.Empty);

        public static string FormataPost(NameValueCollection postData)
        {
            string post = string.Empty;
            for (int i = 0; i < postData.Count; i++)
            {
                if (i == 0)
                    post += string.Format("{0}={1}", postData.Keys[i], postData[i]);
                else
                    post += string.Format("&{0}={1}", postData.Keys[i], postData[i]);
            }
            return post;
        }

        public static string PesquisaHtml(string resposta, string inicio)
        {
            int pos;
            string retorno = string.Empty;

            pos = resposta.IndexOf(inicio);

            if (pos > -1)
            {
                retorno = resposta.Remove(0, pos);
                pos = retorno.IndexOf("\">");

                if (pos > -1)
                {
                    retorno = retorno.Substring(0, pos);
                    retorno = retorno.Replace(inicio, string.Empty);
                }
            }

            return retorno;
        }

        public static string PesquisaHtml(string resposta, string inicio, string fim)
        {
            int pos;
            string retorno = string.Empty;

            pos = resposta.IndexOf(inicio);

            if (pos > -1)
            {
                retorno = resposta.Remove(0, pos);
                pos = retorno.IndexOf(fim);

                if (pos > -1)
                {
                    retorno = retorno.Substring(0, pos);
                    retorno = retorno.Replace(inicio, string.Empty);
                }
            }

            return retorno;
        }

        public static string PesquisaHtml2(string resposta, string inicio, string fim)
        {
            int pos;
            string retorno = string.Empty;

            pos = resposta.IndexOf(inicio) + inicio.Length;

            if (pos > -1)
            {
                retorno = resposta.Remove(0, pos);
                pos = retorno.IndexOf(fim);

                if (pos > -1)
                {
                    retorno = retorno.Substring(0, pos);
                }
            }

            return retorno;
        }
    }
}
