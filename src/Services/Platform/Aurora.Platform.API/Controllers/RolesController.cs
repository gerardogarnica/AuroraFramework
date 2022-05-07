using Aurora.Framework.Collections;
using Aurora.Framework.Services;
using Aurora.Platform.Domain.Security;
using Aurora.Platform.Services.Security.Commands;
using Aurora.Platform.Services.Security.Queries;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Aurora.Platform.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("aurora/api/platform/roles")]
    public class RolesController : AuroraController
    {
        #region Miembros privados del controlador

        private readonly IRoleQueryService _roleQueryService;

        #endregion

        #region Constructores del controlador

        public RolesController(
            IRoleQueryService roleQueryService,
            ILogger<RolesController> logger,
            IMediator mediator)
            : base(logger, mediator)
        {
            _roleQueryService = roleQueryService ?? throw new ArgumentNullException(nameof(roleQueryService));
        }

        #endregion

        #region Operaciones del controlador

        // GET aurora/api/platform/roles
        /// <summary>
        /// Obtiene la lista de roles de usuarios.
        /// </summary>
        /// <param name="repositoryId">Identificador único del repositorio.</param>
        /// <param name="onlyActives">Indica si solo se obtienen los roles de usuarios activos.</param>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedCollection<Role>>> GetList(
            [FromQuery] PagedViewRequest viewRequest, [FromQuery] int repositoryId, [FromQuery] bool onlyActives)
        {
            var roles = await _roleQueryService.GetListAsync(viewRequest, repositoryId, onlyActives);
            return Ok(roles);
        }

        // GET aurora/api/platform/roles/{roleId}
        /// <summary>
        /// Obtiene un rol de usuarios de acuerdo a su identificador único.
        /// </summary>
        /// <param name="roleId">Identificador único del rol de usuarios.</param>
        /// <returns>Registro del rol de usuarios.</returns>
        [HttpGet("{roleId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Role>> Get(int roleId)
        {
            var role = await _roleQueryService.GetByIdAsync(roleId);
            if (role == null) return NoContent();

            return Ok(role);
        }

        // POST aurora/api/platform/roles
        /// <summary>
        /// Crea un nuevo registro de rol de usuarios.
        /// </summary>
        /// <param name="command">Clase con la información requerida para la creación del nuevo rol de usuarios.</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RoleResponse>> Create([FromBody] RoleCreateCommand command)
        {
            var response = await _mediator.Send(command);
            return Created(string.Empty, response);
        }

        // PUT aurora/api/platform/roles/{roleId}
        /// <summary>
        /// Actualiza un registro de rol de usuarios.
        /// </summary>
        /// <param name="roleId">Identificador único del rol de usuarios.</param>
        /// <param name="command">Clase con la información requerida para la actualización del rol de usuarios.</param>
        [HttpPut("{roleId}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RoleResponse>> Update(int roleId, [FromBody] RoleUpdateDescriptionCommand command)
        {
            command.RoleId = roleId;
            var response = await _mediator.Send(command);
            return Accepted(response);
        }

        // PUT aurora/api/platform/roles/{roleId}/activate
        /// <summary>
        /// Activa un registro de rol de usuarios existente.
        /// </summary>
        /// <param name="roleId">Identificador único del rol de usuarios.</param>
        [HttpPut("{roleId}/activate")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RoleResponse>> Activate(int roleId)
        {
            var command = new RoleUpdateStatusCommand()
            {
                RoleId = roleId,
                IsActive = true
            };

            var response = await _mediator.Send(command);
            return Accepted(response);
        }

        // PUT aurora/api/platform/roles/{roleId}/deactivate
        /// <summary>
        /// Desactiva un registro de rol de usuarios existente.
        /// </summary>
        /// <param name="roleId">Identificador único del rol de usuarios.</param>
        [HttpPut("{roleId}/deactivate")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RoleResponse>> Deactivate(int roleId)
        {
            var command = new RoleUpdateStatusCommand()
            {
                RoleId = roleId,
                IsActive = false
            };

            var response = await _mediator.Send(command);
            return Accepted(response);
        }

        // PUT aurora/api/platform/roles/saveusers
        /// <summary>
        /// Agrega o elimina un conjunto de usuarios a un rol.
        /// </summary>
        /// <param name="command">Clase con la información requerida para la asignación o eliminación de usuarios del rol.</param>
        /// <returns></returns>
        [HttpPut("saveusers")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RoleResponse>> SaveUsers([FromBody] RoleSaveUsersCommand command)
        {
            var response = await _mediator.Send(command);
            return Accepted(response);
        }

        #endregion

        #region Métodos privados del controlador

        #endregion
    }
}