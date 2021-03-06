using Aurora.Framework.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Aurora.Framework.Services
{
    /// <summary>
    /// Define un middleware para el manejo de excepciones.
    /// </summary>
    public class ExceptionHandlerMiddleware : IMiddleware
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Inicializa una nueva instancia de la clase ExceptionHandlerMiddleware.
        /// </summary>
        /// <param name="logger">Interface para manejo de registro de logs.</param>
        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

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
            catch (BusinessException e)
            {
                var response = new ErrorDetailResponse(
                    StatusCodes.Status400BadRequest,
                    ErrorDetailCategory.BusinessValidation);

                response.AddErrorMessage(e.ErrorType, e.Message);

                await HandleExceptionAsync(context, response);
            }
            catch (ValidationException e)
            {
                var response = new ErrorDetailResponse(
                    StatusCodes.Status400BadRequest,
                    ErrorDetailCategory.ModelValidation);

                foreach (var error in e.Errors)
                {
                    response.AddErrorMessage(e.GetType().Name, error);
                }

                await HandleExceptionAsync(context, response);
            }
            catch (Exception e)
            {
                var response = new ErrorDetailResponse(
                    StatusCodes.Status500InternalServerError,
                    ErrorDetailCategory.Error);

                response.AddErrorMessage(e.GetType().Name, e.Message);

                if (e.InnerException != null)
                {
                    response.AddErrorMessage(
                        e.InnerException.GetType().Name,
                        e.InnerException.Message);
                }

                await HandleExceptionAsync(context, response);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, ErrorDetailResponse response)
        {
            context.Response.StatusCode = response.StatusCode;
            _logger.LogError("Error de ejecución en la operación: {@response}.", response);
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}