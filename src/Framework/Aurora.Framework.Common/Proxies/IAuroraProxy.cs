using System.Threading.Tasks;

namespace Aurora.Framework.Proxies
{
    /// <summary>
    /// Interface de comunicación a servicios Aurora.
    /// </summary>
    public interface IAuroraProxy
    {
        /// <summary>
        /// Obtiene la Uri del servicio a invocar en el formato definido por el proxy de Aurora.
        /// </summary>
        /// <param name="serverName">Nombre del servidor proxy, por ejemplo www.contoso.com.</param>
        /// <param name="apiRoute">Ruta del API donde se enviarán los requerimientos.</param>
        /// <returns>Uri del servicio a invocar en el formato definido por el proxy de Aurora,
        /// por ejemplo http://<paramref name="serverName"/>/<paramref name="apiRoute"/>.</returns>
        string GetProxyRequestUri(string serverName, string apiRoute);

        /// <summary>
        /// Ejecuta la invocación de un servicio sin esperar una respuesta del mismo.
        /// </summary>
        /// <param name="requestUri">Uri del servicio a invocar.</param>
        /// <param name="operationType">Tipo de operación REST del servicio a invocar.</param>
        Task PerformRequest(string requestUri, RestOperationType operationType);

        /// <summary>
        /// Ejecuta la invocación de un servicio con un conjunto de parámetros de tipo <typeparamref name="TParams"/>
        /// sin esperar una respuesta del mismo.
        /// </summary>
        /// <typeparam name="TParams">El tipo de objeto que contiene los parámetros del servicio a invocar.</typeparam>
        /// <param name="requestUri">Uri del servicio a invocar.</param>
        /// <param name="operationType">Tipo de operación REST del servicio a invocar.</param>
        /// <param name="parameters">Parámetros del servicio invocado.</param>
        Task PerformRequest<TParams>(string requestUri, RestOperationType operationType, TParams parameters);

        /// <summary>
        /// Ejecuta la invocación de un servicio y devuelve la respuesta del servicio en una instancia de tipo <typeparamref name="T"/> especificada.
        /// </summary>
        /// <typeparam name="T">El tipo de objeto que se requiere deserializar en la respuesta del servicio.</typeparam>
        /// <param name="requestUri">Uri del servicio a invocar.</param>
        /// <param name="operationType">Tipo de operación REST del servicio a invocar.</param>
        /// <returns>Instancia de tipo <typeparamref name="T"/> especificada</returns>
        Task<T> PerformRequest<T>(string requestUri, RestOperationType operationType);

        /// <summary>
        /// Ejecuta la invocación de un servicio con un conjunto de parámetros de tipo <typeparamref name="TParams"/>
        /// y devuelve la respuesta del servicio en una instancia de tipo <typeparamref name="T"/> especificada.
        /// </summary>
        /// <typeparam name="T">El tipo de objeto que se requiere deserializar en la respuesta del servicio.</typeparam>
        /// <typeparam name="TParams">El tipo de objeto que contiene los parámetros del servicio a invocar.</typeparam>
        /// <param name="requestUri">Uri del servicio a invocar.</param>
        /// <param name="operationType">Tipo de operación REST que se ejecutará.</param>
        /// <param name="parameters">Parámetros del servicio invocado.</param>
        /// <returns>Instancia de tipo <typeparamref name="T"/> especificada</returns>
        Task<T> PerformRequest<T, TParams>(string requestUri, RestOperationType operationType, TParams parameters);
    }
}