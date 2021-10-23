using Aurora.Framework.Collections;
using Aurora.Platform.Domain.Security;
using Aurora.Platform.Domain.Security.Models;
using Aurora.Platform.Domain.Security.Repositories;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aurora.Platform.Services.Security.Queries
{
    public interface IUserQueryService
    {
        Task<User> GetByLoginNameAsync(string loginName);
        Task<PagedCollection<User>> GetListAsync(PagedViewRequest viewRequest, int roleId, bool onlyActives);
    }

    public class UserQueryService : IUserQueryService
    {
        #region Miembros privados de la clase

        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        #endregion

        #region Constructores de la clase

        public UserQueryService(
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        #endregion

        #region Implementación de la interface IUserQueryService

        async Task<User> IUserQueryService.GetByLoginNameAsync(string loginName)
        {
            var userData = await _userRepository.GetAsync(loginName);

            if (userData == null) return null;

            var user = _mapper.Map<User>(userData);
            user.Roles = await GetRoles(userData);

            return user;
        }

        async Task<PagedCollection<User>> IUserQueryService.GetListAsync(
            PagedViewRequest viewRequest, int roleId, bool onlyActives)
        {
            var usersData = await _userRepository
                .GetListAsync(viewRequest, roleId, onlyActives);

            return _mapper.Map<PagedCollection<User>>(usersData);
        }

        #endregion

        #region Métodos privados de la clase

        private async Task<IList<Role>> GetRoles(UserData user)
        {
            var roles = new List<Role>();

            foreach (var membership in user.Memberships.ToList().Where(x => x.IsActive))
            {
                var roleData = await _roleRepository.GetAsync(x => x.RoleId.Equals(membership.RoleId));
                roles.Add(_mapper.Map<Role>(roleData));
            }

            return roles;
        }

        #endregion
    }
}