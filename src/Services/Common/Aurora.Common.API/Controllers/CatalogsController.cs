using Aurora.Common.Domain.Catalogs;
using Aurora.Common.Services.Catalogs.Commands;
using Aurora.Common.Services.Catalogs.Queries;
using Aurora.Framework.Collections;
using Aurora.Framework.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Aurora.Common.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("aurora/api/common/catalogs")]
    public class CatalogsController : AuroraController
    {
        #region Miembros privados del controlador

        private readonly ICatalogQueryService _catalogQueryService;

        #endregion

        #region Constructores del controlador

        public CatalogsController(
            ICatalogQueryService catalogQueryService,
            IMediator mediator)
            : base(mediator)
        {
            _catalogQueryService = catalogQueryService ?? throw new ArgumentNullException(nameof(catalogQueryService));
        }

        #endregion

        #region Operaciones del controlador

        // GET aurora/api/common/catalogs/{code}
        /// <summary>
        /// Obtiene un catálogo con sus elementos de acuerdo a su código.
        /// </summary>
        /// <param name="code">Código de catálogo.</param>
        /// <param name="onlyGetActiveItems">Indica si solo se obtienen los elementos activos del catálogo.</param>
        /// <returns>Registro de catálogo con sus elementos.</returns>
        [HttpGet("{code}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Catalog>> Get(string code, [FromQuery] bool onlyGetActiveItems)
        {
            try
            {
                var catalog = await _catalogQueryService.GetByCodeAsync(code, onlyGetActiveItems);
                if (catalog == null) return NoContent();

                return Ok(catalog);
            }
            catch (Exception e)
            {
                return ProcessException(StatusCodes.Status404NotFound, e);
            }
        }

        // GET aurora/api/common/catalogs
        /// <summary>
        /// Obtiene la lista de catálogos en formato paginado.
        /// </summary>
        /// <param name="viewRequest">Entidad con la información de números de página y elementos de la consulta.</param>
        /// <param name="onlyGetVisibles">Indica si solo se obtienen los catálogos en estado visible.</param>
        /// <param name="onlyGetEditables">Indica si solo se obtienen los catálogos que son editables.</param>
        /// <returns>Lista de catálogos en formato paginado.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedCollection<Catalog>>> GetList(
            [FromQuery] PagedViewRequest viewRequest, [FromQuery] bool onlyGetVisibles, [FromQuery] bool onlyGetEditables)
        {
            try
            {
                var catalogs = await _catalogQueryService.GetListAsync(viewRequest, onlyGetVisibles, onlyGetEditables);
                return Ok(catalogs);
            }
            catch (Exception e)
            {
                return ProcessException(StatusCodes.Status500InternalServerError, e);
            }
        }

        // POST aurora/api/common/catalogs/create
        /// <summary>
        /// Crea un nuevo registro de catálogo con sus elementos.
        /// </summary>
        /// <param name="command">Clase con la información requerida para la creación del nuevo catálogo.</param>
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CatalogResponse>> Create([FromBody] CatalogCreateCommand command)
        {
            try
            {
                var response = await _mediator.Send(command);
                return Created(string.Empty, response);
            }
            catch (Exception e)
            {
                return ProcessException(StatusCodes.Status500InternalServerError, e);
            }
        }

        // PUT aurora/api/common/catalogs/update
        /// <summary>
        /// Actualiza un registro de catálogo.
        /// </summary>
        /// <param name="command">Clase con la información requerida para la actualización de un catálogo existente.</param>
        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CatalogResponse>> Update([FromBody] CatalogUpdateCommand command)
        {
            try
            {
                var response = await _mediator.Send(command);
                return Accepted(response);
            }
            catch (Exception e)
            {
                return ProcessException(StatusCodes.Status500InternalServerError, e);
            }
        }

        // PUT aurora/api/common/catalogs/items/save
        /// <summary>
        /// Crea o actualiza un registro de elemento de catálogo.
        /// </summary>
        /// <param name="command">Clase con la información requerida para la creación o actualización de un elemento de catálogo.</param>
        [HttpPut("items/save")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CatalogResponse>> SaveItem([FromBody] CatalogItemSaveCommand command)
        {
            try
            {
                var response = await _mediator.Send(command);
                return Accepted(response);
            }
            catch (Exception e)
            {
                return ProcessException(StatusCodes.Status500InternalServerError, e);
            }
        }

        #endregion

        #region Métodos privados del controlador

        #endregion
    }
}