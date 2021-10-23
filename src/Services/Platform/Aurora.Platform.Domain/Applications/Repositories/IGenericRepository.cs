using Aurora.Framework.Logic.Repositories;
using Aurora.Platform.Domain.Applications.Models;
using System.Threading.Tasks;

namespace Aurora.Platform.Domain.Applications.Repositories
{
    public interface IGenericRepository : IQueryableRepository<RepositoryData>, IWriteableRepository<RepositoryData>
    {
        Task<RepositoryData> GetAsync(int repositoryId);
        Task<RepositoryData> GetAsync(short applicationId, string code);
    }
}