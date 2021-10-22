using Aurora.Framework.Collections;
using Aurora.Framework.Platform.Locations;
using Aurora.Framework.Proxies;
using System.Threading.Tasks;

namespace Aurora.Framework.Platform
{
    /// <summary>
    /// Administrador de servicios de países de la plataforma.
    /// </summary>
    public interface ICountriesServices
    {
        /// <summary>
        /// Obtiene un registro de país con una lista de sus divisiones.
        /// </summary>
        /// <param name="countryId">Identificador único de país.</param>
        /// <returns>Registro de país con sus divisiones administrativas.</returns>
        Task<Country> GetCountry(short countryId);

        /// <summary>
        /// Obtiene la lista de países en formato paginado.
        /// </summary>
        /// <param name="viewRequest">Entidad con la información de números de página y elementos de la consulta.</param>
        /// <param name="onlyGetActives">Indica si solo se obtienen los países en estado activo.</param>
        /// <returns>Lista de países en formato paginado.</returns>
        Task<PagedCollection<Country>> GetCountries(PagedViewRequest viewRequest, bool onlyGetActives);

        /// <summary>
        /// Crea un nuevo registro de país con sus divisiones administrativas.
        /// </summary>
        /// <param name="request">Clase con la información requerida para la creación del nuevo país.</param>
        Task<Country.Response> CreateCountry(Country.CreateRequest request);

        /// <summary>
        /// Actualiza los datos de un registro de país.
        /// </summary>
        /// <param name="request">Clase con la información requerida para la actualización de un país existente.</param>
        Task<Country.Response> Update(Country.UpdateRequest request);

        /// <summary>
        /// Crea o actualiza un registro de división administrativa de país.
        /// </summary>
        /// <param name="request">Clase con la información requerida para la
        /// creación o actualización de una división administrativa de país.</param>
        Task<Country.Response> SaveDivision(CountryDivision.SaveRequest request);
    }

    /// <summary>
    /// Implementación de servicios de países de la plataforma.
    /// </summary>
    public class CountriesServices : PlatformServicesBase, ICountriesServices
    {
        #region Constructores de la clase

        /// <summary>
        /// Inicializa una nueva instancia de la clase CountriesServices.
        /// </summary>
        /// <param name="auroraProxy">Instancia de la interface de comunicación a servicios Aurora.</param>
        public CountriesServices(IAuroraProxy auroraProxy)
            : base(auroraProxy, ApiRoutes.Countries) { }

        #endregion

        #region Implementación de la interface ICountriesServices

        async Task<Country> ICountriesServices.GetCountry(short countryId)
        {
            AddToRequestUri(countryId.ToString());
            return await PerformRequest<Country>(RestOperationType.Get);
        }

        async Task<PagedCollection<Country>> ICountriesServices.GetCountries(
            PagedViewRequest viewRequest, bool onlyGetActives)
        {
            var requestParams = UriRequestBuilder
                .GetBuilder()
                .AddInteger("PageIndex", viewRequest.PageIndex)
                .AddInteger("PageSize", viewRequest.PageSize)
                .AddBoolean("OnlyGetActives", onlyGetActives)
                .ParametersUri;

            AddToRequestUri(requestParams);
            return await PerformRequest<PagedCollection<Country>>(RestOperationType.Get);
        }

        async Task<Country.Response> ICountriesServices.CreateCountry(Country.CreateRequest request)
        {
            AddToRequestUri("create");
            return await PerformRequest<Country.Response, Country.CreateRequest>(RestOperationType.Post, request);
        }

        async Task<Country.Response> ICountriesServices.Update(Country.UpdateRequest request)
        {
            AddToRequestUri("update");
            return await PerformRequest<Country.Response, Country.UpdateRequest>(RestOperationType.Put, request);
        }

        async Task<Country.Response> ICountriesServices.SaveDivision(CountryDivision.SaveRequest request)
        {
            AddToRequestUri("divisions/save");
            return await PerformRequest<Country.Response, CountryDivision.SaveRequest>(RestOperationType.Put, request);
        }

        #endregion
    }
}