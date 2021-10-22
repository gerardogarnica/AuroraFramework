using Aurora.Framework.Collections;
using Aurora.Framework.Platform.Settings;
using Aurora.Framework.Proxies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aurora.Framework.Platform
{
    /// <summary>
    /// Administrador de servicios de configuraciones comunes de la plataforma.
    /// </summary>
    public interface ISettingsServices
    {
        /// <summary>
        /// Obtiene un registro de configuración de atributos de parametrización.
        /// </summary>
        /// <param name="code">Código de la configuración de atributo de parametrización.</param>
        /// <returns>Registro de configuración de atributo de parametrización.</returns>
        Task<AttributeSetting> GetSetting(string code);

        /// <summary>
        /// Obtiene la lista de configuraciones de atributos de parametrización en formato paginado.
        /// </summary>
        /// <param name="viewRequest">Entidad con la información de números de página y elementos de la consulta.</param>
        /// <param name="scopeType">Tipo de configuración de atributos de parametrización.</param>
        /// <param name="onlyGetActives">Indica si solo se obtienen las configuraciones de atributos de parametrización en estado activo.</param>
        /// <returns>Lista de configuraciones de atributos de parametrización en formato paginado.</returns>
        Task<PagedCollection<AttributeSetting>> GetSettings(PagedViewRequest viewRequest, string scopeType, bool onlyGetActives);

        /// <summary>
        /// Crea un nuevo registro de configuración de atributo de parametrización.
        /// </summary>
        /// <param name="request">Clase con la información requerida para la creación
        /// de la nueva configuración de atributo de parametrización.</param>
        Task<AttributeSetting.Response> CreateSetting(AttributeSetting.CreateRequest request);

        /// <summary>
        /// Obtiene un registro de valor de atributo de parametrización.
        /// </summary>
        /// <param name="code">Código del de atributo de parametrización.</param>
        /// <returns>Registro de valor de atributo de parametrización.</returns>
        Task<AttributeValue> GetValue(string code);

        /// <summary>
        /// Obtiene un registro de valor de atributo de parametrización.
        /// </summary>
        /// <param name="code">Código del de atributo de parametrización.</param>
        /// <param name="relationshipId">Identificador del registro de relación.</param>
        /// <returns>Registro de valor de atributo de parametrización.</returns>
        Task<AttributeValue> GetValue(string code, int relationshipId);

        /// <summary>
        /// Obtiene la lista de valores de atributos de parametrización de un tipo de configuración determinados.
        /// </summary>
        /// <param name="scopeType">Tipo de configuración de atributos de parametrización.</param>
        /// <returns>Lista de valores de atributos de parametrización de un tipo de configuración y un registro de relación.</returns>
        Task<IList<AttributeValue>> GetValues(string scopeType);

        /// <summary>
        /// Obtiene la lista de valores de atributos de parametrización de un tipo de configuración y un registro de relación determinados.
        /// </summary>
        /// <param name="scopeType">Tipo de configuración de atributos de parametrización.</param>
        /// <param name="relationshipId">Identificador del registro de relación.</param>
        /// <returns>Lista de valores de atributos de parametrización de un tipo de configuración y un registro de relación.</returns>
        Task<IList<AttributeValue>> GetValues(string scopeType, int relationshipId);

        /// <summary>
        /// Almacena un registro de valor de atributo de parametrización.
        /// </summary>
        /// <param name="request">Clase con la información requerida para el almacenamiento
        /// del valor de atributo de parametrización.</param>
        Task<AttributeValue.Response> SaveValue(AttributeValue.SaveRequest request);
    }

    /// <summary>
    /// Implementación de servicios de configuraciones comunes de la plataforma.
    /// </summary>
    public class SettingsServices : PlatformServicesBase, ISettingsServices
    {
        #region Constructores de la clase

        /// <summary>
        /// Inicializa una nueva instancia de la clase SettingsServices.
        /// </summary>
        /// <param name="auroraProxy">Instancia de la interface de comunicación a servicios Aurora.</param>
        public SettingsServices(IAuroraProxy auroraProxy)
            : base(auroraProxy, ApiRoutes.Settings) { }

        #endregion

        #region Implementación de la interface ISettingsServices

        async Task<AttributeSetting> ISettingsServices.GetSetting(string code)
        {
            AddToRequestUri(string.Format("configs/{0}", code));
            return await PerformRequest<AttributeSetting>(RestOperationType.Get);
        }

        async Task<PagedCollection<AttributeSetting>> ISettingsServices.GetSettings(
            PagedViewRequest viewRequest, string scopeType, bool onlyGetActives)
        {
            var requestParams = UriRequestBuilder
                .GetBuilder("configs")
                .AddInteger("PageIndex", viewRequest.PageIndex)
                .AddInteger("PageSize", viewRequest.PageSize)
                .AddString("ScopeType", scopeType)
                .AddBoolean("OnlyGetActives", onlyGetActives)
                .ParametersUri;

            AddToRequestUri(requestParams);
            return await PerformRequest<PagedCollection<AttributeSetting>>(RestOperationType.Get);
        }

        async Task<AttributeSetting.Response> ISettingsServices.CreateSetting(AttributeSetting.CreateRequest request)
        {
            AddToRequestUri("configs/create");
            return await PerformRequest<AttributeSetting.Response, AttributeSetting.CreateRequest>(RestOperationType.Post, request);
        }

        async Task<AttributeValue> ISettingsServices.GetValue(string code)
        {
            var requestParams = UriRequestBuilder
                .GetBuilder(string.Format("values/{0}", code))
                .AddInteger("RelationshipId", 0)
                .ParametersUri;

            AddToRequestUri(requestParams);
            return await PerformRequest<AttributeValue>(RestOperationType.Get);
        }

        async Task<AttributeValue> ISettingsServices.GetValue(string code, int relationshipId)
        {
            var requestParams = UriRequestBuilder
                .GetBuilder(string.Format("values/{0}", code))
                .AddInteger("RelationshipId", relationshipId)
                .ParametersUri;

            AddToRequestUri(requestParams);
            return await PerformRequest<AttributeValue>(RestOperationType.Get);
        }

        async Task<IList<AttributeValue>> ISettingsServices.GetValues(string scopeType)
        {
            AddToRequestUri(string.Format("values/{0}/0", scopeType));
            return await PerformRequest<IList<AttributeValue>>(RestOperationType.Get);
        }

        async Task<IList<AttributeValue>> ISettingsServices.GetValues(string scopeType, int relationshipId)
        {
            AddToRequestUri(string.Format("values/{0}/{1}", scopeType, relationshipId));
            return await PerformRequest<IList<AttributeValue>>(RestOperationType.Get);
        }

        async Task<AttributeValue.Response> ISettingsServices.SaveValue(AttributeValue.SaveRequest request)
        {
            AddToRequestUri("values/save");
            return await PerformRequest<AttributeValue.Response, AttributeValue.SaveRequest>(RestOperationType.Post, request);
        }

        #endregion
    }
}