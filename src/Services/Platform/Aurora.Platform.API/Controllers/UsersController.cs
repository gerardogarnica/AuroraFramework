﻿using Aurora.Framework.Collections;
using Aurora.Framework.Services;
using Aurora.Platform.Domain.Security;
using Aurora.Platform.Services.Security.Commands;
using Aurora.Platform.Services.Security.Queries;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Aurora.Platform.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("aurora/api/platform/users")]
    public class UsersController : AuroraController
    {
        #region Miembros privados del controlador

        private readonly IUserQueryService _userQueryService;

        #endregion

        #region Constructores del controlador

        public UsersController(
            IUserQueryService userQueryService,
            IMediator mediator)
            : base(mediator)
        {
            _userQueryService = userQueryService ?? throw new ArgumentNullException(nameof(userQueryService));
        }

        #endregion

        #region Operaciones del controlador

        // GET aurora/api/platform/users/{loginName}
        /// <summary>
        /// Obtiene un usuario de acuerdo a su nombre de inicio de sesión.
        /// </summary>
        /// <param name="loginName">Nombre de inicio de sesión de usuario.</param>
        /// <returns>Registro de usuario.</returns>
        [HttpGet("{loginName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<User>> Get(string loginName)
        {
            try
            {
                var user = await _userQueryService.GetByLoginNameAsync(loginName);
                if (user == null) return NoContent();

                return Ok(user);
            }
            catch (Exception e)
            {
                return ProcessException(StatusCodes.Status404NotFound, e);
            }
        }

        // GET aurora/api/platform/users
        /// <summary>
        /// Obtiene la lista de usuarios registrados.
        /// </summary>
        /// <param name="onlyActives">Indica si solo se obtienen los usuarios activos.</param>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedCollection<User>>> GetList(
            [FromQuery] PagedViewRequest viewRequest, [FromQuery] int roleId, [FromQuery] bool onlyActives)
        {
            try
            {
                var users = await _userQueryService.GetListAsync(viewRequest, roleId, onlyActives);
                return Ok(users);
            }
            catch (Exception e)
            {
                return ProcessException(StatusCodes.Status500InternalServerError, e);
            }
        }

        // POST aurora/api/platform/users/create
        /// <summary>
        /// Crea un nuevo registro de usuario.
        /// </summary>
        /// <param name="command">Clase con la información requerida para la creación del nuevo usuario.</param>
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserResponse>> Create([FromBody] UserCreateCommand command)
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

        // PUT aurora/api/platform/users/activate/{loginName}
        /// <summary>
        /// Activa un registro de usuario existente.
        /// </summary>
        /// <param name="loginName">Nombre de inicio de sesión de usuario.</param>
        /// <returns></returns>
        [HttpPut("activate/{loginName}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserResponse>> Activate(string loginName)
        {
            try
            {
                var command = new UserUpdateCommand()
                {
                    LoginName = loginName,
                    IsActive = true
                };

                var response = await _mediator.Send(command);
                return Accepted(response);
            }
            catch (Exception e)
            {
                return ProcessException(StatusCodes.Status500InternalServerError, e);
            }
        }

        // PUT aurora/api/platform/users/deactivate/{loginName}
        /// <summary>
        /// Desactiva un registro de usuario existente.
        /// </summary>
        /// <param name="loginName">Nombre de inicio de sesión de usuario.</param>
        /// <returns></returns>
        [HttpPut("deactivate/{loginName}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserResponse>> Deactivate(string loginName)
        {
            try
            {
                var command = new UserUpdateCommand()
                {
                    LoginName = loginName,
                    IsActive = false
                };

                var response = await _mediator.Send(command);
                return Accepted(response);
            }
            catch (Exception e)
            {
                return ProcessException(StatusCodes.Status500InternalServerError, e);
            }
        }

        // PUT aurora/api/platform/users/saveroles
        /// <summary>
        /// Agrega o elimina un conjunto de roles a un usuario.
        /// </summary>
        /// <param name="command">Clase con la información requerida para la asignación o eliminación de roles del usuario.</param>
        /// <returns></returns>
        [HttpPut("saveroles")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserResponse>> SaveRoles([FromBody] UserSaveRolesCommand command)
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