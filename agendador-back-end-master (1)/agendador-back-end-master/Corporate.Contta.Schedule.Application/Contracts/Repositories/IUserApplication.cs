using Corporate.Contta.Schedule.Application.Mapping.Result.GenerateAccessToken;
using Corporate.Contta.Schedule.Domain.Filters;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Application.Contracts.Repositories
{
    public interface IUserApplication
    {
        Task<GenerateAccessTokenResponse> GenerateAccessToken(GenerateAccessTokenFilter validateLoginFilter, bool encryptPassword = true);
        void PasswordChangeRequest(string email);
    }
}
