using Aurora.Common.Domain.Locations.Models;
using Aurora.Framework.Logic.Repositories;
using System.Threading.Tasks;

namespace Aurora.Common.Domain.Locations.Repositories
{
    public interface ICountryRepository : IQueryableRepository<CountryData>, IWriteableRepository<CountryData>
    {
        Task<CountryData> GetAsync(short countryId);
        Task<CountryData> GetAsync(string threeLettersCode);
    }
}