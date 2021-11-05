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
    public class UserSaveRolesHandler : IRequestHandler<UserSaveRolesCommand, UserResponse>
    {
        #region Miembros privados de la clase

        private readonly IUserRepository _userRepository;
        private readonly IAuroraSession _auroraSession;
        private readonly string _userName;

        #endregion

        #region Constructores de la clase

        public UserSaveRolesHandler(
            IUserRepository userRepository,
            IAuroraSession auroraSession)
        {
            _userRepository = userRepository;
            _auroraSession = auroraSession;

            _userName = _auroraSession.GetSessionInfo().UserLoginName;
        }

        #endregion

        #region Implementación de la interface IRequestHandler

        async Task<UserResponse> IRequestHandler<UserSaveRolesCommand, UserResponse>.Handle(
            UserSaveRolesCommand request, CancellationToken cancellationToken)
        {
            // Se obtiene el usuario existente
            var entry = await GetExistentUserData(request.LoginName);

            // Se verifica que no se envíen roles duplicados
            VerifyDuplicatedRoles(request);

            // Se actualizan los registros de roles
            UpdateRoles(entry, request.RolesToAdd, BusinessActions.AddDetail);
            UpdateRoles(entry, request.RolesToRemove, BusinessActions.RemoveDetail);
            entry = await _userRepository.UpdateAsync(entry);

            return new UserResponse(entry);
        }

        #endregion

        #region Miembros privados de la clase

        private void UpdateRoles(UserData userData, IList<int> roles, BusinessActions action)
        {
            foreach (var roleId in roles)
            {
                var membership = userData.Memberships.ToList().Find(x => x.RoleId.Equals(roleId));

                if (membership == null)
                {
                    if (!action.Equals(BusinessActions.AddDetail)) continue;

                    userData.Memberships.Add(CreateMembershipData(roleId));
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

        private UserMembershipData CreateMembershipData(int roleId)
        {
            var membershipData = new UserMembershipData()
            {
                RoleId = roleId,
                IsDefaultMembership = false,
                IsActive = true
            };

            membershipData.AddCreated(_userName);
            membershipData.AddLastUpdated(_userName);

            return membershipData;
        }

        private async Task<UserData> GetExistentUserData(string loginName)
        {
            var userData = await _userRepository.GetAsync(loginName);

            if (userData == null)
            {
                throw new InvalidUserNameException(loginName);
            }

            return userData;
        }

        private void VerifyDuplicatedRoles(UserSaveRolesCommand request)
        {
            if (request.RolesToRemove.Any(x => request.RolesToAdd.Contains(x)))
            {
                throw new DuplicatedRoleException();
            }
        }

        #endregion
    }
}