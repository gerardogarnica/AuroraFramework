using Aurora.Framework.Collections;
using Aurora.Framework.Logic;
using Aurora.Platform.Domain.Security;
using Aurora.Platform.Domain.Security.Repositories;
using AutoMapper;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Aurora.Platform.Services.Security.Queries
{
    public interface IRoleQueryService
    {
        Task<Role> GetByIdAsync(int roleId);
        Task<Role> GetByNameAsync(int repositoryId, string name);
        Task<PagedCollection<Role>> GetListAsync(PagedViewRequest viewRequest, int repositoryId, bool onlyActives);
    }

    public class RoleQueryService : IRoleQueryService
    {
        #region Miembros privados de la clase

        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        #endregion

        #region Constructores de la clase

        public RoleQueryService(
            IRoleRepository roleRepository,
            IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        #endregion

        #region Implementación de la interface IRoleQueryService

        async Task<Role> IRoleQueryService.GetByIdAsync(int roleId)
        {
            var roleData = await _roleRepository
                .GetAsync(x => x.RoleId.Equals(roleId));

            if (roleData == null) return null;

            return _mapper.Map<Role>(roleData);
        }

        async Task<Role> IRoleQueryService.GetByNameAsync(int repositoryId, string name)
        {
            var roleData = await _roleRepository
                .GetAsync(x => x.RepositoryId.Equals(repositoryId) && x.Name.Equals(name));

            if (roleData == null) return null;

            return _mapper.Map<Role>(roleData);
        }

        async Task<PagedCollection<Role>> IRoleQueryService.GetListAsync(PagedViewRequest viewRequest, int repositoryId, bool onlyActives)
        {
            // Adición de filtros
            Expression<Func<Domain.Security.Models.RoleData, bool>> filter = x => x.RepositoryId.Equals(repositoryId);

            if (onlyActives)
                filter = filter.And(x => x.IsActive.Equals(true));

            var rolesData = await _roleRepository
                .GetPagedCollectionAsync(viewRequest, filter, x => x.Name);

            return _mapper.Map<PagedCollection<Role>>(rolesData);
        }

        #endregion
    }
}