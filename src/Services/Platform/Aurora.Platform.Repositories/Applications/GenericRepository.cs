using Aurora.Framework.Logic.Repositories;
using Aurora.Platform.Domain.Applications.Models;
using Aurora.Platform.Domain.Applications.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Aurora.Platform.Repositories.Applications
{
    public class GenericRepository : DataRepository<RepositoryData>, IGenericRepository
    {
        #region Miembros privados de la clase

        private readonly PlatformDataContext _dataContext;

        #endregion

        #region Constructores de la clase

        public GenericRepository(PlatformDataContext dataContext)
            : base(dataContext)
        {
            _dataContext = dataContext;
        }

        #endregion

        #region Implementación de la interface IGenericRepository

        async Task<RepositoryData> IGenericRepository.GetAsync(int repositoryId)
        {
            return await _dataContext
                .Repositories
                .AsNoTracking()
                .Include(x => x.Details)
                .FirstOrDefaultAsync(x => x.RepositoryId.Equals(repositoryId));
        }

        async Task<RepositoryData> IGenericRepository.GetAsync(short applicationId, string code)
        {
            return await _dataContext
                .Repositories
                .AsNoTracking()
                .Include(x => x.Details)
                .FirstOrDefaultAsync(x => x.ApplicationId.Equals(applicationId) && x.Code.Equals(code));
        }

        /*async Task<IList<RepositoryData>> IGenericRepository.GetListAsync(short applicationId)
        {
            return await _dataContext
                .Repositories
                .AsNoTracking()
                .Where(x => x.ApplicationId.Equals(applicationId))
                .ToListAsync();
        }*/

        #endregion
    }
}