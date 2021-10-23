using Aurora.Common.Domain.Settings.Models;
using Aurora.Framework.Logic.Repositories;

namespace Aurora.Common.Domain.Settings.Repositories
{
    public interface IAttributeSettingRepository : IQueryableRepository<AttributeSettingData>, IWriteableRepository<AttributeSettingData>
    {
    }
}