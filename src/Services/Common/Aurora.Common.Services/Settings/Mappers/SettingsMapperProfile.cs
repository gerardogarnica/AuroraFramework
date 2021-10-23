using Aurora.Common.Domain.Settings;
using Aurora.Common.Domain.Settings.Models;
using Aurora.Common.Services.Settings.Commands;
using Aurora.Framework;
using Aurora.Framework.Collections;
using Aurora.Framework.Settings;
using AutoMapper;

namespace Aurora.Common.Services.Settings.Mappers
{
    public class SettingsMapperProfile : Profile
    {
        public SettingsMapperProfile()
        {
            // Source: modelo de datos. Destination: modelo de negocios.
            CreateMap<AttributeSettingData, AttributeSetting>()
                .ForMember(d => d.BooleanSetting, o => o.MapFrom(o => o.DataType.Equals(AuroraDataType.Boolean.ToString()) ? new BooleanAttributeSetting(o.Configuration) : null))
                .ForMember(d => d.CatalogSetting, o => o.MapFrom(o => o.DataType.Equals(AuroraDataType.Catalog.ToString()) ? new CatalogAttributeSetting(o.Configuration) : null))
                .ForMember(d => d.IntegerSetting, o => o.MapFrom(o => o.DataType.Equals(AuroraDataType.Integer.ToString()) ? new IntegerAttributeSetting(o.Configuration) : null))
                .ForMember(d => d.MoneySetting, o => o.MapFrom(o => o.DataType.Equals(AuroraDataType.Money.ToString()) ? new MoneyAttributeSetting(o.Configuration) : null))
                .ForMember(d => d.NumericSetting, o => o.MapFrom(o => o.DataType.Equals(AuroraDataType.Numeric.ToString()) ? new NumericAttributeSetting(o.Configuration) : null))
                .ForMember(d => d.TextSetting, o => o.MapFrom(o => o.DataType.Equals(AuroraDataType.Text.ToString()) ? new TextAttributeSetting(o.Configuration) : null));

            CreateMap<PagedCollection<AttributeSettingData>, PagedCollection<AttributeSetting>>();

            CreateMap<AttributeValueData, AttributeValue>()
                .ForMember(d => d.Code, o => o.MapFrom(o => o.AttributeSetting.Code))
                .ForMember(d => d.DataType, o => o.MapFrom(o => o.AttributeSetting.DataType))
                .ForMember(d => d.BooleanValue, o => o.MapFrom(o => o.AttributeSetting.DataType.Equals(AuroraDataType.Boolean.ToString()) ? new BooleanAttributeValue(o.Value) : null))
                .ForMember(d => d.CatalogValue, o => o.MapFrom(o => o.AttributeSetting.DataType.Equals(AuroraDataType.Catalog.ToString()) ? new CatalogAttributeValue(o.Value) : null))
                .ForMember(d => d.IntegerValue, o => o.MapFrom(o => o.AttributeSetting.DataType.Equals(AuroraDataType.Integer.ToString()) ? new IntegerAttributeValue(o.Value) : null))
                .ForMember(d => d.MoneyValue, o => o.MapFrom(o => o.AttributeSetting.DataType.Equals(AuroraDataType.Money.ToString()) ? new MoneyAttributeValue(o.Value) : null))
                .ForMember(d => d.NumericValue, o => o.MapFrom(o => o.AttributeSetting.DataType.Equals(AuroraDataType.Numeric.ToString()) ? new NumericAttributeValue(o.Value) : null))
                .ForMember(d => d.TextValue, o => o.MapFrom(o => o.AttributeSetting.DataType.Equals(AuroraDataType.Text.ToString()) ? new TextAttributeValue(o.Value) : null));

            // Source: comandos. Destination: modelo de datos.
            CreateMap<SettingCreateCommand, AttributeSettingData>()
                .ForMember(d => d.Name, o => o.MapFrom(o => o.Name.Trim()))
                .ForMember(d => d.DataType, o => o.MapFrom(o => o.DataType.ToString()))
                .ForMember(d => d.Configuration, o => o.MapFrom(o => o.GetConfigurationSetting()));
        }
    }
}