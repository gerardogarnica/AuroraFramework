using Aurora.Common.Domain.Locations.Models;
using Aurora.Common.Domain.Locations.Repositories;
using Aurora.Framework.Logic.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Aurora.Common.Repositories.Locations
{
    public class LocationRepository : DataRepository<LocationData>, ILocationRepository
    {
        #region Miembros privados de la clase

        private readonly CommonDataContext _dataContext;

        #endregion

        #region Constructores de la clase

        public LocationRepository(CommonDataContext dataContext)
            : base(dataContext)
        {
            _dataContext = dataContext;
        }

        #endregion

        #region Implementación de la interface ILocationRepository

        async Task<LocationData> ILocationRepository.GetAsync(int locationId)
        {
            return await _dataContext
                .Locations
                .AsNoTracking()
                .Include(x => x.Division)
                .FirstOrDefaultAsync(x => x.LocationId.Equals(locationId));
        }

        async Task<LocationData> ILocationRepository.GetAsync(string name, int parentLocationId)
        {
            return await _dataContext
                .Locations
                .AsNoTracking()
                .Include(x => x.Division)
                .FirstOrDefaultAsync(x => x.Name.Equals(name) && x.ParentLocationId.Equals(parentLocationId));
        }

        #endregion
    }
}