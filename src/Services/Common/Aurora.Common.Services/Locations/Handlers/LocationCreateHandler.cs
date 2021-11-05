using Aurora.Common.Domain.Exceptions;
using Aurora.Common.Domain.Locations.Models;
using Aurora.Common.Domain.Locations.Repositories;
using Aurora.Common.Services.Locations.Commands;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Aurora.Common.Services.Locations.Handlers
{
    public class LocationCreateHandler : IRequestHandler<LocationCreateCommand, LocationResponse>
    {
        #region Miembros privados de la clase

        private readonly ICountryDivisionRepository _divisionRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;

        #endregion

        #region Constructores de la clase

        public LocationCreateHandler(
            ICountryDivisionRepository divisionRepository,
            ILocationRepository locationRepository,
            IMapper mapper)
        {
            _divisionRepository = divisionRepository;
            _locationRepository = locationRepository;
            _mapper = mapper;
        }

        #endregion

        #region Implementación de la interface IRequestHandler

        async Task<LocationResponse> IRequestHandler<LocationCreateCommand, LocationResponse>.Handle(
            LocationCreateCommand request, CancellationToken cancellationToken)
        {
            // Se obtiene la división administrativa
            var divisionData = await GetExistentCountryDivisionData(request.DivisionId);

            // Se obtiene la localidad padre
            var parentLocationData = await GetExistentParentLocationData(request.ParentLocationId);

            // Se verifica el nivel de la localidad
            ValidateLocationLevel(divisionData, parentLocationData);

            // Se verifica si la localidad ya se encuentra registrada
            await VerifyIfLocationDataExists(request.Name, request.ParentLocationId);

            var entry = CreateLocationData(request);
            entry = await _locationRepository.InsertAsync(entry);

            return new LocationResponse(entry);
        }

        #endregion

        #region Métodos privados de la clase

        private LocationData CreateLocationData(LocationCreateCommand request)
        {
            return _mapper.Map<LocationData>(request);
        }

        private async Task<CountryDivisionData> GetExistentCountryDivisionData(short divisionId)
        {
            var divisionData = await _divisionRepository.GetAsync(x => x.DivisionId.Equals(divisionId));

            if (divisionData == null)
            {
                throw new InvalidCountryDivisionIdException(divisionId);
            }

            return divisionData;
        }

        private async Task<LocationData> GetExistentParentLocationData(int parentLocationId)
        {
            if (parentLocationId <= 0) return null;

            var locationData = await _locationRepository.GetAsync(parentLocationId);

            if (locationData == null)
            {
                throw new InvalidParentLocationIdException(parentLocationId);
            }

            return locationData;
        }

        private void ValidateLocationLevel(CountryDivisionData division, LocationData parentLocation)
        {
            if (division.LevelNumber.Equals(1))
            {
                if (parentLocation == null) return;

                throw new InvalidLocationFirstLevelException(division.Name);
            }

            if (parentLocation == null)
                throw new InvalidLocationLevelException(division.Name, division.LevelNumber);

            if (!division.LevelNumber.Equals(parentLocation.Division.LevelNumber + 1))
                throw new InvalidParentLocationLevelException(parentLocation.Name, division.Name);

            if (!parentLocation.Division.CountryId.Equals(division.CountryId))
                throw new InvalidParentLocationCountryException(parentLocation.Name, division.Country.Name);
        }

        private async Task VerifyIfLocationDataExists(string name, int parentLocationId)
        {
            var location = await _locationRepository.GetAsync(name, parentLocationId);

            if (location != null) throw new ExistsLocationNameException(name);
        }

        #endregion
    }
}