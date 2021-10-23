using Aurora.Framework.Services;
using Aurora.Platform.Domain.Applications;
using Aurora.Platform.Services.Applications.Commands;
using Aurora.Platform.Services.Applications.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aurora.Platform.API.Controllers
{
    [ApiController]
    [Route("aurora/api/platform/repositories")]
    public class RepositoriesController : AuroraController
    {
        #region Miembros privados del controlador

        private readonly IRepositoryQueryService _repositoryQueryService;

        #endregion

        #region Constructores del controlador

        public RepositoriesController(
            IRepositoryQueryService repositoryQueryService,
            IMediator mediator)
            : base(mediator)
        {
            _repositoryQueryService = repositoryQueryService ?? throw new ArgumentNullException(nameof(repositoryQueryService));
        }

        #endregion

        #region Operaciones del controlador

        // GET aurora/api/platform/repositories/{applicationId},{code}
        /// <summary>
        /// Obtiene un registro de repositorio de la plataforma de acuerdo a su código.
        /// </summary>
        /// <param name="applicationId">ID de la aplicación de la plataforma.</param>
        /// <param name="code">Código del repositorio de la plataforma.</param>
        /// <returns></returns>
        [HttpGet("{applicationId},{code}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Repository>> Get(short applicationId, string code)
        {
            try
            {
                var repository = await _repositoryQueryService.GetByCodeAsync(applicationId, code);
                if (repository == null) return NoContent();

                return Ok(repository);
            }
            catch (Exception e)
            {
                return ProcessException(StatusCodes.Status500InternalServerError, e);
            }
        }

        // GET aurora/api/platform/repositories
        /// <summary>
        /// Obtiene la lista de repositorios de una aplicación de la plataforma.
        /// </summary>
        /// <param name="applicationId">ID de la aplicación de la plataforma.</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IList<Repository>>> GetList(short applicationId)
        {
            try
            {
                var repositories = await _repositoryQueryService.GetListAsync(applicationId);
                return Ok(repositories);
            }
            catch (Exception e)
            {
                return ProcessException(StatusCodes.Status500InternalServerError, e);
            }
        }

        // POST aurora/api/platform/repositories/create
        /// <summary>
        /// Crea un nuevo registro de repositorio de la plataforma.
        /// </summary>
        /// <param name="command">Clase con la información requerida para la creación de un nuevo repositorio.</param>
        /// <returns></returns>
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RepositoryResponse>> Create([FromBody] RepositoryCreateCommand command)
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

        // PUT aurora/api/platform/repositories/savedetail
        /// <summary>
        /// Graba un registro de detalle de conexión de un repositorio.
        /// </summary>
        /// <param name="command">Clase con la información requerida para la creación de un nuevo detalle de conexión.</param>
        /// <returns></returns>
        [HttpPut("savedetail")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RepositoryResponse>> SaveDetail([FromBody] RepositoryDetailCreateCommand command)
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
    }
}