using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using EmaloteContta.Models.NotaFiscalEletronicaMod55;

namespace EmaloteContta.Infra.Repositorios
{
    public class NotaFiscalRepositorio
    {
        private RestClient _restClient;
        private RestRequest _restRequest;
        private string _uriBase = "https://localhost:5001/";
        public NotaFiscalRepositorio()
        {
            _restClient = new RestClient();
            _restRequest = new RestRequest();
        }

        public List<string> Send(List<NfeProc> nfe)
        {
            _restRequest.Parameters.Clear();

           
            _restClient.BaseUrl = new Uri(_uriBase);
            _restRequest.AddHeader("Content-type", "application/json");
            _restRequest.Resource = "api/notafiscal/newnfexml";
            _restRequest.Method = Method.POST;
            _restRequest.AddQueryParameter("key", "17bc083eaca50bf2ee8ac156099585c9");
            _restRequest.AddJsonBody(nfe);

            var result = _restClient.Execute<List<string>>(_restRequest);

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
                return null;
            else if (result.StatusCode == System.Net.HttpStatusCode.PartialContent)
                return result.Data;          
            else 
            {
                if (string.IsNullOrEmpty(result.Content))
                      throw new Exception("Servidor remoto nao respondeu.");
                return result.Data;
            }
                
        }
    }
}
