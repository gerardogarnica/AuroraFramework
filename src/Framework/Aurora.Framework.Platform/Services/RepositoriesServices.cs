using Aurora.Framework.Platform.Applications;
using Aurora.Framework.Proxies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aurora.Framework.Platform
{
    /// <summary>
    /// Administrador de servicios de repositorios de aplicaciones de la plataforma.
    /// </summary>
    public interface IRepositoriesServices
    {
        /// <summary>
        /// Obtiene un registro de repositorio de la plataforma de acuerdo a su código.
        /// </summary>
        /// <param name="applicationId">Identificador único de la aplicación de la plataforma.</param>
        /// <param name="code">Código del repositorio de la aplicación.</param>
        Task<RepositoryInfo> GetRepository(short applicationId, string code);

        /// <summary>
        /// Obtiene la lista de repositorios de una aplicación de la plataforma.
        /// </summary>
        /// <param name="applicationId">Identificador único de la aplicación de la plataforma.</param>
        Task<IList<RepositoryInfo>> GetRepositories(short applicationId);

        /// <summary>
        /// Crea un nuevo registro de repositorio de la plataforma.
        /// </summary>
        /// <param name="request">Clase con la información requerida para la creación de un nuevo repositorio.</param>
        Task<RepositoryInfo.Response> CreateRepository(RepositoryInfo.CreateRequest request);

        /// <summary>
        /// Almacena un registro de detalle de conexión de un repositorio.
        /// </summary>
        /// <param name="request">Clase con la información requerida para la creación de un nuevo detalle de conexión.</param>
        Task<RepositoryInfo.Response> SaveDetail(RepositoryInfo.DetailCreateRequest request);
    }

    /// <summary>
    /// Implementación de servicios de repositorios de aplicaciones de la plataforma.
    /// </summary>
    public class RepositoriesServices : PlatformServicesBase, IRepositoriesServices
    {
        #region Constructores de la clase

        /// <summary>
        /// Inicializa una nueva instancia de la clase RepositoriesServices.
        /// </summary>
        /// <param name="auroraProxy">Instancia de la interface de comunicación a servicios Aurora.</param>
        public RepositoriesServices(IAuroraProxy auroraProxy)
            : base(auroraProxy, ApiRoutes.Repositories) { }

        #endregion

        #region Implementación de la interface IRepositoriesServices

        async Task<RepositoryInfo> IRepositoriesServices.GetRepository(short applicationId, string code)
        {
            AddToRequestUri(string.Format("{0},{1}", applicationId, code));
            return await PerformRequest<RepositoryInfo>(RestOperationType.Get);
        }

        async Task<IList<RepositoryInfo>> IRepositoriesServices.GetRepositories(short applicationId)
        {
            AddToRequestUri(applicationId.ToString());
            return await PerformRequest<IList<RepositoryInfo>>(RestOperationType.Get);
        }

        async Task<RepositoryInfo.Response> IRepositoriesServices.CreateRepository(RepositoryInfo.CreateRequest request)
        {
            AddToRequestUri("create");
            return await PerformRequest<RepositoryInfo.Response, RepositoryInfo.CreateRequest>(RestOperationType.Post, request);
        }

        async Task<RepositoryInfo.Response> IRepositoriesServices.SaveDetail(RepositoryInfo.DetailCreateRequest request)
        {
            AddToRequestUri("savedetail");
            return await PerformRequest<RepositoryInfo.Response, RepositoryInfo.DetailCreateRequest>(RestOperationType.Put, request);
        }

        #endregion
    }
}