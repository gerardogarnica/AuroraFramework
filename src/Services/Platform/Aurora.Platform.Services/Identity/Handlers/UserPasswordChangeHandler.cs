using Aurora.Framework.Cryptography;
using Aurora.Framework.Logic.Data;
using Aurora.Framework.Platform;
using Aurora.Framework.Sessions;
using Aurora.Platform.Domain.Exceptions;
using Aurora.Platform.Domain.Security.Models;
using Aurora.Platform.Domain.Security.Repositories;
using Aurora.Platform.Services.Identity.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Aurora.Platform.Services.Identity.Handlers
{
    public class UserPasswordChangeHandler : IRequestHandler<UserPasswordChangeCommand, UserPasswordChangeResponse>
    {
        #region Miembros privados de la clase

        private readonly IUserRepository _userRepository;
        private readonly IUserCredentialRepository _userCredentialRepository;
        private readonly IAuroraSession _auroraSession;
        private readonly ISettingsServices _settingsServices;
        private readonly AuroraSessionInfo _sessionInfo;
        private IList<Framework.Platform.Settings.AttributeValue> _settingsValues;

        #endregion

        #region Constructores de la clase

        public UserPasswordChangeHandler(
            IUserRepository userRepository,
            IUserCredentialRepository userCredentialRepository,
            IAuroraSession auroraSession,
            ISettingsServices settingsServices)
        {
            _userRepository = userRepository;
            _userCredentialRepository = userCredentialRepository;
            _auroraSession = auroraSession;
            _settingsServices = settingsServices;

            _sessionInfo = _auroraSession.GetSessionInfo();
        }

        #endregion

        #region Implementación de la interface IRequestHandler

        async Task<UserPasswordChangeResponse> IRequestHandler<UserPasswordChangeCommand, UserPasswordChangeResponse>.Handle(
            UserPasswordChangeCommand request, CancellationToken cancellationToken)
        {
            // Se obtiene y valida el usuario
            var userData = await GetExistentUserData();

            // Se valida la contraseña
            var entry = userData.Credential;
            ValidatePassword(entry, request.CurrentPassword);

            // Se obtienen los atributos de configuraciones de Seguridad
            await GetSecurityAttributesAsync();

            // Se actualiza el registro de credencial
            Update(userData.Credential, request.NewPassword);
            entry = await _userCredentialRepository.UpdateAsync(entry);

            return new UserPasswordChangeResponse();
        }

        private async Task GetSecurityAttributesAsync()
        {
            _settingsValues = await _settingsServices.GetValues("Security");
        }

        #endregion

        #region Métodos privados de la clase

        private void Update(UserCredentialData credentialData, string newPassword)
        {
            // Se valida que la contraseña cumpla con la política
            ValidatePasswordPattern(newPassword);

            // Se valida que la contraseña no haya sido ingresada con anterioridad

            // Se encripta la contraseña del usuario
            var passwordContent = EncryptionProvider
                .Protect(newPassword, out string passwordControl);

            // Se obtiene el parámetro de política de expiración de contraseñas
            var usePasswordExpirationPolicy = IsEnabledUserPasswordExpirationPolicy();

            credentialData.Password = passwordContent;
            credentialData.PasswordControl = passwordControl;
            credentialData.MustChange = usePasswordExpirationPolicy;
            credentialData.ExpirationDate = null;
            credentialData.AddLastUpdated(_sessionInfo.UserLoginName);

            if (usePasswordExpirationPolicy)
            {
                // Se obtiene el parámetro de días de validez de contraseña
                var expirationDays = GetUserPasswordExpirationDays();

                credentialData.ExpirationDate = DateTime.Now.Date.AddDays(expirationDays);
            }
        }

        private async Task<UserData> GetExistentUserData()
        {
            var loginName = _sessionInfo.UserLoginName;

            var userData = await _userRepository.GetAsync(loginName);

            if (userData == null)
            {
                throw new InvalidUserNameException(loginName);
            }

            if (!userData.IsActive)
            {
                throw new InactiveUserException(loginName);
            }

            return userData;
        }

        private void ValidatePassword(UserCredentialData credential, string currentPassword)
        {
            var dataPassword = EncryptionProvider
                .Unprotect(credential.Password, credential.PasswordControl);

            if (!dataPassword.Equals(currentPassword))
            {
                throw new InvalidCredentialsException();
            }
        }

        private bool IsEnabledUserPasswordExpirationPolicy()
        {
            var userPasswordExpirationPolicySetting = _settingsValues.GetValue("UserPasswordExpirationPolicy");

            return userPasswordExpirationPolicySetting.BooleanValue.Value;
        }

        private int GetUserPasswordExpirationDays()
        {
            var userPasswordExpirationDaysSetting = _settingsValues.GetValue("UserPasswordExpirationDays");

            return userPasswordExpirationDaysSetting.IntegerValue.Value;
        }

        private int GetUserPasswordHistoryValidationCount()
        {
            var userPasswordHistoryValidationCountSetting = _settingsValues.GetValue("UserPasswordHistoryValidationCount");

            return userPasswordHistoryValidationCountSetting.IntegerValue.Value;
        }

        private void ValidatePasswordPattern(string newPassword)
        {
            var userPasswordPatternPolicySetting = _settingsValues.GetValue("UserPasswordPatternPolicy");

            var pattern = userPasswordPatternPolicySetting.TextValue.Value;

            if (string.IsNullOrWhiteSpace(pattern)) return;

            try
            {
                Regex.Match("", pattern);
            }
            catch { return; }

            if (!Regex.IsMatch(newPassword, pattern))
            {
                throw new InvalidPasswordPatternException("");
            }
        }

        #endregion
    }
}