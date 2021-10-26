using Aurora.Framework.Serialization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Aurora.Platform.Gateway
{
    public class AuroraRouter
    {
        public IList<Route> Routes { get; set; }

        public AuroraRouter(string routesConfigFilePath)
        {
            var routesFileContent = JsonSerializer.LoadFromFile<dynamic>(routesConfigFilePath);

            Routes = JsonSerializer.Deserialize<List<Route>>(Convert.ToString(routesFileContent.routes));
        }

        public async Task<HttpResponseMessage> RouteRequest(HttpRequest request)
        {
            var requestPath = request.Path.ToString();

            try
            {
                var route = Routes.First(x => requestPath.Contains(x.Path));
                return await route.SendRequest(request);
            }
            catch
            {
                return GetMessageError("No se encuentra configurada la ruta requerida.");
            }
        }

        private HttpResponseMessage GetMessageError(string message)
        {
            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent(message)
            };
        }
    }
}