using Aurora.Framework.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Aurora.Framework.Services
{
    /// <summary>
    /// Define un middleware para el manejo de excepciones.
    /// </summary>
    public class ExceptionHandlerMiddleware : IMiddleware
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase ExceptionHandlerMiddleware.
        /// </summary>
        // <param name="next">Función para procesar un requerimiento HTTP.</param>
        public ExceptionHandlerMiddleware() { }

        /// <summary>
        /// Método para el manejo de requerimientos.
        /// </summary>
        /// <param name="context">Contexto HttpContext para el requerimiento actual.</param>
        /// <param name="next">Función para procesar un requerimiento HTTP.</param>
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception e)
        {
            AuroraBaseResponse responseModel;
            var statusCode = StatusCodes.Status500InternalServerError;

            switch (e)
            {
                case BusinessException ex:
                    statusCode = StatusCodes.Status400BadRequest;
                    responseModel = new AuroraBaseResponse(
                        ex.ErrorCategory, ex.ErrorType, ex.Message);

                    break;

                case PlatformException ex:
                    responseModel = new AuroraBaseResponse("PlatformException", ex.Message);
                    break;

                default:
                    responseModel = new AuroraBaseResponse(e.GetType().ToString(), e.Message);

                    if (e.InnerException != null)
                    {
                        responseModel.Errors.Add(new AuroraBaseResponse.ResponseError()
                        {
                            Category = "System.Exception",
                            ErrorType = e.InnerException.GetType().ToString(),
                            Message = e.InnerException.Message
                        });
                    }

                    break;
            }

            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsJsonAsync(responseModel);
        }
    }
}