using Corporate.Contta.Schedule.Domain.Entities.UserAgg;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Domain.Contracts.Repositories
{
    public interface IUserRepository : IRepositorio<User>
    {
        Task<bool> ExistsUser(string userName);
        Task<User> Login(string email);
        Task<List<User>> GetUsersByMasterId(Guid userMasterId);
        Task<User> GetUsersByMaster(Guid userMasterId);
        Task DeleteUserById(Guid userId);
        Task<bool> UpdateUser(User user);
        Task<bool> UpdateUser(User user, Guid? id);
        Task<TokenAcesso> GetToken(TokenAcesso user);
        Task<bool> RedefinePassword(Guid id, string passwordNew);
        Task<User> GetUserById(Guid Id);
        Task<User> GetUser(Guid Id);
        Task<List<UserCompany>> GetAllUserCompany(Guid id);
        Task<bool> ChangeLinkWithTheCompany(Guid id,List<UserCompany> userCompanies);

        Task<bool> UpdateTokenAcess(User user);
    }
}
