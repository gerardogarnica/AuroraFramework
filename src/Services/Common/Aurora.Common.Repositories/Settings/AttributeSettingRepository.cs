using Aurora.Common.Domain.Settings.Models;
using Aurora.Common.Domain.Settings.Repositories;
using Aurora.Framework.Logic.Repositories;

namespace Aurora.Common.Repositories.Settings
{
    public class AttributeSettingRepository : DataRepository<AttributeSettingData>, IAttributeSettingRepository
    {
        #region Miembros privados de la clase

        private readonly CommonDataContext _dataContext;

        #endregion

        #region Constructores de la clase

        public AttributeSettingRepository(CommonDataContext dataContext)
            : base(dataContext)
        {
            _dataContext = dataContext;
        }

        #endregion

        #region Implementación de la interface IAttributeSettingRepository

        #endregion
    }
}