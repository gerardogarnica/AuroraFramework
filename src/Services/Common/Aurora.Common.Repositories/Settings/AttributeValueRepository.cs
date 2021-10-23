using Aurora.Common.Domain.Settings.Models;
using Aurora.Common.Domain.Settings.Repositories;
using Aurora.Framework.Logic.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aurora.Common.Repositories.Settings
{
    public class AttributeValueRepository : DataRepository<AttributeValueData>, IAttributeValueRepository
    {
        #region Miembros privados de la clase

        private readonly CommonDataContext _dataContext;

        #endregion

        #region Constructores de la clase

        public AttributeValueRepository(CommonDataContext dataContext)
            : base(dataContext)
        {
            _dataContext = dataContext;
        }

        #endregion

        #region Implementación de la interface IAttributeValueRepository

        async Task<AttributeValueData> IAttributeValueRepository.GetAsync(string code, int relationshipId)
        {
            return await _dataContext
                .AttributeValues
                .Include(x => x.AttributeSetting)
                .FirstOrDefaultAsync(x => x.AttributeSetting.Code.Equals(code) && x.RelationshipId.Equals(relationshipId));
        }

        async Task<IList<AttributeValueData>> IAttributeValueRepository.GetListAsync(string scopeType, int relationshipId)
        {
            return await _dataContext
                .AttributeValues
                .Include(x => x.AttributeSetting)
                .Where(x => x.AttributeSetting.ScopeType.Equals(scopeType) && x.RelationshipId.Equals(relationshipId))
                .ToListAsync();
        }

        #endregion
    }
}