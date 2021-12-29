using Aurora.Common.Domain.Locations;
using Aurora.Common.Services.Locations.Commands;
using Aurora.Common.Services.Locations.Queries;
using Aurora.Framework.Collections;
using Aurora.Framework.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Aurora.Common.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("aurora/api/common/countries")]
    public class CountriesController : AuroraController
    {
        #region Miembros privados del controlador

        private readonly ICountryQueryService _countryQueryService;

        #endregion

        #region Constructores del controlador

        public CountriesController(
            ICountryQueryService countryQueryService,
            ILogger<CountriesController> logger,
            IMediator mediator)
            : base(logger, mediator)
        {
            _countryQueryService = countryQueryService ?? throw new ArgumentNullException(nameof(countryQueryService));
        }

        #endregion

        #region Operaciones del controlador

        // GET aurora/api/common/countries/{countryId}
        /// <summary>
        /// Obtiene un registro de país con una lista de sus divisiones.
        /// </summary>
        /// <param name="countryId">Identificador único de país.</param>
        /// <returns>Registro de país con sus divisiones administrativas.</returns>
        [HttpGet("{countryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Country>> Get(short countryId)
        {
            var country = await _countryQueryService.GetAsync(countryId);
            if (country == null) return NoContent();

            return Ok(country);
        }

        // GET aurora/api/common/countries
        /// <summary>
        /// Obtiene la lista de países en formato paginado.
        /// </summary>
        /// <param name="viewRequest">Entidad con la información de números de página y elementos de la consulta.</param>
        /// <param name="onlyGetActives">Indica si solo se obtienen los países en estado activo.</param>
        /// <returns>Lista de países en formato paginado.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedCollection<Country>>> GetList(
            [FromQuery] PagedViewRequest viewRequest, [FromQuery] bool onlyGetActives)
        {
            var countries = await _countryQueryService.GetListAsync(viewRequest, onlyGetActives);
            return Ok(countries);
        }

        // POST aurora/api/common/countries/create
        /// <summary>
        /// Crea un nuevo registro de país con sus divisiones administrativas.
        /// </summary>
        /// <param name="command">Clase con la información requerida para la creación del nuevo país.</param>
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CountryResponse>> Create([FromBody] CountryCreateCommand command)
        {
            var response = await _mediator.Send(command);
            return Created(string.Empty, response);
        }

        // PUT aurora/api/common/countries/update
        /// <summary>
        /// Actualiza un registro de país.
        /// </summary>
        /// <param name="command">Clase con la información requerida para la actualización de un país existente.</param>
        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CountryResponse>> Update([FromBody] CountryUpdateCommand command)
        {
            var response = await _mediator.Send(command);
            return Accepted(response);
        }

        // PUT aurora/api/common/countries/divisions/save
        /// <summary>
        /// Crea o actualiza un registro de división administrativa de país.
        /// </summary>
        /// <param name="command">Clase con la información requerida para la creación o actualización de una división administrativa de país.</param>
        [HttpPut("divisions/save")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CountryResponse>> SaveDivision([FromBody] CountryDivisionSaveCommand command)
        {
            var response = await _mediator.Send(command);
            return Accepted(response);
        }

        #endregion

        #region Métodos privados del controlador

        #endregion
    }
}