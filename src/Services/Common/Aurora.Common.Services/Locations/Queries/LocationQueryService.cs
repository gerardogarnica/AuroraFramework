using Aurora.Common.Domain.Locations;
using Aurora.Common.Domain.Locations.Repositories;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aurora.Common.Services.Locations.Queries
{
    public interface ILocationQueryService
    {
        Task<Location> GetAsync(int locationId, bool getNextLevel, bool onlyGetActivesChilds);
        Task<IList<Location>> GetListAsync(int parentId, bool onlyGetActives);
    }

    public class LocationQueryService : ILocationQueryService
    {
        #region Miembros privados de la clase

        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;

        #endregion

        #region Constructores de la clase

        public LocationQueryService(
            ILocationRepository locationRepository,
            IMapper mapper)
        {
            _locationRepository = locationRepository;
            _mapper = mapper;
        }

        #endregion

        #region Implementación de la interface ILocationQueryService

        async Task<Location> ILocationQueryService.GetAsync(int locationId, bool getNextLevel, bool onlyGetActivesChilds)
        {
            var locationData = await _locationRepository.GetAsync(locationId);

            if (locationData == null) return null;

            var location = _mapper.Map<Location>(locationData);
            if (getNextLevel) location.Locations = await GetLocationsAsync(location.LocationId, onlyGetActivesChilds);

            return location;
        }

        async Task<IList<Location>> ILocationQueryService.GetListAsync(int parentId, bool onlyGetActives)
        {
            return await GetLocationsAsync(parentId, onlyGetActives);
        }

        #endregion

        #region Métodos privados de la clase

        private async Task<IList<Location>> GetLocationsAsync(int parentId, bool onlyGetActives)
        {
            if (parentId == 0) return new List<Location>();

            var locationsData = await _locationRepository
                .GetListAsync(x => x.ParentLocationId.Equals(parentId), x => x.Name);

            if (onlyGetActives) locationsData.ToList().RemoveAll(x => !x.IsActive);

            return _mapper.Map<List<Location>>(locationsData);
        }

        #endregion
    }
}