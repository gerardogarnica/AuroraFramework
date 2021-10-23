using Aurora.Framework.Sessions;
using Aurora.Platform.Domain.Applications;
using Aurora.Platform.Domain.Applications.Models;
using Aurora.Platform.Services.Applications.Commands;
using AutoMapper;
using System;

namespace Aurora.Platform.Services.Applications.Mappers
{
    public class ApplicationsMapperProfile : Profile
    {
        private readonly IAuroraSession _auroraSession;

        public ApplicationsMapperProfile(IAuroraSession auroraSession)
        {
            _auroraSession = auroraSession;

            var userName = _auroraSession.GetSessionInfo().UserLoginName;

            // Source: modelo de datos. Destination: modelo de negocios.
            CreateMap<ApplicationData, Application>();
            CreateMap<ComponentData, Component>();
            CreateMap<RepositoryData, Repository>();

            // Source: comandos. Destination: modelo de datos.
            CreateMap<ApplicationCreateCommand, ApplicationData>()
                .ForMember(d => d.Code, o => o.MapFrom(o => o.Code.Trim().ToUpper()))
                .ForMember(d => d.Name, o => o.MapFrom(o => o.Name.Trim()))
                .ForMember(d => d.Description, o => o.MapFrom(o => o.Description.Trim()))
                .ForMember(d => d.CreatedDate, o => o.MapFrom(o => DateTime.Now));

            CreateMap<ComponentCreateCommand, ComponentData>()
                .ForMember(d => d.Code, o => o.MapFrom(o => o.Code.Trim().ToUpper()))
                .ForMember(d => d.Description, o => o.MapFrom(o => o.Description.Trim()))
                .ForMember(d => d.CreatedDate, o => o.MapFrom(o => DateTime.Now));

            CreateMap<RepositoryCreateCommand, RepositoryData>()
                .ForMember(d => d.Code, o => o.MapFrom(o => Guid.NewGuid().ToString().ToUpper()))
                .ForMember(d => d.Description, o => o.MapFrom(o => o.Description.Trim().ToUpper()))
                .ForMember(d => d.CreatedDate, o => o.MapFrom(o => DateTime.Now));

            CreateMap<RepositoryDetailCreateCommand, RepositoryDetailData>()
                .ForMember(d => d.StringData, o => o.MapFrom(o => o.GetConnectionString()))
                .ForMember(d => d.CreatedBy, o => o.MapFrom(o => userName))
                .ForMember(d => d.CreatedDate, o => o.MapFrom(o => DateTime.Now))
                .ForMember(d => d.LastUpdatedBy, o => o.MapFrom(o => userName))
                .ForMember(d => d.LastUpdatedDate, o => o.MapFrom(o => DateTime.Now));
        }
    }
}