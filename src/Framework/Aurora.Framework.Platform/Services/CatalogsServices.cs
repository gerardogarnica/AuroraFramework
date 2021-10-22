using Aurora.Framework.Collections;
using Aurora.Framework.Platform.Catalogs;
using Aurora.Framework.Proxies;
using System.Threading.Tasks;

namespace Aurora.Framework.Platform
{
    /// <summary>
    /// Administrador de servicios de catálogos de la plataforma.
    /// </summary>
    public interface ICatalogsServices
    {
        /// <summary>
        /// Obtiene un catálogo con sus elementos de acuerdo a su código.
        /// </summary>
        /// <param name="code">Código de catálogo.</param>
        /// <param name="onlyGetActiveItems">Indica si solo se obtienen los elementos activos del catálogo.</param>
        /// <returns>Registro de catálogo con sus elementos.</returns>
        Task<Catalog> GetCatalog(string code, bool onlyGetActiveItems);

        /// <summary>
        /// Obtiene la lista de catálogos en formato paginado.
        /// </summary>
        /// <param name="viewRequest">Entidad con la información de números de página y elementos de la consulta.</param>
        /// <param name="onlyGetVisibles">Indica si solo se obtienen los catálogos en estado visible.</param>
        /// <param name="onlyGetEditables">Indica si solo se obtienen los catálogos que son editables.</param>
        /// <returns>Lista de catálogos en formato paginado.</returns>
        Task<PagedCollection<Catalog>> GetCatalogs(PagedViewRequest viewRequest, bool onlyGetVisibles, bool onlyGetEditables);

        /// <summary>
        /// Crea un nuevo registro de catálogo con sus elementos.
        /// </summary>
        /// <param name="request">Clase con la información requerida para la creación del nuevo catálogo.</param>
        Task<Catalog.Response> CreateCatalog(Catalog.CreateRequest request);

        /// <summary>
        /// Actualiza un registro de catálogo.
        /// </summary>
        /// <param name="request">Clase con la información requerida para la actualización de un catálogo existente.</param>
        Task<Catalog.Response> UpdateCatalog(Catalog.UpdateRequest request);

        /// <summary>
        /// Crea o actualiza un registro de elemento de catálogo.
        /// </summary>
        /// <param name="request">Clase con la información requerida para la creación o actualización de un elemento de catálogo.</param>
        Task<Catalog.Response> SaveItem(CatalogItem.SaveRequest request);
    }

    /// <summary>
    /// Implementación de servicios de catálogos de la plataforma.
    /// </summary>
    public class CatalogsServices : PlatformServicesBase, ICatalogsServices
    {
        #region Constructores de la clase

        /// <summary>
        /// Inicializa una nueva instancia de la clase CatalogsServices.
        /// </summary>
        /// <param name="auroraProxy">Instancia de la interface de comunicación a servicios Aurora.</param>
        public CatalogsServices(IAuroraProxy auroraProxy)
            : base(auroraProxy, ApiRoutes.Catalogs) { }

        #endregion

        #region Implementación de la interface ICatalogsServices

        async Task<Catalog> ICatalogsServices.GetCatalog(string code, bool onlyGetActiveItems)
        {
            var requestParams = UriRequestBuilder
                .GetBuilder(code)
                .AddBoolean("OnlyGetActiveItems", onlyGetActiveItems)
                .ParametersUri;

            AddToRequestUri(requestParams);
            return await PerformRequest<Catalog>(RestOperationType.Get);
        }

        async Task<PagedCollection<Catalog>> ICatalogsServices.GetCatalogs(
            PagedViewRequest viewRequest, bool onlyGetVisibles, bool onlyGetEditables)
        {
            var requestParams = UriRequestBuilder
                .GetBuilder()
                .AddInteger("PageIndex", viewRequest.PageIndex)
                .AddInteger("PageSize", viewRequest.PageSize)
                .AddBoolean("OnlyGetVisibles", onlyGetVisibles)
                .AddBoolean("OnlyGetEditables", onlyGetEditables)
                .ParametersUri;

            AddToRequestUri(requestParams);
            return await PerformRequest<PagedCollection<Catalog>>(RestOperationType.Get);
        }

        async Task<Catalog.Response> ICatalogsServices.CreateCatalog(Catalog.CreateRequest request)
        {
            AddToRequestUri("create");
            return await PerformRequest<Catalog.Response, Catalog.CreateRequest>(RestOperationType.Post, request);
        }

        async Task<Catalog.Response> ICatalogsServices.UpdateCatalog(Catalog.UpdateRequest request)
        {
            AddToRequestUri("update");
            return await PerformRequest<Catalog.Response, Catalog.UpdateRequest>(RestOperationType.Put, request);
        }

        async Task<Catalog.Response> ICatalogsServices.SaveItem(CatalogItem.SaveRequest request)
        {
            AddToRequestUri("items/save");
            return await PerformRequest<Catalog.Response, CatalogItem.SaveRequest>(RestOperationType.Put, request);
        }

        #endregion
    }
}