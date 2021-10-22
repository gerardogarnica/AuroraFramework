using Aurora.Framework.Collections;
using Aurora.Framework.Platform.Security;
using Aurora.Framework.Proxies;
using System.Threading.Tasks;

namespace Aurora.Framework.Platform
{
    /// <summary>
    /// Administrador de servicios de usuarios de la plataforma.
    /// </summary>
    public interface IUsersServices
    {
        /// <summary>
        /// Obtiene un usuario de acuerdo a su nombre de inicio de sesión.
        /// </summary>
        /// <param name="loginName">Nombre de inicio de sesión de usuario.</param>
        Task<UserInfo> GetUser(string loginName);

        /// <summary>
        /// Obtiene una lista de usuarios registrados.
        /// </summary>
        /// <param name="viewRequest">Elemento con la información para obtener la lista en formato paginado.</param>
        /// <param name="onlyActives">Indica si solo se obtienen los usuarios activos.</param>
        Task<PagedCollection<UserInfo>> GetUsers(PagedViewRequest viewRequest, bool onlyActives);

        /// <summary>
        /// Crea un nuevo registro de usuario.
        /// </summary>
        /// <param name="request">Clase con la información requerida para la creación del nuevo usuario.</param>
        Task<UserInfo.Response> CreateUser(UserInfo.CreateRequest request);

        /// <summary>
        /// Activa un registro de usuario existente.
        /// </summary>
        /// <param name="loginName">Nombre de inicio de sesión de usuario.</param>
        Task<UserInfo.Response> ActivateUser(string loginName);

        /// <summary>
        /// Desactiva un registro de usuario existente.
        /// </summary>
        /// <param name="loginName">Nombre de inicio de sesión de usuario.</param>
        Task<UserInfo.Response> DeactivateUser(string loginName);

        /// <summary>
        /// Agrega o elimina un conjunto de roles a un usuario.
        /// </summary>
        /// <param name="request">Clase con la información requerida para la asignación o eliminación de roles del usuario.</param>
        Task<UserInfo.Response> SaveRoles(UserInfo.SaveRolesRequest request);
    }

    /// <summary>
    /// Implementación de servicios de usuarios de la plataforma.
    /// </summary>
    public class UsersServices : PlatformServicesBase, IUsersServices
    {
        #region Constructores de la clase

        /// <summary>
        /// Inicializa una nueva instancia de la clase UsersServices.
        /// </summary>
        /// <param name="auroraProxy">Instancia de la interface de comunicación a servicios Aurora.</param>
        public UsersServices(IAuroraProxy auroraProxy)
            : base(auroraProxy, ApiRoutes.Users) { }

        #endregion

        #region Implementación de la interface IUsersServices

        async Task<UserInfo> IUsersServices.GetUser(string loginName)
        {
            AddToRequestUri(loginName);
            return await PerformRequest<UserInfo>(RestOperationType.Get);
        }

        async Task<PagedCollection<UserInfo>> IUsersServices.GetUsers(PagedViewRequest viewRequest, bool onlyActives)
        {
            var requestParams = UriRequestBuilder
                .GetBuilder()
                .AddInteger("PageIndex", viewRequest.PageIndex)
                .AddInteger("PageSize", viewRequest.PageSize)
                .AddBoolean("OnlyActives", onlyActives)
                .ParametersUri;

            AddToRequestUri(requestParams);
            return await PerformRequest<PagedCollection<UserInfo>>(RestOperationType.Get);
        }

        async Task<UserInfo.Response> IUsersServices.CreateUser(UserInfo.CreateRequest request)
        {
            AddToRequestUri("create");
            return await PerformRequest<UserInfo.Response, UserInfo.CreateRequest>(RestOperationType.Post, request);
        }

        async Task<UserInfo.Response> IUsersServices.ActivateUser(string loginName)
        {
            AddToRequestUri(string.Format("activate/{0}", loginName));
            return await PerformRequest<UserInfo.Response>(RestOperationType.Put);
        }

        async Task<UserInfo.Response> IUsersServices.DeactivateUser(string loginName)
        {
            AddToRequestUri(string.Format("deactivate/{0}", loginName));
            return await PerformRequest<UserInfo.Response>(RestOperationType.Put);
        }

        async Task<UserInfo.Response> IUsersServices.SaveRoles(UserInfo.SaveRolesRequest request)
        {
            AddToRequestUri("saveroles");
            return await PerformRequest<UserInfo.Response, UserInfo.SaveRolesRequest>(RestOperationType.Put, request);
        }

        #endregion
    }
}