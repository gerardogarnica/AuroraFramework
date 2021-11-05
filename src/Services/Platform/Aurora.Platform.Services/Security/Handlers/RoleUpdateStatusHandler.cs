using Aurora.Framework.Logic.Data;
using Aurora.Framework.Sessions;
using Aurora.Platform.Domain.Exceptions;
using Aurora.Platform.Domain.Security.Models;
using Aurora.Platform.Domain.Security.Repositories;
using Aurora.Platform.Services.Security.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Aurora.Platform.Services.Security.Handlers
{
    public class RoleUpdateStatusHandler : IRequestHandler<RoleUpdateStatusCommand, RoleResponse>
    {
        #region Miembros privados de la clase

        private readonly IRoleRepository _roleRepository;
        private readonly IAuroraSession _auroraSession;
        private readonly string _userName;

        #endregion

        #region Constructores de la clase

        public RoleUpdateStatusHandler(
            IRoleRepository roleRepository,
            IAuroraSession auroraSession)
        {
            _roleRepository = roleRepository;
            _auroraSession = auroraSession;

            _userName = _auroraSession.GetSessionInfo().UserLoginName;
        }

        #endregion

        #region Implementación de la interface IRequestHandler

        async Task<RoleResponse> IRequestHandler<RoleUpdateStatusCommand, RoleResponse>.Handle(
            RoleUpdateStatusCommand request, CancellationToken cancellationToken)
        {
            // Se obtiene el rol de usuario existente
            var entry = await GetExistentRoleData(request.RoleId);

            // Se actualiza el registro de rol de usuario
            UpdateRoleData(entry, request);
            entry = await _roleRepository.UpdateAsync(entry);

            return new RoleResponse(entry);
        }

        #endregion

        #region Miembros privados de la clase

        private void UpdateRoleData(RoleData role, RoleUpdateStatusCommand request)
        {
            role.IsActive = request.IsActive;
            role.AddLastUpdated(_userName);
        }

        private async Task<RoleData> GetExistentRoleData(int roleId)
        {
            var roleData = await _roleRepository.GetAsync(x => x.RoleId.Equals(roleId));

            if (roleData == null)
            {
                throw new InvalidRoleIdException(roleId);
            }

            return roleData;
        }

        #endregion
    }
}