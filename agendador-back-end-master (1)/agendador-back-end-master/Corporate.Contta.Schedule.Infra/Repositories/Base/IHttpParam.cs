namespace Corporate.Contta.Schedule.Infra.Repositories.Base
{
    public interface IHttpParam
    {
        string GetApiUrl();
        string GetToken();
        bool IsAuthentication();
    }
}
