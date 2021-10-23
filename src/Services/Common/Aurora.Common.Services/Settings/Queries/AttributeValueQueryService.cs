using Aurora.Common.Domain.Exceptions;
using Aurora.Common.Domain.Settings;
using Aurora.Common.Domain.Settings.Models;
using Aurora.Common.Domain.Settings.Repositories;
using Aurora.Framework;
using Aurora.Framework.Settings;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aurora.Common.Services.Settings.Queries
{
    public interface IAttributeValueQueryService
    {
        Task<AttributeValue> GetAsync(string code, int relationshipId);
        Task<IList<AttributeValue>> GetListAsync(string scopeType, int relationshipId);
    }

    public class AttributeValueQueryService : IAttributeValueQueryService
    {
        #region Miembros privados de la clase

        private readonly IAttributeSettingRepository _settingRepository;
        private readonly IAttributeValueRepository _valueRepository;
        private readonly IMapper _mapper;

        #endregion

        #region Constructores de la clase

        public AttributeValueQueryService(
            IAttributeSettingRepository settingRepository,
            IAttributeValueRepository valueRepository,
            IMapper mapper)
        {
            _settingRepository = settingRepository;
            _valueRepository = valueRepository;
            _mapper = mapper;
        }

        #endregion

        #region Implementación de la interface IAttributeValueQueryService

        async Task<AttributeValue> IAttributeValueQueryService.GetAsync(string code, int relationshipId)
        {
            var valueData = await _valueRepository.GetAsync(code, relationshipId);

            if (valueData == null)
            {
                var settingData = await GetExistentAttributeSettingData(code);

                var setting = _mapper.Map<AttributeSetting>(settingData);

                valueData = new AttributeValueData()
                {
                    AttributeId = settingData.AttributeId,
                    RelationshipId = relationshipId,
                    Value = CreateDefaultValue(setting),
                    AttributeSetting = settingData
                };
            }

            return _mapper.Map<AttributeValue>(valueData);
        }

        async Task<IList<AttributeValue>> IAttributeValueQueryService.GetListAsync(string scopeType, int relationshipId)
        {
            var settingsData = await GetSettingsList(scopeType);
            var valuesData = await _valueRepository.GetListAsync(scopeType, relationshipId);

            foreach (var settingData in settingsData)
            {
                if (valuesData.ToList().Any(x => x.AttributeId.Equals(settingData.AttributeId)))
                    continue;

                var setting = _mapper.Map<AttributeSetting>(settingData);

                valuesData.Add(new AttributeValueData()
                {
                    AttributeId = settingData.AttributeId,
                    RelationshipId = relationshipId,
                    Value = CreateDefaultValue(setting),
                    AttributeSetting = settingData
                });
            }

            return _mapper.Map<IList<AttributeValue>>(valuesData);
        }

        #endregion

        #region Métodos privados de la clase

        private async Task<IEnumerable<AttributeSettingData>> GetSettingsList(string scopeType)
        {
            return await _settingRepository.GetListAsync(
                x => x.ScopeType.Equals(scopeType), x => x.Name);
        }

        private async Task<AttributeSettingData> GetExistentAttributeSettingData(string code)
        {
            var settingData = await _settingRepository.GetAsync(x => x.Code.Equals(code));

            if (settingData == null) throw new InvalidSettingCodeException(code);

            return settingData;
        }

        private string CreateDefaultValue(AttributeSetting setting)
        {
            switch (setting.DataType)
            {
                case AuroraDataType.Boolean:
                    var booleanValue = new BooleanAttributeValue()
                    {
                        Value = setting.BooleanSetting.DefaultValue
                    };
                    return booleanValue.GetValueWrapper();

                case AuroraDataType.Catalog:
                    var catalogValue = new CatalogAttributeValue()
                    {
                        ItemCodes = setting.CatalogSetting.DefaultItemCodes
                    };
                    return catalogValue.GetValueWrapper(setting.CatalogSetting);

                case AuroraDataType.Integer:
                    var integerValue = new IntegerAttributeValue()
                    {
                        Value = setting.IntegerSetting.DefaultValue
                    };
                    return integerValue.GetValueWrapper(setting.IntegerSetting);

                case AuroraDataType.Money:
                    var moneyValue = new MoneyAttributeValue()
                    {
                        Value = setting.MoneySetting.DefaultValue
                    };
                    return moneyValue.GetValueWrapper(setting.MoneySetting);

                case AuroraDataType.Numeric:
                    var numericValue = new NumericAttributeValue()
                    {
                        Value = setting.NumericSetting.DefaultValue
                    };
                    return numericValue.GetValueWrapper(setting.NumericSetting);

                case AuroraDataType.Text:
                    var textValue = new TextAttributeValue()
                    {
                        Value = setting.TextSetting.DefaultValue
                    };
                    return textValue.GetValueWrapper(setting.TextSetting);

                default:
                    return null;
            }
        }

        #endregion
    }
}