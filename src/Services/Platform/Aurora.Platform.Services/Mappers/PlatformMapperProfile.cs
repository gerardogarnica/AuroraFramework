using Aurora.Framework.Collections;
using Aurora.Framework.Sessions;
using Aurora.Platform.Domain.Applications;
using Aurora.Platform.Domain.Applications.Models;
using Aurora.Platform.Domain.Security;
using Aurora.Platform.Domain.Security.Models;
using Aurora.Platform.Services.Applications.Commands;
using Aurora.Platform.Services.Security.Commands;
using System;
using ProfileInfo = Aurora.Platform.Domain.Applications.Profile;

namespace Aurora.Platform.Services.Mappers
{
    public class PlatformMapperProfile : AutoMapper.Profile
    {
        private readonly IAuroraSession _auroraSession;
        private readonly string _userName;

        public PlatformMapperProfile(IAuroraSession auroraSession)
        {
            _auroraSession = auroraSession;
            _userName = _auroraSession.GetSessionInfo().UserLoginName;

            SetApplicationsMapper();
            SetSecurityMapper();
        }

        private void SetApplicationsMapper()
        {
            // Source: modelo de datos. Destination: modelo de negocios.
            CreateMap<ApplicationData, Application>();
            CreateMap<ComponentData, Component>();
            CreateMap<ProfileData, ProfileInfo>();

            // Source: comandos. Destination: modelo de datos.
            CreateMap<ApplicationCreateCommand, ApplicationData>()
                .ForMember(d => d.Code, o => o.MapFrom(o => o.Code.Trim().ToUpper()))
                .ForMember(d => d.Name, o => o.MapFrom(o => o.Name.Trim()))
                .ForMember(d => d.Description, o => o.MapFrom(o => o.Description.Trim()))
                .ForMember(d => d.CreatedDate, o => o.MapFrom(o => DateTime.Now));

            CreateMap<ComponentCreateCommand, ComponentData>()
                .ForMember(d => d.Code, o => o.MapFrom(o => o.Code.Trim()))
                .ForMember(d => d.Description, o => o.MapFrom(o => o.Description.Trim()))
                .ForMember(d => d.CreatedDate, o => o.MapFrom(o => DateTime.Now));

            CreateMap<ProfileCreateCommand, ProfileData>()
                .ForMember(d => d.Code, o => o.MapFrom(o => Guid.NewGuid().ToString().ToUpper()))
                .ForMember(d => d.Description, o => o.MapFrom(o => o.Description.Trim()))
                .ForMember(d => d.CreatedDate, o => o.MapFrom(o => DateTime.Now));

            CreateMap<ConnectionCreateCommand, ConnectionData>()
                .ForMember(d => d.ConnString, o => o.MapFrom(o => o.GetConnectionString()))
                .ForMember(d => d.CreatedBy, o => o.MapFrom(o => _userName))
                .ForMember(d => d.CreatedDate, o => o.MapFrom(o => DateTime.Now))
                .ForMember(d => d.LastUpdatedBy, o => o.MapFrom(o => _userName))
                .ForMember(d => d.LastUpdatedDate, o => o.MapFrom(o => DateTime.Now));
        }

        private void SetSecurityMapper()
        {
            // Source: modelo de datos. Destination: modelo de negocios.
            CreateMap<RoleData, Role>();
            CreateMap<UserData, User>()
                .ForMember(d => d.PasswordMustChange, o => o.MapFrom(o => o.Credential.MustChange))
                .ForMember(d => d.PasswordExpirationDate, o => o.MapFrom(o => o.Credential.ExpirationDate));
            CreateMap<PagedCollection<RoleData>, PagedCollection<Role>>();
            CreateMap<PagedCollection<UserData>, PagedCollection<User>>();

            // Source: comandos. Destination: modelo de datos.
            CreateMap<RoleCreateCommand, RoleData>()
                .ForMember(d => d.Name, o => o.MapFrom(o => o.Name.Trim().ToUpper()))
                .ForMember(d => d.Description, o => o.MapFrom(o => o.Description.Trim().ToUpper()))
                .ForMember(d => d.IsDefaultRole, o => o.MapFrom(o => false))
                .ForMember(d => d.IsActive, o => o.MapFrom(o => false))
                .ForMember(d => d.CreatedBy, o => o.MapFrom(o => _userName))
                .ForMember(d => d.CreatedDate, o => o.MapFrom(o => DateTime.Now))
                .ForMember(d => d.LastUpdatedBy, o => o.MapFrom(o => _userName))
                .ForMember(d => d.LastUpdatedDate, o => o.MapFrom(o => DateTime.Now));

            CreateMap<UserCreateCommand, UserData>()
                .ForMember(d => d.LoginName, o => o.MapFrom(o => o.LoginName.Trim()))
                .ForMember(d => d.Description, o => o.MapFrom(o => o.Description.Trim().ToUpper()))
                .ForMember(d => d.Email, o => o.MapFrom(o => o.Email.Trim()))
                .ForMember(d => d.IsDefaultUser, o => o.MapFrom(o => false))
                .ForMember(d => d.IsActive, o => o.MapFrom(o => false))
                .ForMember(d => d.CreatedBy, o => o.MapFrom(o => _userName))
                .ForMember(d => d.CreatedDate, o => o.MapFrom(o => DateTime.Now))
                .ForMember(d => d.LastUpdatedBy, o => o.MapFrom(o => _userName))
                .ForMember(d => d.LastUpdatedDate, o => o.MapFrom(o => DateTime.Now));
        }
    }
}