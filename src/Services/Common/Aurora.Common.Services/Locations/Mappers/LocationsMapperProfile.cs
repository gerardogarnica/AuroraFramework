using Aurora.Common.Domain.Locations;
using Aurora.Common.Domain.Locations.Models;
using Aurora.Common.Services.Locations.Commands;
using Aurora.Framework.Collections;
using Aurora.Framework.Sessions;
using AutoMapper;
using System;

namespace Aurora.Common.Services.Locations.Mappers
{
    public class LocationsMapperProfile : Profile
    {
        private readonly IAuroraSession _auroraSession;

        public LocationsMapperProfile(IAuroraSession auroraSession)
        {
            _auroraSession = auroraSession;

            var userName = _auroraSession.GetSessionInfo().UserLoginName;

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
                .ForMember(d => d.CreatedBy, o => o.MapFrom(o => userName))
                .ForMember(d => d.CreatedDate, o => o.MapFrom(o => DateTime.Now))
                .ForMember(d => d.LastUpdatedBy, o => o.MapFrom(o => userName))
                .ForMember(d => d.LastUpdatedDate, o => o.MapFrom(o => DateTime.Now));

            CreateMap<CountryDivisionSaveCommand, CountryDivisionData>()
                .ForMember(d => d.Name, o => o.MapFrom(o => o.Name.Trim()))
                .ForMember(d => d.CreatedBy, o => o.MapFrom(o => userName))
                .ForMember(d => d.CreatedDate, o => o.MapFrom(o => DateTime.Now))
                .ForMember(d => d.LastUpdatedBy, o => o.MapFrom(o => userName))
                .ForMember(d => d.LastUpdatedDate, o => o.MapFrom(o => DateTime.Now));

            CreateMap<LocationCreateCommand, LocationData>()
                .ForMember(d => d.Name, o => o.MapFrom(o => o.Name.Trim()))
                .ForMember(d => d.CreatedBy, o => o.MapFrom(o => userName))
                .ForMember(d => d.CreatedDate, o => o.MapFrom(o => DateTime.Now))
                .ForMember(d => d.LastUpdatedBy, o => o.MapFrom(o => userName))
                .ForMember(d => d.LastUpdatedDate, o => o.MapFrom(o => DateTime.Now));
        }
    }
}