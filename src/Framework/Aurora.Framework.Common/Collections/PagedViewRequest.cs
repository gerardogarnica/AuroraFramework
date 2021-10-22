using Newtonsoft.Json;

namespace Aurora.Framework.Collections
{
    /// <summary>
    /// Representa un requerimiento para obtener elementos de una colección en formato paginado.
    /// </summary>
    [JsonObject("pagedViewRequest")]
    public class PagedViewRequest
    {
        /// <summary>
        /// Número de página de los elementos a retornar.
        /// </summary>
        [JsonProperty("pageIndex")]
        public int PageIndex { get; set; }

        /// <summary>
        /// Número de elementos a retornar.
        /// </summary>
        [JsonProperty("pageSize")]
        public int PageSize { get; set; }
    }
}