using Microsoft.AspNetCore.Http;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;

namespace Aurora.Framework.Sessions
{
    /// <summary>
    /// Implementación de procesos de administración de sesiones de usuario de Aurora.
    /// </summary>
    public class AuroraSession : AuroraHttpClientBase, IAuroraSession
    {
        #region Constructores de la clase

        /// <summary>
        /// Inicializa una nueva instancia de la clase AuroraSession.
        /// </summary>
        /// <param name="httpClient">Clase base para envío de requerimientos HTTP y recepción de respuestas HTTP.</param>
        /// <param name="httpContextAccessor">Acceso a contextos HTTP.</param>
        public AuroraSession(
            HttpClient httpClient,
            IHttpContextAccessor httpContextAccessor)
            : base(httpClient, httpContextAccessor) { }

        #endregion

        #region Implementación de interface IAuroraSession

        AuroraSessionInfo IAuroraSession.GetSessionInfo()
        {
            var accessToken = AuroraHttpClient.DefaultRequestHeaders.Authorization.Parameter;
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(accessToken);

            return new AuroraSessionInfo()
            {
                UserId = Convert.ToInt32(jwt.Claims.FirstOrDefault(x => x.Type.Contains("sid")).Value),
                UserLoginName = jwt.Claims.FirstOrDefault(x => x.Type.Equals("nameid")).Value,
                UserDescription = jwt.Claims.FirstOrDefault(x => x.Type.Equals("unique_name")).Value
            };
        }

        #endregion
    }
}