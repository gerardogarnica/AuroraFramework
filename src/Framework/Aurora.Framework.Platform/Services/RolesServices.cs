using Aurora.Framework.Collections;
using Aurora.Framework.Platform.Security;
using Aurora.Framework.Proxies;
using System.Threading.Tasks;

namespace Aurora.Framework.Platform
{
    /// <summary>
    /// Administrador de servicios de roles de usuarios de la plataforma.
    /// </summary>
    public interface IRolesServices
    {
        /// <summary>
        /// Obtiene un rol de usuarios de acuerdo a su identificador único.
        /// </summary>
        /// <param name="roleId">Identificador único del rol de usuarios.</param>
        Task<RoleInfo> GetRole(int roleId);

        /// <summary>
        /// Obtiene la lista de roles de usuarios.
        /// </summary>
        /// <param name="viewRequest">Elemento con la información para obtener la lista en formato paginado.</param>
        /// <param name="repositoryId">Identificador único del repositorio.</param>
        /// <param name="onlyActives">Indica si solo se obtienen los roles de usuarios activos.</param>
        Task<PagedCollection<RoleInfo>> GetRoles(PagedViewRequest viewRequest, int repositoryId, bool onlyActives);

        /// <summary>
        /// Crea un nuevo registro de rol de usuarios.
        /// </summary>
        /// <param name="request">Clase con la información requerida para la creación del nuevo rol de usuarios.</param>
        Task<RoleInfo.Response> CreateRole(RoleInfo.CreateRequest request);

        /// <summary>
        /// Actualiza un registro de rol de usuarios.
        /// </summary>
        /// <param name="request">Clase con la información requerida para la actualización del rol de usuarios.</param>
        Task<RoleInfo.Response> UpdateRole(RoleInfo.UpdateRequest request);

        /// <summary>
        /// Activa un registro de rol de usuarios existente.
        /// </summary>
        /// <param name="roleId">Identificador único del rol de usuarios.</param>
        Task<RoleInfo.Response> ActivateRole(int roleId);

        /// <summary>
        /// Desactiva un registro de rol de usuarios existente.
        /// </summary>
        /// <param name="roleId">Identificador único del rol de usuarios.</param>
        Task<RoleInfo.Response> DeactivateRole(int roleId);

        /// <summary>
        /// Agrega o elimina un conjunto de usuarios a un rol.
        /// </summary>
        /// <param name="request">Clase con la información requerida para la asignación o eliminación de usuarios del rol.</param>
        Task<RoleInfo.Response> SaveUsers(RoleInfo.SaveUsersRequest request);
    }

    /// <summary>
    /// Implementación de servicios de roles de usuarios de la plataforma.
    /// </summary>
    public class RolesServices : PlatformServicesBase, IRolesServices
    {
        #region Constructores de la clase

        /// <summary>
        /// Inicializa una nueva instancia de la clase RolesServices.
        /// </summary>
        /// <param name="auroraProxy">Instancia de la interface de comunicación a servicios Aurora.</param>
        public RolesServices(IAuroraProxy auroraProxy)
            : base(auroraProxy, ApiRoutes.Roles) { }

        #endregion

        #region Implementación de la interface IRolesServices

        async Task<RoleInfo> IRolesServices.GetRole(int roleId)
        {
            AddToRequestUri(roleId.ToString());
            return await PerformRequest<RoleInfo>(RestOperationType.Get);
        }

        async Task<PagedCollection<RoleInfo>> IRolesServices.GetRoles(
            PagedViewRequest viewRequest, int repositoryId, bool onlyActives)
        {
            var requestParams = UriRequestBuilder
                .GetBuilder()
                .AddInteger("PageIndex", viewRequest.PageIndex)
                .AddInteger("PageSize", viewRequest.PageSize)
                .AddInteger("RepositoryId", repositoryId)
                .AddBoolean("OnlyActives", onlyActives)
                .ParametersUri;

            AddToRequestUri(requestParams);
            return await PerformRequest<PagedCollection<RoleInfo>>(RestOperationType.Get);
        }

        async Task<RoleInfo.Response> IRolesServices.CreateRole(RoleInfo.CreateRequest request)
        {
            AddToRequestUri("create");
            return await PerformRequest<RoleInfo.Response, RoleInfo.CreateRequest>(RestOperationType.Post, request);
        }

        async Task<RoleInfo.Response> IRolesServices.UpdateRole(RoleInfo.UpdateRequest request)
        {
            AddToRequestUri("update");
            return await PerformRequest<RoleInfo.Response, RoleInfo.UpdateRequest>(RestOperationType.Put, request);
        }

        async Task<RoleInfo.Response> IRolesServices.ActivateRole(int roleId)
        {
            AddToRequestUri(string.Format("activate/{0}", roleId));
            return await PerformRequest<RoleInfo.Response>(RestOperationType.Put);
        }

        async Task<RoleInfo.Response> IRolesServices.DeactivateRole(int roleId)
        {
            AddToRequestUri(string.Format("deactivate/{0}", roleId));
            return await PerformRequest<RoleInfo.Response>(RestOperationType.Put);
        }

        async Task<RoleInfo.Response> IRolesServices.SaveUsers(RoleInfo.SaveUsersRequest request)
        {
            AddToRequestUri("saveusers");
            return await PerformRequest<RoleInfo.Response, RoleInfo.SaveUsersRequest>(RestOperationType.Put, request);
        }

        #endregion
    }
}