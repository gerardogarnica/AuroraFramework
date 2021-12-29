using Aurora.Framework.Services;
using Aurora.Platform.Domain.Applications;
using Aurora.Platform.Services.Applications.Commands;
using Aurora.Platform.Services.Applications.Queries;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aurora.Platform.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("aurora/api/platform/applications")]
    public class ApplicationsController : AuroraController
    {
        #region Miembros privados del controlador

        private readonly IApplicationQueryService _applicationQueryService;

        #endregion

        #region Constructores del controlador

        public ApplicationsController(
            IApplicationQueryService applicationQueryService,
            ILogger<ApplicationsController> logger,
            IMediator mediator)
            : base(logger, mediator)
        {
            _applicationQueryService = applicationQueryService ?? throw new ArgumentNullException(nameof(applicationQueryService));
        }

        #endregion

        #region Operaciones del controlador

        // GET aurora/api/platform/applications/{code}
        /// <summary>
        /// Obtiene una aplicación de la plataforma de acuerdo a su código.
        /// </summary>
        /// <param name="code">Código de la aplicación de la plataforma.</param>
        /// <returns></returns>
        [HttpGet("{code}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Application>> Get(string code)
        {
            var application = await _applicationQueryService.GetByCodeAsync(code);
            if (application == null) return NoContent();

            return Ok(application);
        }

        // GET aurora/api/platform/applications
        /// <summary>
        /// Obtiene la lista de aplicaciones de la plataforma.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IList<Application>>> GetList()
        {
            var applications = await _applicationQueryService.GetListAsync();
            return Ok(applications);
        }

        // POST aurora/api/platform/applications/create
        /// <summary>
        /// Crea un nuevo registro de aplicación de la plataforma.
        /// </summary>
        /// <param name="command">Clase con la información requerida para la creación de una nueva aplicación.</param>
        /// <returns></returns>
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApplicationResponse>> Create([FromBody] ApplicationCreateCommand command)
        {
            var response = await _mediator.Send(command);
            return Created(string.Empty, response);
        }

        #endregion

        #region Métodos privados del controlador

        #endregion
    }
}