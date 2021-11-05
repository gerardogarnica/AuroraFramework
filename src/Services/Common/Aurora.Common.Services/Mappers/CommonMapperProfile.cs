using Aurora.Common.Domain.Catalogs;
using Aurora.Common.Domain.Catalogs.Models;
using Aurora.Common.Domain.Locations;
using Aurora.Common.Domain.Locations.Models;
using Aurora.Common.Domain.Settings;
using Aurora.Common.Domain.Settings.Models;
using Aurora.Common.Services.Catalogs.Commands;
using Aurora.Common.Services.Locations.Commands;
using Aurora.Common.Services.Settings.Commands;
using Aurora.Framework;
using Aurora.Framework.Collections;
using Aurora.Framework.Sessions;
using Aurora.Framework.Settings;
using AutoMapper;
using System;

namespace Aurora.Common.Services.Mappers
{
    public class CommonMapperProfile : Profile
    {
        private readonly IAuroraSession _auroraSession;
        private readonly string _userName;

        public CommonMapperProfile(IAuroraSession auroraSession)
        {
            _auroraSession = auroraSession;
            _userName = _auroraSession.GetSessionInfo().UserLoginName;

            SetCatalogsMapper();
            SetLocationsMapper();
            SetSettingsMapper();
        }

        private void SetCatalogsMapper()
        {
            // Source: modelo de datos. Destination: modelo de negocios.
            CreateMap<CatalogData, Catalog>();
            CreateMap<PagedCollection<CatalogData>, PagedCollection<Catalog>>();
            CreateMap<CatalogItemData, CatalogItem>();

            // Source: comandos. Destination: modelo de datos.
            CreateMap<CatalogCreateCommand, CatalogData>()
                .ForMember(d => d.Code, o => o.MapFrom(o => o.Code.Trim()))
                .ForMember(d => d.Name, o => o.MapFrom(o => o.Name.Trim()))
                .ForMember(d => d.Description, o => o.MapFrom(o => o.Description.Trim()));

            CreateMap<CatalogUpdateCommand, CatalogData>()
                .ForMember(d => d.Code, o => o.MapFrom(o => o.Code.Trim()))
                .ForMember(d => d.Name, o => o.MapFrom(o => o.Name.Trim()))
                .ForMember(d => d.Description, o => o.MapFrom(o => o.Description.Trim()));

            CreateMap<CatalogItemCreate, CatalogItemData>()
                .ForMember(d => d.Code, o => o.MapFrom(o => o.Code.Trim()))
                .ForMember(d => d.Description, o => o.MapFrom(o => o.Description.Trim()))
                .ForMember(d => d.CreatedBy, o => o.MapFrom(o => _userName))
                .ForMember(d => d.CreatedDate, o => o.MapFrom(o => DateTime.Now))
                .ForMember(d => d.LastUpdatedBy, o => o.MapFrom(o => _userName))
                .ForMember(d => d.LastUpdatedDate, o => o.MapFrom(o => DateTime.Now));

            CreateMap<CatalogItemSaveCommand, CatalogItemData>()
                .ForMember(d => d.Code, o => o.MapFrom(o => o.ItemCode.Trim()))
                .ForMember(d => d.Description, o => o.MapFrom(o => o.Description.Trim()))
                .ForMember(d => d.CreatedBy, o => o.MapFrom(o => _userName))
                .ForMember(d => d.CreatedDate, o => o.MapFrom(o => DateTime.Now))
                .ForMember(d => d.LastUpdatedBy, o => o.MapFrom(o => _userName))
                .ForMember(d => d.LastUpdatedDate, o => o.MapFrom(o => DateTime.Now));
        }

        private void SetLocationsMapper()
        {
            // Source: modelo de datos. Destination: modelo de negocios.
            CreateMap<CountryData, Country>();
            CreateMap<PagedCollection<CountryData>, PagedCollection<Country>>();
            CreateMap<CountryDivisionData, CountryDivision>();
            CreateMap<LocationData, Location>();

            // Source: comandos. Destination: modelo de datos.
            CreateMap<CountryCreateCommand, CountryData>()
                .ForMember(d => d.Name, o => o.MapFrom(o => o.Name.Trim()))
                .ForMember(d => d.OfficialName, o => o.MapFrom(o => o.OfficialName.Trim()))
                .ForMember(d => d.TwoLettersCode, o => o.MapFrom(o => o.TwoLettersCode.Trim()))
                .ForMember(d => d.ThreeLettersCode, o => o.MapFrom(o => o.ThreeLettersCode.Trim()))
                .ForMember(d => d.ThreeDigitsCode, o => o.MapFrom(o => o.ThreeDigitsCode.Trim()));

            CreateMap<CountryDivisionCreate, CountryDivisionData>()
                .ForMember(d => d.Name, o => o.MapFrom(o => o.Name.Trim()))
                .ForMember(d => d.CreatedBy, o => o.MapFrom(o => _userName))
                .ForMember(d => d.CreatedDate, o => o.MapFrom(o => DateTime.Now))
                .ForMember(d => d.LastUpdatedBy, o => o.MapFrom(o => _userName))
                .ForMember(d => d.LastUpdatedDate, o => o.MapFrom(o => DateTime.Now));

            CreateMap<CountryDivisionSaveCommand, CountryDivisionData>()
                .ForMember(d => d.Name, o => o.MapFrom(o => o.Name.Trim()))
                .ForMember(d => d.CreatedBy, o => o.MapFrom(o => _userName))
                .ForMember(d => d.CreatedDate, o => o.MapFrom(o => DateTime.Now))
                .ForMember(d => d.LastUpdatedBy, o => o.MapFrom(o => _userName))
                .ForMember(d => d.LastUpdatedDate, o => o.MapFrom(o => DateTime.Now));

            CreateMap<LocationCreateCommand, LocationData>()
                .ForMember(d => d.Name, o => o.MapFrom(o => o.Name.Trim()))
                .ForMember(d => d.CreatedBy, o => o.MapFrom(o => _userName))
                .ForMember(d => d.CreatedDate, o => o.MapFrom(o => DateTime.Now))
                .ForMember(d => d.LastUpdatedBy, o => o.MapFrom(o => _userName))
                .ForMember(d => d.LastUpdatedDate, o => o.MapFrom(o => DateTime.Now));
        }

        private void SetSettingsMapper()
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