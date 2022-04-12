using Aurora.Framework.Logic.Repositories;
using Aurora.Platform.Domain.Applications.Models;
using Aurora.Platform.Domain.Applications.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Aurora.Platform.Repositories.Applications
{
    public class ProfileRepository : DataRepository<ProfileData>, IProfileRepository
    {
        #region Miembros privados de la clase

        private readonly PlatformDataContext _dataContext;

        #endregion

        #region Constructores de la clase

        public ProfileRepository(PlatformDataContext dataContext)
            : base(dataContext)
        {
            _dataContext = dataContext;
        }

        #endregion

        #region Implementación de la interface IProfileRepository

        async Task<ProfileData> IProfileRepository.GetAsync(int profileId)
        {
            return await _dataContext
                .Profiles
                .AsNoTracking()
                .Include(x => x.Connections)
                .FirstOrDefaultAsync(x => x.ProfileId.Equals(profileId));
        }

        async Task<ProfileData> IProfileRepository.GetAsync(short applicationId, string code)
        {
            return await _dataContext
                .Profiles
                .AsNoTracking()
                .Include(x => x.Connections)
                .FirstOrDefaultAsync(x => x.ApplicationId.Equals(applicationId) && x.Code.Equals(code));
        }

        #endregion
    }
}