using Aurora.Platform.Domain.Applications.Models;
using Aurora.Platform.Domain.Applications.Repositories;
using Aurora.Platform.Domain.Exceptions;
using Aurora.Platform.Services.Applications.Commands;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Aurora.Platform.Services.Applications.Handlers
{
    public class RepositoryCreateHandler : IRequestHandler<RepositoryCreateCommand, RepositoryResponse>
    {
        #region Miembros privados de la clase

        private readonly IApplicationRepository _applicationRepository;
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;

        #endregion

        #region Constructores de la clase

        public RepositoryCreateHandler(
            IApplicationRepository applicationRepository,
            IGenericRepository genericRepository,
            IMapper mapper)
        {
            _applicationRepository = applicationRepository;
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        #endregion

        #region Implementación de la interface IRequestHandler

        async Task<RepositoryResponse> IRequestHandler<RepositoryCreateCommand, RepositoryResponse>.Handle(
            RepositoryCreateCommand request, CancellationToken cancellationToken)
        {
            // Se verifica que la aplicación se encuentre registrada
            await VerifyIfApplicationExists(request.ApplicationId);

            // Se verifica si el repositorio ya se encuentra registrado
            await VerifyIfRepositoryExists(request.ApplicationId, request.Description);

            // Se crea el registro de repositorio
            var entry = CreateRepositoryData(request);
            entry = await _genericRepository.InsertAsync(entry);

            return new RepositoryResponse(entry);
        }

        #endregion

        #region Métodos privados de la clase

        private RepositoryData CreateRepositoryData(RepositoryCreateCommand request)
        {
            return _mapper.Map<RepositoryData>(request);
        }

        private async Task VerifyIfApplicationExists(short applicationId)
        {
            var applicationData = await _applicationRepository.GetAsync(applicationId);

            if (applicationData == null)
            {
                throw new InvalidApplicationIdException(applicationId);
            }
        }

        private async Task VerifyIfRepositoryExists(short applicationId, string code)
        {
            var repository = await _genericRepository.GetAsync(applicationId, code);

            if (repository != null)
            {
                throw new ExistsRepositoryNameException(code);
            }
        }

        #endregion
    }
}