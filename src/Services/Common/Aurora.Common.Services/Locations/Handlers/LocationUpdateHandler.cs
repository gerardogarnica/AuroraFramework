using Aurora.Common.Domain.Exceptions;
using Aurora.Common.Domain.Locations.Models;
using Aurora.Common.Domain.Locations.Repositories;
using Aurora.Common.Services.Locations.Commands;
using Aurora.Framework.Logic.Data;
using Aurora.Framework.Sessions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Aurora.Common.Services.Locations.Handlers
{
    public class LocationUpdateHandler : IRequestHandler<LocationUpdateCommand, LocationResponse>
    {
        #region Miembros privados de la clase

        private readonly ILocationRepository _locationRepository;
        private readonly IAuroraSession _auroraSession;
        private readonly string _userName;

        #endregion

        #region Constructores de la clase

        public LocationUpdateHandler(
            ILocationRepository locationRepository,
            IAuroraSession auroraSession)
        {
            _locationRepository = locationRepository;
            _auroraSession = auroraSession;

            _userName = _auroraSession.GetSessionInfo().UserLoginName;
        }

        #endregion

        #region Implementación de la interface IRequestHandler

        async Task<LocationResponse> IRequestHandler<LocationUpdateCommand, LocationResponse>.Handle(
            LocationUpdateCommand request, CancellationToken cancellationToken)
        {
            // Se obtiene la localidad existente
            var entry = await GetExistentLocationData(request.LocationId);

            // Se actualiza el registro de localidad
            UpdateLocationData(entry, request);
            entry = await _locationRepository.UpdateAsync(entry);

            return new LocationResponse(entry);
        }

        #endregion

        #region Métodos privados de la clase

        private void UpdateLocationData(LocationData location, LocationUpdateCommand request)
        {
            location.Name = request.Name.Trim();
            location.Code = request.Code;
            location.AlternativeCode = request.AlternativeCode;
            location.IsActive = request.IsActive;
            location.AddLastUpdated(_userName);
        }

        private async Task<LocationData> GetExistentLocationData(int locationId)
        {
            var location = await _locationRepository.GetAsync(locationId);

            if (location == null) throw new InvalidLocationIdException(locationId);

            return location;
        }

        #endregion
    }
}