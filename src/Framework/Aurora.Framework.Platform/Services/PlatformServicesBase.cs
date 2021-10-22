using Aurora.Framework.Exceptions;
using Aurora.Framework.Proxies;
using System;
using System.Threading.Tasks;

namespace Aurora.Framework.Platform
{
    /// <summary>
    /// Clase base para la implementación de acceso a servicios de Aurora Platform.
    /// </summary>
    public abstract class PlatformServicesBase
    {
        #region Constantes

        private const string cGatewayServerEnvName = "GATEWAY_SERVER";

        #endregion

        #region Miembros privados de la clase

        private readonly IAuroraProxy _auroraProxy;
        private string _requestUri;

        #endregion

        #region Constructores de la clase

        /// <summary>
        /// Inicializa una nueva instancia de la clase PlatformServicesBase.
        /// </summary>
        /// <param name="auroraProxy">Instancia de la interface de comunicación a servicios Aurora.</param>
        /// <param name="apiRoute">Ruta del API donde se enviarán los requerimientos.</param>
        public PlatformServicesBase(IAuroraProxy auroraProxy, string apiRoute)
        {
            _auroraProxy = auroraProxy ?? throw new ArgumentNullException(nameof(auroraProxy));

            if (string.IsNullOrWhiteSpace(apiRoute)) throw new ArgumentNullException(nameof(apiRoute));

            _requestUri = _auroraProxy.GetProxyRequestUri(GetProxyServerName(), apiRoute);
        }

        #endregion

        #region Métodos base de la clase

        internal void AddToRequestUri(string uriToAdd)
        {
            if (string.IsNullOrWhiteSpace(uriToAdd)) throw new ArgumentNullException(nameof(uriToAdd));

            _requestUri = string.Format("{0}/{1}", _requestUri, uriToAdd);
        }

        async internal Task PerformRequest(RestOperationType operationType)
        {
            await _auroraProxy.PerformRequest(_requestUri, operationType);
        }

        async internal Task PerformRequest<TParams>(RestOperationType operationType, TParams parameters)
        {
            await _auroraProxy.PerformRequest<TParams>(_requestUri, operationType, parameters);
        }

        async internal Task<T> PerformRequest<T>(RestOperationType operationType)
        {
            return await _auroraProxy.PerformRequest<T>(_requestUri, operationType);
        }

        async internal Task<T> PerformRequest<T, TParams>(RestOperationType operationType, TParams parameters)
        {
            return await _auroraProxy.PerformRequest<T, TParams>(_requestUri, operationType, parameters);
        }

        #endregion

        #region Métodos privados de la clase

        private string GetProxyServerName()
        {
            var serverName = Environment.GetEnvironmentVariable(cGatewayServerEnvName);
            if (string.IsNullOrWhiteSpace(serverName))
            {
                throw new PlatformException(ExceptionMessages.ProxyServerNameArgumentException);
            }

            return serverName;
        }

        #endregion
    }
}