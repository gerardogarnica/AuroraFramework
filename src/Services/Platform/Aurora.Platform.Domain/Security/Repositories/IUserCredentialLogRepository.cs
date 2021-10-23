using Aurora.Framework.Logic.Repositories;
using Aurora.Platform.Domain.Security.Models;

namespace Aurora.Platform.Domain.Security.Repositories
{
    public interface IUserCredentialLogRepository : IQueryableRepository<UserCredentialLogData>
    {
    }
}