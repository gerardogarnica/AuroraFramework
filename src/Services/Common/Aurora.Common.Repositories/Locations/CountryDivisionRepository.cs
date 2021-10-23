using Aurora.Common.Domain.Locations.Models;
using Aurora.Common.Domain.Locations.Repositories;
using Aurora.Framework.Logic.Repositories;

namespace Aurora.Common.Repositories.Locations
{
    public class CountryDivisionRepository : DataRepository<CountryDivisionData>, ICountryDivisionRepository
    {
        #region Miembros privados de la clase

        private readonly CommonDataContext _dataContext;

        #endregion

        #region Constructores de la clase

        public CountryDivisionRepository(CommonDataContext dataContext)
            : base(dataContext)
        {
            _dataContext = dataContext;
        }

        #endregion

        #region Implementación de la interface ICountryDivisionRepository

        #endregion
    }
}