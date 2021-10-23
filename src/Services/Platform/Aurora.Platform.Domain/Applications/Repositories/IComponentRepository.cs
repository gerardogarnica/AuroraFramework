using Aurora.Framework.Logic.Repositories;
using Aurora.Platform.Domain.Applications.Models;

namespace Aurora.Platform.Domain.Applications.Repositories
{
    public interface IComponentRepository : IQueryableRepository<ComponentData>, IWriteableRepository<ComponentData>
    {
    }
}