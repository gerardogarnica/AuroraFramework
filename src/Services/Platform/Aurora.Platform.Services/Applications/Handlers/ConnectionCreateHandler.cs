using Aurora.Framework.Logic.Data;
using Aurora.Framework.Sessions;
using Aurora.Platform.Domain.Applications.Models;
using Aurora.Platform.Domain.Applications.Repositories;
using Aurora.Platform.Domain.Exceptions;
using Aurora.Platform.Services.Applications.Commands;
using AutoMapper;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Aurora.Platform.Services.Applications.Handlers
{
    public class ConnectionCreateHandler : IRequestHandler<ConnectionCreateCommand, ProfileResponse>
    {
        #region Miembros privados de la clase

        private readonly IComponentRepository _componentRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IMapper _mapper;
        private readonly IAuroraSession _auroraSession;
        private readonly AuroraSessionInfo _sessionInfo;

        #endregion

        #region Constructores de la clase

        public ConnectionCreateHandler(
            IComponentRepository componentRepository,
            IProfileRepository profileRepository,
            IMapper mapper,
            IAuroraSession auroraSession)
        {
            _componentRepository = componentRepository;
            _profileRepository = profileRepository;
            _mapper = mapper;
            _auroraSession = auroraSession;

            _sessionInfo = _auroraSession.GetSessionInfo();
        }

        #endregion

        #region Implementación de la interface IRequestHandler

        async Task<ProfileResponse> IRequestHandler<ConnectionCreateCommand, ProfileResponse>.Handle(
            ConnectionCreateCommand request, CancellationToken cancellationToken)
        {
            // Se obtiene el perfil de configuración existente
            var entry = await GetExistentProfileData(request.ProfileId);

            // Se verifica que el componente se encuentre registrado
            await VerifyIfComponentExists(request.ComponentId);

            // Se almacena el registro de conexión
            SaveConnectionData(entry, request);
            entry = await _profileRepository.UpdateAsync(entry);

            return new ProfileResponse(entry);
        }

        #endregion

        #region Métodos privados de la clase

        private void SaveConnectionData(
            ProfileData profileData, ConnectionCreateCommand request)
        {
            if (ExistsConnectionData(profileData, request.ComponentId))
            {
                UpdateConnectionData(profileData, request);
            }
            else
            {
                CreateConnectionData(profileData, request);
            }
        }

        private void CreateConnectionData(
            ProfileData profileData, ConnectionCreateCommand request)
        {
            profileData.Connections.Add(_mapper.Map<ConnectionData>(request));
        }

        private void UpdateConnectionData(
            ProfileData profileData, ConnectionCreateCommand request)
        {
            var connectionData = profileData
                .Connections
                .FirstOrDefault(x => x.ProfileId.Equals(profileData.ProfileId) && x.ComponentId.Equals(request.ComponentId));

            connectionData.ConnString = request.GetConnectionString();
            connectionData.AddLastUpdated(_sessionInfo.UserLoginName);
        }

        private async Task<ProfileData> GetExistentProfileData(int profileId)
        {
            var profileData = await _profileRepository.GetAsync(profileId);

            if (profileData == null)
            {
                throw new InvalidProfileIdException(profileId);
            }

            return profileData;
        }

        private async Task VerifyIfComponentExists(int componentId)
        {
            var componentData = await _componentRepository.GetAsync(x => x.ComponentId.Equals(componentId));

            if (componentData == null)
            {
                throw new InvalidComponentIdException(componentId);
            }
        }

        private bool ExistsConnectionData(ProfileData profileData, int componentId)
        {
            return profileData
                .Connections
                .ToList()
                .Exists(x => x.ProfileId.Equals(profileData.ProfileId) && x.ComponentId.Equals(componentId));
        }

        #endregion
    }
}