using Aurora.Framework.Logic.Repositories;
using Aurora.Platform.Domain.Applications.Models;
using Aurora.Platform.Domain.Applications.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Aurora.Platform.Repositories.Applications
{
    public class ApplicationRepository : DataRepository<ApplicationData>, IApplicationRepository
    {
        #region Miembros privados de la clase

        private readonly PlatformDataContext _dataContext;

        #endregion

        #region Constructores de la clase

        public ApplicationRepository(PlatformDataContext dataContext)
            : base(dataContext)
        {
            _dataContext = dataContext;
        }

        #endregion

        #region Implementación de la interface IApplicationRepository

        async Task<ApplicationData> IApplicationRepository.GetAsync(string code)
        {
            return await _dataContext
                .Applications
                .AsNoTracking()
                .Include(x => x.Components)
                .Include(x => x.Profiles)
                .FirstOrDefaultAsync(x => x.Code.Equals(code));
        }

        async Task<ApplicationData> IApplicationRepository.GetAsync(short applicationId)
        {
            return await _dataContext
                .Applications
                .AsNoTracking()
                .Include(x => x.Components)
                .Include(x => x.Profiles)
                .FirstOrDefaultAsync(x => x.ApplicationId.Equals(applicationId));
        }

        #endregion
    }
}