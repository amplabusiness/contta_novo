using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PortalMoedas.Extensions
{
    public abstract class BaseApi
    {
        protected static readonly HttpStatusCode[] ValidStatusCodes = new[]
        {
            HttpStatusCode.OK,
            HttpStatusCode.Created,
            HttpStatusCode.Accepted
        };

        protected virtual IRestRequest CreateRequest( string resource, Method method)
        {
            var request = new RestRequest(resource, method)
            {
                JsonSerializer = new ERPJsonSerializer()
            };          

            return request;
        }

        protected virtual RestClient CreateClient(string baseUrl)
        {
            var client = new RestClient(baseUrl)
            {
                Proxy = WebRequest.DefaultWebProxy
            };

            return client;
        }

        protected virtual void VerifyResponse(IRestResponse response)
        {
            if (!ValidStatusCodes.Contains(response.StatusCode) ||
                response.ResponseStatus != ResponseStatus.Completed)
            {
               
            }
        }
    }
}
