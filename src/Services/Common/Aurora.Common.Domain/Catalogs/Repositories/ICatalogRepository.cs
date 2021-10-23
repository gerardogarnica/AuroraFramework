using Aurora.Common.Domain.Catalogs.Models;
using Aurora.Framework.Logic.Repositories;
using System.Threading.Tasks;

namespace Aurora.Common.Domain.Catalogs.Repositories
{
    public interface ICatalogRepository : IQueryableRepository<CatalogData>, IWriteableRepository<CatalogData>
    {
        Task<CatalogData> GetByCodeAsync(string code);
    }
}