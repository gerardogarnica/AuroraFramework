using Aurora.Common.Domain.Settings.Models;
using Aurora.Framework.Logic.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aurora.Common.Domain.Settings.Repositories
{
    public interface IAttributeValueRepository : IQueryableRepository<AttributeValueData>, IWriteableRepository<AttributeValueData>
    {
        Task<AttributeValueData> GetAsync(string code, int relationshipId);
        Task<IList<AttributeValueData>> GetListAsync(string scopeType, int relationshipId);
    }
}