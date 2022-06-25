using Aurora.Framework.Services;
using Aurora.Platform.Services.Identity.Commands;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Aurora.Identity.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("aurora/api/identity/session")]
    public class SessionController : AuroraController
    {
        #region Miembros privados del controlador

        #endregion

        #region Constructores del controlador

        public SessionController(
            ILogger<SessionController> logger,
            IMediator mediator)
            : base(logger, mediator) { }

        #endregion

        #region Operaciones del controlador

        // POST aurora/api/identity/session/login
        /// <summary>
        /// Inicia una sesión de usuario en la plataforma.
        /// </summary>
        /// <param name="command">Clase con la información requerida para el inicio de sesión de usuario.</param>
        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IdentityAccess>> Login([FromBody] UserLoginCommand command)
        {
            var response = await _mediator.Send(command);
            return Created(string.Empty, response);
        }

        // PUT aurora/api/identity/session/changepassword
        /// <summary>
        /// Registra la modificación de la contraseña de un usuario.
        /// </summary>
        /// <param name="command">Clase con la información requerida para la modificación de la contraseña de un usuario.</param>
        [HttpPut("changepassword")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserPasswordChangeResponse>> ChangePassword(
            [FromBody] UserPasswordChangeCommand command)
        {
            var response = await _mediator.Send(command);
            return Accepted(string.Empty, response);
        }

        #endregion
    }
}