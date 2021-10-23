using Aurora.Framework.Collections;
using Aurora.Framework.Logic;
using Aurora.Framework.Logic.Repositories;
using Aurora.Platform.Domain.Security.Models;
using Aurora.Platform.Domain.Security.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Aurora.Platform.Repositories.Security
{
    public class UserRepository : DataRepository<UserData>, IUserRepository
    {
        #region Miembros privados de la clase

        private readonly PlatformDataContext _dataContext;

        #endregion

        #region Constructores de la clase

        public UserRepository(PlatformDataContext dataContext)
            : base(dataContext)
        {
            _dataContext = dataContext;
        }

        #endregion

        #region Implementación de la interface IUserRepository

        async Task<UserData> IUserRepository.GetAsync(string loginName)
        {
            return await _dataContext
                .Users
                .AsNoTracking()
                .Include(x => x.Credential)
                .Include(x => x.Memberships)
                .FirstOrDefaultAsync(x => x.LoginName.Equals(loginName));
        }

        async Task<PagedCollection<UserData>> IUserRepository.GetListAsync(
            PagedViewRequest viewRequest, int roleId, bool onlyActives)
        {
            var usersIds = await _dataContext
                .Memberships
                .Where(x => roleId > 0 ? x.RoleId.Equals(roleId) : x.RoleId.Equals(x.RoleId) &&
                            x.IsActive)
                .OrderBy(x => x.User.LoginName)
                .Skip(viewRequest.PageIndex * viewRequest.PageSize)
                .Take(viewRequest.PageSize)
                .Select(x => x.UserId)
                .ToArrayAsync();

            return await (from s in _dataContext.Users.Include(x => x.Credential)
                          where usersIds.Contains(s.UserId)
                          select s)
                          .ToPagedCollectionAsync(viewRequest.PageIndex, viewRequest.PageSize);
        }

        #endregion
    }
}