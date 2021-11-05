using Aurora.Common.Domain.Locations;
using Aurora.Common.Services.Locations.Commands;
using Aurora.Common.Services.Locations.Queries;
using Aurora.Framework.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aurora.Common.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("aurora/api/common/locations")]
    public class LocationsController : AuroraController
    {
        #region Miembros privados del controlador

        private readonly ILocationQueryService _locationQueryService;

        #endregion

        #region Constructores del controlador

        public LocationsController(
            ILocationQueryService locationQueryService,
            IMediator mediator)
            : base(mediator)
        {
            _locationQueryService = locationQueryService ?? throw new ArgumentNullException(nameof(locationQueryService));
        }

        #endregion

        #region Operaciones del controlador

        // GET aurora/api/common/locations/{locationId}
        /// <summary>
        /// Obtiene un registro de localidad con sus subdivisiones en caso de requerirlo.
        /// </summary>
        /// <param name="locationId">Identificador único de localidad.</param>
        /// <param name="getNextLevel">Indica si se obtienen las subdivisiones administrativas del siguiente nivel.</param>
        /// <param name="onlyGetActivesChilds">Indica si solo se obtienen las subdivisiones administrativas en estado activo.</param>
        /// <returns>Registro de localidad con sus subdivisiones.</returns>
        [HttpGet("{locationId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Location>> Get(
            int locationId, [FromQuery] bool getNextLevel, [FromQuery] bool onlyGetActivesChilds)
        {
            var location = await _locationQueryService.GetAsync(locationId, getNextLevel, onlyGetActivesChilds);
            if (location == null) return NoContent();

            return Ok(location);
        }

        // GET aurora/api/common/locations
        /// <summary>
        /// Obtiene la lista de localidades de una división administrativa.
        /// </summary>
        /// <param name="parentId">ID de la división administrativa de la que se van a obtener las localidades.</param>
        /// <param name="onlyGetActives">Indica si solo se obtienen las localidades en estado activo.</param>
        /// <returns>Lista de localidades de una división administrativa.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IList<Location>>> GetList([FromQuery] int parentId, [FromQuery] bool onlyGetActives)
        {
            var locations = await _locationQueryService.GetListAsync(parentId, onlyGetActives);
            return Ok(locations);
        }

        // POST aurora/api/common/locations/create
        /// <summary>
        /// Crea un nuevo registro de localidad.
        /// </summary>
        /// <param name="command">Clase con la información requerida para la creación de la nueva localidad.</param>
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LocationResponse>> Create([FromBody] LocationCreateCommand command)
        {
            var response = await _mediator.Send(command);
            return Created(string.Empty, response);
        }

        // PUT aurora/api/common/locations/update
        /// <summary>
        /// Actualiza un registro de localidad.
        /// </summary>
        /// <param name="command">Clase con la información requerida para la actualización de una localidad existente.</param>
        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LocationResponse>> Update([FromBody] LocationUpdateCommand command)
        {
            var response = await _mediator.Send(command);
            return Accepted(response);
        }

        #endregion

        #region Métodos privados del controlador

        #endregion
    }
}