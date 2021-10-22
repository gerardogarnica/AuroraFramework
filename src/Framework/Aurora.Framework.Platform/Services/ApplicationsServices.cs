using Aurora.Framework.Platform.Applications;
using Aurora.Framework.Proxies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aurora.Framework.Platform
{
    /// <summary>
    /// Administrador de servicios de aplicaciones de la plataforma.
    /// </summary>
    public interface IApplicationsServices
    {
        /// <summary>
        /// Obtiene una aplicación de la plataforma de acuerdo a su código.
        /// </summary>
        /// <param name="code">Código de la aplicación de la plataforma.</param>
        Task<ApplicationInfo> GetApplication(string code);

        /// <summary>
        /// Obtiene la lista de aplicaciones de la plataforma.
        /// </summary>
        Task<IList<ApplicationInfo>> GetApplications();

        /// <summary>
        /// Crea un nuevo registro de aplicación de la plataforma.
        /// </summary>
        /// <param name="request">Clase con la información requerida para la creación de una nueva aplicación.</param>
        Task<ApplicationInfo.Response> CreateApplication(ApplicationInfo.CreateRequest request);
    }

    /// <summary>
    /// Implementación de servicios de aplicaciones de la plataforma.
    /// </summary>
    public class ApplicationsServices : PlatformServicesBase, IApplicationsServices
    {
        #region Constructores de la clase

        /// <summary>
        /// Inicializa una nueva instancia de la clase ApplicationsServices.
        /// </summary>
        /// <param name="auroraProxy">Instancia de la interface de comunicación a servicios Aurora.</param>
        public ApplicationsServices(IAuroraProxy auroraProxy)
            : base(auroraProxy, ApiRoutes.Applications) { }

        #endregion

        #region Implementación de la interface IApplicationsServices

        async Task<ApplicationInfo> IApplicationsServices.GetApplication(string code)
        {
            AddToRequestUri(code);
            return await PerformRequest<ApplicationInfo>(RestOperationType.Get);
        }

        async Task<IList<ApplicationInfo>> IApplicationsServices.GetApplications()
        {
            return await PerformRequest<IList<ApplicationInfo>>(RestOperationType.Get);
        }

        async Task<ApplicationInfo.Response> IApplicationsServices.CreateApplication(ApplicationInfo.CreateRequest request)
        {
            AddToRequestUri("create");
            return await PerformRequest<ApplicationInfo.Response, ApplicationInfo.CreateRequest>(RestOperationType.Post, request);
        }

        #endregion
    }
}