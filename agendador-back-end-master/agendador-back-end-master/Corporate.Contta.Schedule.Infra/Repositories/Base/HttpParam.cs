namespace Corporate.Contta.Schedule.Infra.Repositories.Base
{
    public sealed class HttpParam : IHttpParam
    { 
        public HttpParam(string apiUrl, string token)
        {
            ApiUrl = apiUrl;
            Token = token;           
        }

        public string ApiUrl { get; private set; }

        public string Token { get; private set; }

        public bool Authentication { get; private set; }

        public string GetApiUrl() => ApiUrl;

        public string GetToken() => Token;

        public bool IsAuthentication() => Authentication;
    }
}
