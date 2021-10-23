using Aurora.Framework;
using Aurora.Framework.Logic.Data;
using Aurora.Framework.Sessions;
using Aurora.Platform.Domain.Exceptions;
using Aurora.Platform.Domain.Security.Models;
using Aurora.Platform.Domain.Security.Repositories;
using Aurora.Platform.Services.Security.Commands;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Aurora.Platform.Services.Security.Handlers
{
    public class RoleSaveUsersHandler : IRequestHandler<RoleSaveUsersCommand, RoleResponse>
    {
        #region Miembros privados de la clase

        private readonly IRoleRepository _roleRepository;
        private readonly IAuroraSession _auroraSession;
        private readonly string _userName;

        #endregion

        #region Constructores de la clase

        public RoleSaveUsersHandler(
            IRoleRepository roleRepository,
            IAuroraSession auroraSession)
        {
            _roleRepository = roleRepository;
            _auroraSession = auroraSession;

            _userName = _auroraSession.GetSessionInfo().UserLoginName;
        }

        #endregion

        #region Implementación de la interface IRequestHandler

        async Task<RoleResponse> IRequestHandler<RoleSaveUsersCommand, RoleResponse>.Handle(
            RoleSaveUsersCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Se obtiene el rol de usuario existente
                var entry = await GetExistentRoleData(request.RoleId);

                // Se verifica que no se envíen usuarios duplicados
                VerifyDuplicatedUsers(request);

                // Se actualizan los registros de usuarios
                UpdateUsers(entry, request.UsersToAdd, BusinessActions.AddDetail);
                UpdateUsers(entry, request.UsersToRemove, BusinessActions.RemoveDetail);
                entry = await _roleRepository.UpdateAsync(entry);

                return new RoleResponse(entry);
            }
            catch (Framework.Exceptions.BusinessException e)
            {
                return new RoleResponse(e.ErrorKeyName, e.Message);
            }
        }

        #endregion

        #region Miembros privados de la clase

        private void UpdateUsers(RoleData role, IList<int> users, BusinessActions action)
        {
            foreach (var userId in users)
            {
                var membership = role.Memberships.ToList().Find(x => x.UserId.Equals(userId));

                if (membership == null)
                {
                    if (!action.Equals(BusinessActions.AddDetail)) continue;

                    role.Memberships.Add(CreateMembershipData(userId));
                }
                else
                {
                    if (!membership.IsActive && action.Equals(BusinessActions.AddDetail))
                    {
                        membership.IsActive = true;
                        membership.AddLastUpdated(_userName);
                    }
                    else if (membership.IsActive && action.Equals(BusinessActions.RemoveDetail))
                    {
                        membership.IsActive = false;
                        membership.AddLastUpdated(_userName);
                    }
                }
            }
        }

        private UserMembershipData CreateMembershipData(int userId)
        {
            var membershipData = new UserMembershipData()
            {
                UserId = userId,
                IsDefaultMembership = false,
                IsActive = true
            };

            membershipData.AddCreated(_userName);
            membershipData.AddLastUpdated(_userName);

            return membershipData;
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

        private void VerifyDuplicatedUsers(RoleSaveUsersCommand request)
        {
            if (request.UsersToRemove.Any(x => request.UsersToAdd.Contains(x)))
            {
                throw new DuplicatedRoleException();
            }
        }

        #endregion
    }
}