using Aurora.Common.Domain.Catalogs;
using Aurora.Common.Domain.Catalogs.Models;
using Aurora.Common.Services.Catalogs.Commands;
using Aurora.Framework.Collections;
using Aurora.Framework.Sessions;
using AutoMapper;
using System;

namespace Aurora.Common.Services.Catalogs.Mappers
{
    public class CatalogMapperProfile : Profile
    {
        private readonly IAuroraSession _auroraSession;

        public CatalogMapperProfile(IAuroraSession auroraSession)
        {
            _auroraSession = auroraSession;

            var userName = _auroraSession.GetSessionInfo().UserLoginName;

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
                .ForMember(d => d.CreatedBy, o => o.MapFrom(o => userName))
                .ForMember(d => d.CreatedDate, o => o.MapFrom(o => DateTime.Now))
                .ForMember(d => d.LastUpdatedBy, o => o.MapFrom(o => userName))
                .ForMember(d => d.LastUpdatedDate, o => o.MapFrom(o => DateTime.Now));

            CreateMap<CatalogItemSaveCommand, CatalogItemData>()
                .ForMember(d => d.Code, o => o.MapFrom(o => o.ItemCode.Trim()))
                .ForMember(d => d.Description, o => o.MapFrom(o => o.Description.Trim()))
                .ForMember(d => d.CreatedBy, o => o.MapFrom(o => userName))
                .ForMember(d => d.CreatedDate, o => o.MapFrom(o => DateTime.Now))
                .ForMember(d => d.LastUpdatedBy, o => o.MapFrom(o => userName))
                .ForMember(d => d.LastUpdatedDate, o => o.MapFrom(o => DateTime.Now));
        }
    }
}