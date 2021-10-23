using Aurora.Common.Domain.Locations.Models;
using Aurora.Common.Domain.Locations.Repositories;
using Aurora.Framework.Logic.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Aurora.Common.Repositories.Locations
{
    public class CountryRepository : DataRepository<CountryData>, ICountryRepository
    {
        #region Miembros privados de la clase

        private readonly CommonDataContext _dataContext;

        #endregion

        #region Constructores de la clase

        public CountryRepository(CommonDataContext dataContext)
            : base(dataContext)
        {
            _dataContext = dataContext;
        }

        #endregion

        #region Implementación de la interface ICountryRepository

        async Task<CountryData> ICountryRepository.GetAsync(short countryId)
        {
            return await _dataContext
                .Countries
                .AsNoTracking()
                .Include(x => x.Divisions)
                .FirstOrDefaultAsync(x => x.CountryId.Equals(countryId));
        }

        async Task<CountryData> ICountryRepository.GetAsync(string threeLettersCode)
        {
            return await _dataContext
                .Countries
                .AsNoTracking()
                .Include(x => x.Divisions)
                .FirstOrDefaultAsync(x => x.ThreeLettersCode.Equals(threeLettersCode));
        }

        #endregion
    }
}