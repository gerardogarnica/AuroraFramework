using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        /// Interface para manejo de registro de logs.
        /// </summary>
        protected readonly ILogger<AuroraController> _logger;

        /// <summary>
        /// Elemento que encapsula patrones request/response e interacciones de publicación.
        /// </summary>
        protected readonly IMediator _mediator;

        #endregion

        #region Constructores del controlador

        /// <summary>
        /// Inicializa una nueva instancia del controlador base de Aurora.
        /// </summary>
        /// <param name="logger">Interface para manejo de registro de logs.</param>
        /// <param name="mediator">Elemento que encapsula patrones request/response e interacciones de publicación.</param>
        public AuroraController(
            ILogger<AuroraController> logger,
            IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region Métodos del controlador

        #endregion
    }
}