using Aurora.Framework.Logic.Repositories;
using Aurora.Platform.Domain.Applications.Models;
using System.Threading.Tasks;

namespace Aurora.Platform.Domain.Applications.Repositories
{
    public interface IProfileRepository : IQueryableRepository<ProfileData>, IWriteableRepository<ProfileData>
    {
        Task<ProfileData> GetAsync(int profileId);
        Task<ProfileData> GetAsync(string applicationCode, string code);
    }
}