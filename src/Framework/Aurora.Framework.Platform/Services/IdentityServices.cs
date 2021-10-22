using Aurora.Framework.Platform.Identity;
using Aurora.Framework.Proxies;
using System.Threading.Tasks;

namespace Aurora.Framework.Platform
{
    /// <summary>
    /// Administrador de servicios de autenticación y manejo de sesión de la plataforma.
    /// </summary>
    public interface IIdentityServices
    {
        /// <summary>
        /// Inicia una sesión de usuario en la plataforma.
        /// </summary>
        /// <param name="credentials">Credenciales para inicio de sesión de usuario.</param>
        Task<string> Login(UserCredentials credentials);

        /// <summary>
        /// Registra la modificación de la contraseña de un usuario.
        /// </summary>
        /// <param name="request">Clase con la información requerida para la modificación de la contraseña de un usuario.</param>
        Task<bool> ChangePassword(UserPasswordChangeRequest request);
    }

    /// <summary>
    /// Implementación de servicios de autenticación y manejo de sesión de la plataforma.
    /// </summary>
    public class IdentityServices : PlatformServicesBase, IIdentityServices
    {
        #region Constructores de la clase

        /// <summary>
        /// Inicializa una nueva instancia de la clase IdentityServices.
        /// </summary>
        /// <param name="auroraProxy">Instancia de la interface de comunicación a servicios Aurora.</param>
        public IdentityServices(IAuroraProxy auroraProxy)
            : base(auroraProxy, ApiRoutes.Identity) { }

        #endregion

        #region Implementación de la interface IIdentityServices

        async Task<bool> IIdentityServices.ChangePassword(UserPasswordChangeRequest request)
        {
            AddToRequestUri("changepassword");
            var response = await PerformRequest<UserPasswordChangeResponse, UserPasswordChangeRequest>(RestOperationType.Put, request);

            return response.IsSuccess;
        }

        async Task<string> IIdentityServices.Login(UserCredentials credentials)
        {
            AddToRequestUri("login");
            var response = await PerformRequest<IdentityAccess, UserCredentials>(RestOperationType.Post, credentials);

            return response.AccessToken;
        }

        #endregion
    }
}