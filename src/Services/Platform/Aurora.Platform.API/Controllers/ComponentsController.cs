using Aurora.Framework.Services;
using Aurora.Platform.Domain.Applications;
using Aurora.Platform.Services.Applications.Commands;
using Aurora.Platform.Services.Applications.Queries;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aurora.Platform.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("aurora/api/platform/components")]
    public class ComponentsController : AuroraController
    {
        #region Miembros privados del controlador

        private readonly IComponentQueryService _componentQueryService;

        #endregion

        #region Constructores del controlador

        public ComponentsController(
            IComponentQueryService componentQueryService,
            IMediator mediator)
            : base(mediator)
        {
            _componentQueryService = componentQueryService ?? throw new ArgumentNullException(nameof(componentQueryService));
        }

        #endregion

        #region Operaciones del controlador

        // GET aurora/api/platform/components/{applicationId},{code}
        /// <summary>
        /// Obtiene un registro de componente de la plataforma de acuerdo a su código.
        /// </summary>
        /// <param name="applicationId">Identificador único de la aplicación de la plataforma.</param>
        /// <param name="code">Código del componente de la aplicación.</param>
        /// <returns></returns>
        [HttpGet("{applicationId},{code}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Component>> Get(short applicationId, string code)
        {
            var component = await _componentQueryService.GetByCodeAsync(applicationId, code);
            if (component == null) return NoContent();

            return Ok(component);
        }

        // GET aurora/api/platform/components
        /// <summary>
        /// Obtiene la lista de componentes de una aplicación de la plataforma.
        /// </summary>
        /// <param name="applicationId">Identificador único de la aplicación de la plataforma.</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IList<Component>>> GetList(short applicationId)
        {
            var components = await _componentQueryService.GetListAsync(applicationId);
            return Ok(components);
        }

        // POST aurora/api/platform/components/create
        /// <summary>
        /// Crea un nuevo registro de componente de la plataforma.
        /// </summary>
        /// <param name="command">Clase con la información requerida para la creación de un nuevo componente.</param>
        /// <returns></returns>
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ComponentResponse>> Create([FromBody] ComponentCreateCommand command)
        {
            var response = await _mediator.Send(command);
            return Created(string.Empty, response);
        }

        #endregion

        #region Métodos privados del controlador

        #endregion
    }
}