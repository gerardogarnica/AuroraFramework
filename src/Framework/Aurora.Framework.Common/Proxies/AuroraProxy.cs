using Aurora.Framework.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Framework.Proxies
{
    /// <summary>
    /// Implementación de procesos de comunicación a servicios Aurora.
    /// </summary>
    public class AuroraProxy : AuroraHttpClientBase, IAuroraProxy
    {
        #region Constructores de la clase

        /// <summary>
        /// Inicializa una nueva instancia de la clase AuroraProxy.
        /// </summary>
        /// <param name="httpClient">Clase base para envío de requerimientos HTTP y recepción de respuestas HTTP.</param>
        /// <param name="httpContextAccessor">Acceso a contextos HTTP.</param>
        public AuroraProxy(
            HttpClient httpClient,
            IHttpContextAccessor httpContextAccessor)
            : base(httpClient, httpContextAccessor) { }

        #endregion

        #region Implementación de interface IAuroraProxy

        string IAuroraProxy.GetProxyRequestUri(string serverName, string apiRoute)
        {
            return string.Format("http://{0}/{1}", serverName, apiRoute);
        }

        async Task IAuroraProxy.PerformRequest(string requestUri, RestOperationType operationType)
        {
            try
            {
                await GetResponse(requestUri, operationType, null);
            }
            catch (Exception e)
            {
                throw new PlatformException(GetMessageException(e));
            }
            finally
            {
                AuroraHttpClient.Dispose();
            }
        }

        async Task IAuroraProxy.PerformRequest<TParams>(string requestUri, RestOperationType operationType, TParams parameters)
        {
            try
            {
                var content = GetStringContent(parameters);
                await GetResponse(requestUri, operationType, content);
            }
            catch (Exception e)
            {
                throw new PlatformException(GetMessageException(e));
            }
            finally
            {
                AuroraHttpClient.Dispose();
            }
        }

        async Task<T> IAuroraProxy.PerformRequest<T>(string requestUri, RestOperationType operationType)
        {
            try
            {
                return await GetResponse<T>(requestUri, operationType, null);
            }
            catch (Exception e)
            {
                throw new PlatformException(GetMessageException(e));
            }
            finally
            {
                AuroraHttpClient.Dispose();
            }
        }

        async Task<T> IAuroraProxy.PerformRequest<T, TParams>(string requestUri, RestOperationType operationType, TParams parameters)
        {
            try
            {
                var content = GetStringContent(parameters);
                return await GetResponse<T>(requestUri, operationType, content);
            }
            catch (Exception e)
            {
                throw new PlatformException(GetMessageException(e));
            }
            finally
            {
                AuroraHttpClient.Dispose();
            }
        }

        #endregion

        #region Métodos privados de la clase

        private string GetMessageException(Exception e)
        {
            return string.Format(
                ExceptionMessages.ProxyGetResponseException,
                e.Source, e.ToString());
        }

        private async Task GetResponse(
            string requestUri, RestOperationType operationType, StringContent content)
        {
            await SendRequest(requestUri, operationType, content);
        }

        private async Task<T> GetResponse<T>(
            string requestUri, RestOperationType operationType, StringContent content)
        {
            var request = await SendRequest(requestUri, operationType, content);

            if (!request.IsSuccessStatusCode)
            {
                var message = string.Format(
                    ExceptionMessages.ProxyGetResponseException,
                    request.ReasonPhrase,
                    request.ToString());

                throw new PlatformException(message);
            }

            var response = await request.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(response);
        }

        private StringContent GetStringContent<TParams>(TParams parameters)
        {
            if (parameters == null) return null;

            return new StringContent(
                JsonConvert.SerializeObject(parameters),
                Encoding.UTF8,
                "application/json");
        }

        private async Task<HttpResponseMessage> SendRequest(
            string requestUri, RestOperationType operationType, StringContent content)
        {
            switch (operationType)
            {
                case RestOperationType.Get:
                    return await AuroraHttpClient.GetAsync(requestUri);

                case RestOperationType.Post:
                    return await AuroraHttpClient.PostAsync(requestUri, content);

                case RestOperationType.Put:
                    return await AuroraHttpClient.PutAsync(requestUri, content);

                case RestOperationType.Delete:
                    return await AuroraHttpClient.DeleteAsync(requestUri);

                case RestOperationType.Patch:
                    return await AuroraHttpClient.PatchAsync(requestUri, content);

                default: return null;
            }
        }

        #endregion
    }
}