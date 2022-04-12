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
    public class ProfileCreateHandler : IRequestHandler<ProfileCreateCommand, ProfileResponse>
    {
        #region Miembros privados de la clase

        private readonly IApplicationRepository _applicationRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IMapper _mapper;

        #endregion

        #region Constructores de la clase

        public ProfileCreateHandler(
            IApplicationRepository applicationRepository,
            IProfileRepository profileRepository,
            IMapper mapper)
        {
            _applicationRepository = applicationRepository;
            _profileRepository = profileRepository;
            _mapper = mapper;
        }

        #endregion

        #region Implementación de la interface IRequestHandler

        async Task<ProfileResponse> IRequestHandler<ProfileCreateCommand, ProfileResponse>.Handle(
            ProfileCreateCommand request, CancellationToken cancellationToken)
        {
            // Se verifica que la aplicación se encuentre registrada
            await VerifyIfApplicationExists(request.ApplicationId);

            // Se verifica si el perfil de configuración ya se encuentra registrado
            await VerifyIfProfileExists(request.ApplicationId, request.Description);

            // Se crea el registro de perfil de configuración
            var entry = CreateProfileData(request);
            entry = await _profileRepository.InsertAsync(entry);

            return new ProfileResponse(entry);
        }

        #endregion

        #region Métodos privados de la clase

        private ProfileData CreateProfileData(ProfileCreateCommand request)
        {
            return _mapper.Map<ProfileData>(request);
        }

        private async Task VerifyIfApplicationExists(short applicationId)
        {
            var applicationData = await _applicationRepository.GetAsync(applicationId);

            if (applicationData == null)
            {
                throw new InvalidApplicationIdException(applicationId);
            }

            if (!applicationData.HasCustomConfig)
            {
                throw new CustomConfigNotAllowedException(applicationData.Name);
            }
        }

        private async Task VerifyIfProfileExists(short applicationId, string code)
        {
            var profileData = await _profileRepository.GetAsync(applicationId, code);

            if (profileData != null)
            {
                throw new ExistsProfileNameException(code);
            }
        }

        #endregion
    }
}