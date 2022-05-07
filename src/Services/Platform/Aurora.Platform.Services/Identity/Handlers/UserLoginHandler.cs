using Aurora.Platform.Domain.Exceptions;
using Aurora.Platform.Domain.Security.Models;
using Aurora.Platform.Domain.Security.Repositories;
using Aurora.Platform.Services.Identity.Commands;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aurora.Platform.Services.Identity.Handlers
{
    public class UserLoginHandler : IRequestHandler<UserLoginCommand, IdentityAccess>
    {
        #region Miembros privados de la clase

        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        #endregion

        #region Constructores de la clase

        public UserLoginHandler(
            IConfiguration configuration,
            IUserRepository userRepository,
            IRoleRepository roleRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        #endregion

        #region Implementación de la interface IRequestHandler

        async Task<IdentityAccess> IRequestHandler<UserLoginCommand, IdentityAccess>.Handle(
            UserLoginCommand request, CancellationToken cancellationToken)
        {
            // Se obtiene y valida el usuario
            var userData = await GetExistentUserData(request.LoginName, request.Password);

            // Se generan los Claims
            var claims = await CreateClaims(userData);

            // Se genera el Token Descriptor
            var tokenDescriptor = CreateTokenDescriptor(claims);

            // Se genera el Token de Seguridad
            var token = new JwtSecurityTokenHandler();
            var createdToken = token.CreateToken(tokenDescriptor);

            return new IdentityAccess(token.WriteToken(createdToken));
        }

        #endregion

        #region Métodos privados de la clase

        private async Task<UserData> GetExistentUserData(string loginName, string password)
        {
            var userData = await _userRepository.GetAsync(loginName);

            if (userData == null)
            {
                throw new InvalidCredentialsException();
            }

            var dataPassword = Framework
                .Cryptography
                .EncryptionProvider
                .Unprotect(userData.Credential.Password, userData.Credential.PasswordControl);

            if (!dataPassword.Equals(password))
            {
                throw new InvalidCredentialsException();
            }

            if (!userData.IsActive)
            {
                throw new InactiveUserException(loginName);
            }

            if (userData.Credential.MustChange)
            {
                if (DateTime.Now.Date > userData.Credential.ExpirationDate.Value.Date)
                {
                    throw new PasswordExpiredException();
                }
            }

            return userData;
        }

        private async Task<List<Claim>> CreateClaims(UserData userData)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Sid, userData.UserId.ToString()),
                new Claim(ClaimTypes.NameIdentifier, userData.LoginName),
                new Claim(ClaimTypes.GivenName, userData.FirstName),
                new Claim(ClaimTypes.Surname, userData.LastName),
                new Claim(ClaimTypes.Name, string.Format("{0} {1}", userData.FirstName, userData.LastName).Trim())
            };

            foreach (var membershipData in userData.Memberships)
            {
                var roleData = await _roleRepository.GetAsync(x => x.RoleId.Equals(membershipData.RoleId));
                claims.Add(new Claim(ClaimTypes.Role, roleData.Name));
            }

            return claims;
        }

        private SecurityTokenDescriptor CreateTokenDescriptor(List<Claim> claims)
        {
            var secretKey = _configuration.GetValue<string>("SecretKey");
            var key = Encoding.ASCII.GetBytes(secretKey);

            return new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
        }

        #endregion
    }
}