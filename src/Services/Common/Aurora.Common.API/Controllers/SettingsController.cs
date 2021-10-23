using Aurora.Common.Domain.Settings;
using Aurora.Common.Services.Settings.Commands;
using Aurora.Common.Services.Settings.Queries;
using Aurora.Framework.Collections;
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
    [Route("aurora/api/common/settings")]
    public class SettingsController : AuroraController
    {
        #region Miembros privados del controlador

        private readonly IAttributeSettingQueryService _attributeSettingQueryService;
        private readonly IAttributeValueQueryService _attributeValueQueryService;

        #endregion

        #region Constructores del controlador

        public SettingsController(
            IAttributeSettingQueryService attributeSettingQueryService,
            IAttributeValueQueryService attributeValueQueryService,
            IMediator mediator)
            : base(mediator)
        {
            _attributeSettingQueryService = attributeSettingQueryService ?? throw new ArgumentNullException(nameof(attributeSettingQueryService));
            _attributeValueQueryService = attributeValueQueryService ?? throw new ArgumentNullException(nameof(attributeValueQueryService));
        }

        #endregion

        #region Operaciones del controlador

        // GET aurora/api/common/settings/configs/{code}
        /// <summary>
        /// Obtiene un registro de configuración de atributos de parametrización.
        /// </summary>
        /// <param name="code">Código de la configuración de atributo de parametrización.</param>
        /// <returns>Registro de configuración de atributo de parametrización.</returns>
        [HttpGet("configs/{code}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AttributeSetting>> GetSetting(string code)
        {
            try
            {
                var setting = await _attributeSettingQueryService.GetAsync(code);
                if (setting == null) return NoContent();

                return Ok(setting);
            }
            catch (Exception e)
            {
                return ProcessException(StatusCodes.Status404NotFound, e);
            }
        }

        // GET aurora/api/common/settings/configs
        /// <summary>
        /// Obtiene la lista de configuraciones de atributos de parametrización en formato paginado.
        /// </summary>
        /// <param name="viewRequest">Entidad con la información de números de página y elementos de la consulta.</param>
        /// <param name="scopeType">Tipo de ámbito (nivel) de configuración de atributos de parametrización.</param>
        /// <param name="onlyGetActives">Indica si solo se obtienen las configuraciones de atributos de parametrización en estado activo.</param>
        /// <returns>Lista de configuraciones de atributos de parametrización en formato paginado.</returns>
        [HttpGet("configs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedCollection<AttributeSetting>>> GetSettingsList(
            [FromQuery] PagedViewRequest viewRequest, [FromQuery] string scopeType, [FromQuery] bool onlyGetActives)
        {
            try
            {
                var settings = await _attributeSettingQueryService.GetListAsync(viewRequest, scopeType, onlyGetActives);
                return Ok(settings);
            }
            catch (Exception e)
            {
                return ProcessException(StatusCodes.Status404NotFound, e);
            }
        }

        // POST aurora/api/common/settings/configs/create
        /// <summary>
        /// Crea un nuevo registro de configuración de atributo de parametrización.
        /// </summary>
        /// <param name="command">Clase con la información requerida para la creación de la nueva configuración de atributo de parametrización.</param>
        [HttpPost("configs/create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SettingResponse>> CreateSetting([FromBody] SettingCreateCommand command)
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

        // GET aurora/api/common/settings/values/{code}
        /// <summary>
        /// Obtiene un registro de valor de atributo de parametrización.
        /// </summary>
        /// <param name="code">Código del de atributo de parametrización.</param>
        /// <param name="relationshipId">Identificador del registro de relación.</param>
        /// <returns>Registro de valor de atributo de parametrización.</returns>
        [HttpGet("values/{code}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AttributeValue>> GetValue(string code, [FromQuery] int relationshipId)
        {
            try
            {
                var value = await _attributeValueQueryService.GetAsync(code, relationshipId);
                if (value == null) return NoContent();

                return Ok(value);
            }
            catch (Exception e)
            {
                return ProcessException(StatusCodes.Status404NotFound, e);
            }
        }

        // GET aurora/api/common/settings/values/{scopeType}/{relationshipId}
        /// <summary>
        /// Obtiene la lista de valores de atributos de parametrización de un ámbito de configuración y un registro de relación determinados.
        /// </summary>
        /// <param name="scopeType">Tipo de ámbito (nivel) de configuración de atributos de parametrización.</param>
        /// <param name="relationshipId">Identificador del registro de relación.</param>
        /// <returns>Lista de valores de atributos de parametrización de un ámbito de configuración y un registro de relación.</returns>
        [HttpGet("values/{scopeType}/{relationshipId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IList<AttributeValue>>> GetValuesList(string scopeType, int relationshipId)
        {
            try
            {
                var values = await _attributeValueQueryService.GetListAsync(scopeType, relationshipId);
                return Ok(values);
            }
            catch (Exception e)
            {
                return ProcessException(StatusCodes.Status404NotFound, e);
            }
        }

        // POST aurora/api/common/settings/values/save
        /// <summary>
        /// Almacena un registro de valor de atributo de parametrización.
        /// </summary>
        /// <param name="command">Clase con la información requerida para el almacenamiento del valor de atributo de parametrización.</param>
        [HttpPost("values/save")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ValueResponse>> SaveValue([FromBody] ValueSaveCommand command)
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

        #endregion
    }
}