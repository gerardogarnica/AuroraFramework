using Aurora.Framework.Platform.Applications;
using Aurora.Framework.Proxies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aurora.Framework.Platform
{
    /// <summary>
    /// Administrador de servicios de componentes de aplicaciones de la plataforma.
    /// </summary>
    public interface IComponentsServices
    {
        /// <summary>
        /// Obtiene un registro de componente de la plataforma de acuerdo a su código.
        /// </summary>
        /// <param name="applicationId">Identificador único de la aplicación de la plataforma.</param>
        /// <param name="code">Código del componente de la aplicación.</param>
        Task<ComponentInfo> GetComponent(short applicationId, string code);

        /// <summary>
        /// Obtiene la lista de componentes de una aplicación de la plataforma.
        /// </summary>
        /// <param name="applicationId">Identificador único de la aplicación de la plataforma.</param>
        Task<IList<ComponentInfo>> GetComponents(short applicationId);

        /// <summary>
        /// Crea un nuevo registro de componente de una aplicación de la plataforma.
        /// </summary>
        /// <param name="request">Clase con la información requerida para la creación de un nuevo componente.</param>
        Task<ComponentInfo.Response> CreateComponent(ComponentInfo.CreateRequest request);
    }

    /// <summary>
    /// Implementación de servicios de componentes de aplicaciones de la plataforma.
    /// </summary>
    public class ComponentsServices : PlatformServicesBase, IComponentsServices
    {
        #region Constructores de la clase

        /// <summary>
        /// Inicializa una nueva instancia de la clase ComponentsServices.
        /// </summary>
        /// <param name="auroraProxy">Instancia de la interface de comunicación a servicios Aurora.</param>
        public ComponentsServices(IAuroraProxy auroraProxy)
            : base(auroraProxy, ApiRoutes.Components) { }

        #endregion

        #region Implementación de la interface IComponentsServices

        async Task<ComponentInfo> IComponentsServices.GetComponent(short applicationId, string code)
        {
            AddToRequestUri(string.Format("{0},{1}", applicationId, code));
            return await PerformRequest<ComponentInfo>(RestOperationType.Get);
        }

        async Task<IList<ComponentInfo>> IComponentsServices.GetComponents(short applicationId)
        {
            AddToRequestUri(applicationId.ToString());
            return await PerformRequest<IList<ComponentInfo>>(RestOperationType.Get);
        }

        async Task<ComponentInfo.Response> IComponentsServices.CreateComponent(ComponentInfo.CreateRequest request)
        {
            AddToRequestUri("create");
            return await PerformRequest<ComponentInfo.Response, ComponentInfo.CreateRequest>(RestOperationType.Post, request);
        }

        #endregion
    }
}