using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Platform.Gateway
{
    public class Route
    {
        #region Propiedades de la clase

        public string ApiServerEnvName { get; set; }
        public string Path { get; set; }
        public bool RequiresAuthorization { get; set; }

        #endregion

        #region Miembros privados de la clase

        private static readonly HttpClient client = new HttpClient();

        #endregion

        #region Constructores de la clase

        public Route(string apiServerEnvName, string path, bool requiresAuthorization)
        {
            ApiServerEnvName = apiServerEnvName;
            Path = path;
            RequiresAuthorization = requiresAuthorization;
        }

        private Route()
        {
            ApiServerEnvName = "";
            Path = "/";
            RequiresAuthorization = false;
        }

        #endregion

        #region Métodos públicos de la clase

        public async Task<HttpResponseMessage> SendRequest(HttpRequest request)
        {
            string requestContent;

            using (var receiveStream = request.Body)
            {
                using (var readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    requestContent = await readStream.ReadToEndAsync();
                }
            }

            using (var newRequest = new HttpRequestMessage(new HttpMethod(request.Method), GetDestinationUri(request)))
            {
                if (RequiresAuthorization)
                {
                    if (request.Headers.ContainsKey("Authorization"))
                    {
                        var token = request.Headers["Authorization"].ToString();
                        if (!string.IsNullOrWhiteSpace(token))
                        {
                            newRequest.Headers.TryAddWithoutValidation("Authorization", token);
                        }
                    }
                }

                newRequest.Content = new StringContent(requestContent, Encoding.UTF8, request.ContentType);

                var response = await client.SendAsync(newRequest);
                return response;
            }
        }

        #endregion

        #region Métodos privados de la clase

        private string GetDestinationUri(HttpRequest request)
        {
            var apiServerName = Environment.GetEnvironmentVariable(ApiServerEnvName);
            var requestPath = request.Path.ToString();
            var queryString = request.QueryString.ToString();

            return string.Format("http://{0}{1}{2}", apiServerName, requestPath, queryString);
        }

        #endregion
    }
}