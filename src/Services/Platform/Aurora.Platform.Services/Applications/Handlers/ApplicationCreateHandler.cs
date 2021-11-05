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
    public class ApplicationCreateHandler : IRequestHandler<ApplicationCreateCommand, ApplicationResponse>
    {
        #region Miembros privados de la clase

        private readonly IApplicationRepository _applicationRepository;
        private readonly IMapper _mapper;

        #endregion

        #region Constructores de la clase

        public ApplicationCreateHandler(
            IApplicationRepository applicationRepository,
            IMapper mapper)
        {
            _applicationRepository = applicationRepository;
            _mapper = mapper;
        }

        #endregion

        #region Implementación de la interface IRequestHandler

        async Task<ApplicationResponse> IRequestHandler<ApplicationCreateCommand, ApplicationResponse>.Handle(
            ApplicationCreateCommand request, CancellationToken cancellationToken)
        {
            // Se verifica si el código de aplicación ya se encuentra registrado
            await VerifyIfApplicationExists(request.Code);

            // Se crea el registro de aplicación
            var entry = CreateApplicationData(request);
            entry = await _applicationRepository.InsertAsync(entry);

            return new ApplicationResponse(entry);
        }

        #endregion

        #region Métodos privados de la clase

        private ApplicationData CreateApplicationData(ApplicationCreateCommand request)
        {
            return _mapper.Map<ApplicationData>(request);
        }

        private async Task VerifyIfApplicationExists(string code)
        {
            var applicationData = await _applicationRepository.GetAsync(code);

            if (applicationData != null)
            {
                throw new ExistsApplicationCodeException(code);
            }
        }

        #endregion
    }
}