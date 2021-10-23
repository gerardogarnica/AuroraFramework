using Aurora.Common.Domain.Catalogs.Models;
using Aurora.Common.Domain.Catalogs.Repositories;
using Aurora.Framework.Logic.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Aurora.Common.Repositories.Catalogs
{
    public class CatalogRepository : DataRepository<CatalogData>, ICatalogRepository
    {
        #region Miembros privados de la clase

        private readonly CommonDataContext _dataContext;

        #endregion

        #region Constructores de la clase

        public CatalogRepository(CommonDataContext dataContext)
            : base(dataContext)
        {
            _dataContext = dataContext;
        }

        #endregion

        #region Implementación de la interface ICatalogRepository

        async Task<CatalogData> ICatalogRepository.GetByCodeAsync(string code)
        {
            return await _dataContext
                .Catalogs
                .AsNoTracking()
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.Code.Equals(code));
        }

        #endregion
    }
}