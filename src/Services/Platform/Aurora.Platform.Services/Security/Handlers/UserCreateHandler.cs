using Aurora.Framework.Cryptography;
using Aurora.Framework.Logic.Data;
using Aurora.Framework.Sessions;
using Aurora.Platform.Domain.Exceptions;
using Aurora.Platform.Domain.Security.Models;
using Aurora.Platform.Domain.Security.Repositories;
using Aurora.Platform.Services.Security.Commands;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Aurora.Platform.Services.Security.Handlers
{
    public class UserCreateHandler : IRequestHandler<UserCreateCommand, UserResponse>
    {
        #region Miembros privados de la clase

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IAuroraSession _auroraSession;
        private readonly string _userName;

        #endregion

        #region Constructores de la clase

        public UserCreateHandler(
            IUserRepository userRepository,
            IMapper mapper,
            IAuroraSession auroraSession)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _auroraSession = auroraSession;

            _userName = _auroraSession.GetSessionInfo().UserLoginName;
        }

        #endregion

        #region Implementación de la interface IRequestHandler

        async Task<UserResponse> IRequestHandler<UserCreateCommand, UserResponse>.Handle(
            UserCreateCommand request, CancellationToken cancellationToken)
        {
            // Se verifica si el nombre de usuario ya se encuentra registrado
            await VerifyIfUserExists(request.LoginName);

            // Se crea el registro de usuario
            var entry = CreateUserData(request);
            entry.Credential = CreateUserCredentialData(entry.LoginName);
            entry = await _userRepository.InsertAsync(entry);

            return new UserResponse(entry);
        }

        #endregion

        #region Métodos privados de la clase

        private UserData CreateUserData(UserCreateCommand request)
        {
            return _mapper.Map<UserData>(request);
        }

        private UserCredentialData CreateUserCredentialData(string loginName)
        {
            // Se encripta la contraseña del usuario
            var passwordContent = EncryptionProvider.Protect(
                loginName, out string passwordControl);

            var credentialData = new UserCredentialData()
            {
                Password = passwordContent,
                PasswordControl = passwordControl,
                MustChange = true,
                ExpirationDate = DateTime.Now.Date
            };

            credentialData.AddCreated(_userName);
            credentialData.AddLastUpdated(_userName);

            return credentialData;
        }

        private async Task VerifyIfUserExists(string loginName)
        {
            var userData = await _userRepository.GetAsync(loginName);

            if (userData != null)
            {
                throw new ExistsUserNameException(loginName);
            }
        }

        #endregion
    }
}