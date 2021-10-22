using Aurora.Framework.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Aurora.Framework.Services
{
    /// <summary>
    /// Clase base para un controlador MVC de servicios API de Aurora.
    /// </summary>
    public abstract class AuroraController : ControllerBase
    {
        #region Miembros privados del controlador

        /// <summary>
        /// Elemento que encapsula patrones request/response e interacciones de publicación.
        /// </summary>
        protected readonly IMediator _mediator;

        #endregion

        #region Constructores del controlador

        /// <summary>
        /// Inicializa una nueva instancia del controlador base de Aurora.
        /// </summary>
        /// <param name="mediator">Elemento que encapsula patrones request/response e interacciones de publicación.</param>
        public AuroraController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region Métodos del controlador

        /// <summary>
        /// Procesa una excepción en la ejecución de un servicio API.
        /// </summary>
        /// <param name="code">Código de la excepción.</param>
        /// <param name="e">Representación de la excepción.</param>
        protected ObjectResult ProcessException(int code, Exception e)
        {
            string message;

            if (e is BusinessException businessException)
            {
                message = string.Format("{0}. Categoría: {1}. Código: {2}",
                    businessException.Message,
                    businessException.ErrorCategory,
                    businessException.ErrorKeyName);

                return StatusCode(code, message);
            }

            message = e.Message;
            if (e.InnerException != null)
            {
                message = string.Format("{0}. Mensaje: {1}", message, e.InnerException.Message);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, message);
        }

        #endregion
    }
}