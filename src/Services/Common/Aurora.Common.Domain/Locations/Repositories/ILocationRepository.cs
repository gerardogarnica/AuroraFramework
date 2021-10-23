using Aurora.Common.Domain.Locations.Models;
using Aurora.Framework.Logic.Repositories;
using System.Threading.Tasks;

namespace Aurora.Common.Domain.Locations.Repositories
{
    public interface ILocationRepository : IQueryableRepository<LocationData>, IWriteableRepository<LocationData>
    {
        Task<LocationData> GetAsync(int locationId);
        Task<LocationData> GetAsync(string name, int parentLocationId);
    }
}