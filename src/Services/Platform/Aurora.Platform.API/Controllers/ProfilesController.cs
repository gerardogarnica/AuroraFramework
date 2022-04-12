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
    [Route("aurora/api/platform/profiles")]
    public class ProfilesController : AuroraController
    {
        #region Miembros privados del controlador

        private readonly IProfileQueryService _profileQueryService;

        #endregion

        #region Constructores del controlador

        public ProfilesController(
            IProfileQueryService profileQueryService,
            ILogger<ProfilesController> logger,
            IMediator mediator)
            : base(logger, mediator)
        {
            _profileQueryService = profileQueryService ?? throw new ArgumentNullException(nameof(profileQueryService));
        }

        #endregion

        #region Operaciones del controlador

        // GET aurora/api/platform/profiles/{applicationId},{code}
        /// <summary>
        /// Obtiene un registro de perfil de configuración de la plataforma de acuerdo a su código.
        /// </summary>
        /// <param name="applicationId">ID de la aplicación de la plataforma.</param>
        /// <param name="code">Código del perfil de configuración de la plataforma.</param>
        /// <returns></returns>
        [HttpGet("{applicationId},{code}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Profile>> Get(short applicationId, string code)
        {
            var profile = await _profileQueryService.GetByCodeAsync(applicationId, code);
            if (profile == null) return NoContent();

            return Ok(profile);
        }

        // GET aurora/api/platform/profiles
        /// <summary>
        /// Obtiene la lista de perfiles de configuración de una aplicación de la plataforma.
        /// </summary>
        /// <param name="applicationId">ID de la aplicación de la plataforma.</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IList<Profile>>> GetList(short applicationId)
        {
            var profiles = await _profileQueryService.GetListAsync(applicationId);
            return Ok(profiles);
        }

        // POST aurora/api/platform/profiles/create
        /// <summary>
        /// Crea un nuevo registro de perfil de configuración de la plataforma.
        /// </summary>
        /// <param name="command">Clase con la información requerida para la creación de un nuevo perfil de configuración.</param>
        /// <returns></returns>
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProfileResponse>> Create([FromBody] ProfileCreateCommand command)
        {
            var response = await _mediator.Send(command);
            return Created(string.Empty, response);
        }

        // PUT aurora/api/platform/profiles/connection
        /// <summary>
        /// Graba un registro de conexión de un perfil de configuración.
        /// </summary>
        /// <param name="command">Clase con la información requerida para la creación de una nueva conexión.</param>
        /// <returns></returns>
        [HttpPut("connection")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProfileResponse>> SaveDetail([FromBody] ConnectionCreateCommand command)
        {
            var response = await _mediator.Send(command);
            return Accepted(response);
        }

        #endregion

        #region Métodos privados del controlador

        #endregion

    }
}