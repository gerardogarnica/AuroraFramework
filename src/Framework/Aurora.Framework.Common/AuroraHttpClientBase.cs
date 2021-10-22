using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Aurora.Framework
{
    /// <summary>
    /// Clase base para la configuración de clientes HTTP.
    /// </summary>
    public abstract class AuroraHttpClientBase
    {
        #region Constantes

        private const string cContentType = "application/json";

        #endregion

        #region Miembros protegidos de la clase

        /// <summary>
        /// Cliente HTTP para enviar requerimientos y recibir respuestas de un recurso identificado por una URI.
        /// </summary>
        protected readonly HttpClient AuroraHttpClient;

        #endregion

        #region Constructores de la clase

        /// <summary>
        /// Inicializa una nueva instancia de la clase AuroraHttpClientBase.
        /// </summary>
        /// <param name="httpClient">Clase base para envío de requerimientos y recepción de respuestas HTTP.</param>
        /// <param name="httpContextAccessor">Acceso a contextos HTTP.</param>
        public AuroraHttpClientBase(
            HttpClient httpClient,
            IHttpContextAccessor httpContextAccessor)
        {
            AuroraHttpClient = httpClient;
            ConfigureHttpClient(httpContextAccessor);
        }

        #endregion

        #region Métodos privados de la clase

        private void ConfigureHttpClient(IHttpContextAccessor context)
        {
            AuroraHttpClient.DefaultRequestHeaders.Clear();
            AuroraHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(cContentType));

            if (context.HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                var token = context.HttpContext.Request.Headers["Authorization"].ToString();

                if (!string.IsNullOrWhiteSpace(token))
                {
                    AuroraHttpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token);
                }

                return;
            }

            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var token = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("access_token"))?.Value;

                if (!string.IsNullOrEmpty(token))
                {
                    AuroraHttpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + token);
                }
            }
        }

        #endregion
    }
}