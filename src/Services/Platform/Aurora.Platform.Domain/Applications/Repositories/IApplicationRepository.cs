using Aurora.Framework.Logic.Repositories;
using Aurora.Platform.Domain.Applications.Models;
using System.Threading.Tasks;

namespace Aurora.Platform.Domain.Applications.Repositories
{
    public interface IApplicationRepository : IQueryableRepository<ApplicationData>, IWriteableRepository<ApplicationData>
    {
        Task<ApplicationData> GetAsync(short applicationId);
        Task<ApplicationData> GetAsync(string code);
    }
}