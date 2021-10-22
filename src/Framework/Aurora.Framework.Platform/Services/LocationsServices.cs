using Aurora.Framework.Platform.Locations;
using Aurora.Framework.Proxies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aurora.Framework.Platform
{
    /// <summary>
    /// Administrador de servicios de localidades de la plataforma.
    /// </summary>
    public interface ILocationsServices
    {
        /// <summary>
        /// Obtiene un registro de localidad con sus subdivisiones en caso de requerirlo.
        /// </summary>
        /// <param name="locationId">Identificador único de localidad.</param>
        /// <param name="getNextLevel">Indica si se obtienen las subdivisiones administrativas del siguiente nivel.</param>
        /// <param name="onlyGetActivesChilds">Indica si solo se obtienen las subdivisiones administrativas en estado activo.</param>
        /// <returns>Registro de localidad con sus subdivisiones.</returns>
        Task<Location> GetLocation(int locationId, bool getNextLevel, bool onlyGetActivesChilds);

        /// <summary>
        /// Obtiene la lista de localidades de una división administrativa.
        /// </summary>
        /// <param name="parentId">ID de la división administrativa de la que se van a obtener las localidades.</param>
        /// <param name="onlyGetActives">Indica si solo se obtienen las localidades en estado activo.</param>
        /// <returns>Lista de localidades de una división administrativa.</returns>
        Task<IList<Location>> GetLocations(int parentId, bool onlyGetActives);

        /// <summary>
        /// Crea un nuevo registro de localidad.
        /// </summary>
        /// <param name="request">Clase con la información requerida para la creación de la nueva localidad.</param>
        Task<Location.Response> CreateLocation(Location.CreateRequest request);

        /// <summary>
        /// Actualiza un registro de localidad.
        /// </summary>
        /// <param name="request">Clase con la información requerida para la actualización de una localidad existente.</param>
        Task<Location.Response> UpdateLocation(Location.UpdateRequest request);
    }

    /// <summary>
    /// Implementación de servicios de localidades de la plataforma.
    /// </summary>
    public class LocationsServices : PlatformServicesBase, ILocationsServices
    {
        #region Constructores de la clase

        /// <summary>
        /// Inicializa una nueva instancia de la clase LocationsServices.
        /// </summary>
        /// <param name="auroraProxy">Instancia de la interface de comunicación a servicios Aurora.</param>
        public LocationsServices(IAuroraProxy auroraProxy)
            : base(auroraProxy, ApiRoutes.Locations) { }

        #endregion

        #region Implementación de la interface ILocationsServices

        async Task<Location> ILocationsServices.GetLocation(
            int locationId, bool getNextLevel, bool onlyGetActivesChilds)
        {
            var requestParams = UriRequestBuilder
                .GetBuilder(locationId.ToString())
                .AddBoolean("GetNextLevel", getNextLevel)
                .AddBoolean("OnlyGetActivesChilds", onlyGetActivesChilds)
                .ParametersUri;

            AddToRequestUri(requestParams);
            return await PerformRequest<Location>(RestOperationType.Get);
        }

        async Task<IList<Location>> ILocationsServices.GetLocations(
            int parentId, bool onlyGetActives)
        {
            var requestParams = UriRequestBuilder
                .GetBuilder()
                .AddInteger("ParentId", parentId)
                .AddBoolean("OnlyGetActives", onlyGetActives)
                .ParametersUri;

            AddToRequestUri(requestParams);
            return await PerformRequest<IList<Location>>(RestOperationType.Get);
        }

        async Task<Location.Response> ILocationsServices.CreateLocation(Location.CreateRequest request)
        {
            AddToRequestUri("create");
            return await PerformRequest<Location.Response, Location.CreateRequest>(RestOperationType.Post, request);
        }

        async Task<Location.Response> ILocationsServices.UpdateLocation(Location.UpdateRequest request)
        {
            AddToRequestUri("update");
            return await PerformRequest<Location.Response, Location.UpdateRequest>(RestOperationType.Put, request);
        }

        #endregion
    }
}