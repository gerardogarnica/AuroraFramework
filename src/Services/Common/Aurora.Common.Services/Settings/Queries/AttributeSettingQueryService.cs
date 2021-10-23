using Aurora.Common.Domain.Settings;
using Aurora.Common.Domain.Settings.Models;
using Aurora.Common.Domain.Settings.Repositories;
using Aurora.Framework.Collections;
using Aurora.Framework.Logic;
using AutoMapper;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Aurora.Common.Services.Settings.Queries
{
    public interface IAttributeSettingQueryService
    {
        Task<AttributeSetting> GetAsync(string code);
        Task<PagedCollection<AttributeSetting>> GetListAsync(PagedViewRequest viewRequest, string scopeType, bool onlyGetActives);
    }

    public class AttributeSettingQueryService : IAttributeSettingQueryService
    {
        #region Miembros privados de la clase

        private readonly IAttributeSettingRepository _settingRepository;
        private readonly IMapper _mapper;

        #endregion

        #region Constructores de la clase

        public AttributeSettingQueryService(
            IAttributeSettingRepository settingRepository,
            IMapper mapper)
        {
            _settingRepository = settingRepository;
            _mapper = mapper;
        }

        #endregion

        #region Implementación de la interface IAttributeSettingQueryService

        async Task<AttributeSetting> IAttributeSettingQueryService.GetAsync(string code)
        {
            var settingData = await _settingRepository.GetAsync(x => x.Code.Equals(code));

            if (settingData == null) return null;

            return _mapper.Map<AttributeSetting>(settingData);
        }

        async Task<PagedCollection<AttributeSetting>> IAttributeSettingQueryService.GetListAsync(
            PagedViewRequest viewRequest, string scopeType, bool onlyGetActives)
        {
            // Adición de filtros
            Expression<Func<AttributeSettingData, bool>> filter = x => x.Equals(x);

            if (!string.IsNullOrWhiteSpace(scopeType)) filter.And(x => x.ScopeType.Equals(scopeType));
            if (onlyGetActives) filter.And(x => x.IsActive);

            var settingsData = await _settingRepository
                .GetPagedCollectionAsync(viewRequest, filter, x => x.Name);

            return _mapper.Map<PagedCollection<AttributeSetting>>(settingsData);
        }

        #endregion

        #region Métodos privados de la clase

        #endregion
    }
}