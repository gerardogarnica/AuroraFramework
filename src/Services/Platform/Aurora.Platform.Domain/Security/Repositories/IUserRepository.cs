using Aurora.Framework.Collections;
using Aurora.Framework.Logic.Repositories;
using Aurora.Platform.Domain.Security.Models;
using System.Threading.Tasks;

namespace Aurora.Platform.Domain.Security.Repositories
{
    public interface IUserRepository : IQueryableRepository<UserData>, IWriteableRepository<UserData>
    {
        Task<UserData> GetAsync(string loginName);
        Task<PagedCollection<UserData>> GetListAsync(PagedViewRequest viewRequest, int roleId, bool onlyActives);
    }
}