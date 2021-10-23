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
    public class UserUpdateHandler : IRequestHandler<UserUpdateCommand, UserResponse>
    {
        #region Miembros privados de la clase

        private readonly IUserRepository _userRepository;
        private readonly IAuroraSession _auroraSession;
        private readonly string _userName;

        #endregion

        #region Constructores de la clase

        public UserUpdateHandler(
            IUserRepository userRepository,
            IAuroraSession auroraSession)
        {
            _userRepository = userRepository;
            _auroraSession = auroraSession;

            _userName = _auroraSession.GetSessionInfo().UserLoginName;
        }

        #endregion

        #region Implementación de la interface IRequestHandler

        async Task<UserResponse> IRequestHandler<UserUpdateCommand, UserResponse>.Handle(
            UserUpdateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Se obtiene el usuario existente
                var entry = await GetExistentUserData(request.LoginName);

                // Se actualiza el registro de usuario
                UpdateUserData(entry, request);
                entry = await _userRepository.UpdateAsync(entry);

                return new UserResponse(entry);
            }
            catch (Framework.Exceptions.BusinessException e)
            {
                return new UserResponse(e.ErrorKeyName, e.Message);
            }
        }

        #endregion

        #region Miembros privados de la clase

        private void UpdateUserData(UserData user, UserUpdateCommand request)
        {
            user.IsActive = request.IsActive;
            user.AddLastUpdated(_userName);
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

        #endregion
    }
}