using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Aurora.Framework.Services
{
    /// <summary>
    /// Clase base para un controlador MVC de servicios API de Aurora.
    /// </summary>
    [ProducesErrorResponseType(typeof(ErrorDetailResponse))]
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

        #endregion
    }
}