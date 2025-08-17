using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Corporate.Contta.Schedule.Domain.Contracts.Repositories
{
    public interface IUserContextAcessorRepository
    {
        string  Name { get; }
        string Id { get; }
        bool IsAuthenticated();
        IEnumerable<Claim> GetClaimsIdentity();
    }
}
