using Aurora.Common.Domain.Locations;
using Aurora.Common.Domain.Locations.Repositories;
using Aurora.Framework.Collections;
using Aurora.Framework.Logic;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Aurora.Common.Services.Locations.Queries
{
    public interface ICountryQueryService
    {
        Task<Country> GetAsync(short countryId);
        Task<PagedCollection<Country>> GetListAsync(PagedViewRequest viewRequest, bool onlyGetActives);
    }

    public class CountryQueryService : ICountryQueryService
    {
        #region Miembros privados de la clase

        private readonly ICountryRepository _countryRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;

        #endregion

        #region Constructores de la clase

        public CountryQueryService(
            ICountryRepository countryRepository,
            ILocationRepository locationRepository,
            IMapper mapper)
        {
            _countryRepository = countryRepository;
            _locationRepository = locationRepository;
            _mapper = mapper;
        }

        #endregion

        #region Implementación de la interface ICountryQueryService

        async Task<Country> ICountryQueryService.GetAsync(short countryId)
        {
            var countryData = await _countryRepository.GetAsync(countryId);

            if (countryData == null) return null;

            var country = _mapper.Map<Country>(countryData);
            country.FirstLevelLocations = await GetLocationsAsync(country);

            return country;
        }

        async Task<PagedCollection<Country>> ICountryQueryService.GetListAsync(PagedViewRequest viewRequest, bool onlyGetActives)
        {
            // Adición de filtros
            Expression<Func<Domain.Locations.Models.CountryData, bool>> filter = x => x.Equals(x);

            if (onlyGetActives)
                filter = filter.And(x => x.IsActive.Equals(true));

            var countriesData = await _countryRepository
                .GetPagedCollectionAsync(viewRequest, filter, x => x.Name);

            return _mapper.Map<PagedCollection<Country>>(countriesData);
        }

        #endregion

        #region Métodos privados de la clase

        private async Task<IList<Location>> GetLocationsAsync(Country country)
        {
            var locations = new List<Location>();

            var locationsData = await _locationRepository
                .GetListAsync(x => x.Division.CountryId.Equals(country.CountryId) && x.Division.LevelNumber <= 1, x => x.Name);

            foreach (var locationData in locationsData)
            {
                var newLocation = _mapper.Map<Location>(locationData);
                newLocation.Locations = new List<Location>();

                if (locationData.ParentLocationId.Equals(0) ||
                    !locations.Any(x => x.LocationId.Equals(locationData.ParentLocationId)))
                {
                    locations.Add(newLocation);
                    continue;
                }

                locations
                    .Find(x => x.LocationId.Equals(locationData.ParentLocationId))
                    .Locations
                    .Add(newLocation);
            }

            return locations
                .OrderBy(x => x.Name)
                .ToList();
        }

        #endregion
    }
}