using Aurora.Framework.Collections;
using Aurora.Framework.Sessions;
using Aurora.Platform.Domain.Security;
using Aurora.Platform.Domain.Security.Models;
using Aurora.Platform.Services.Security.Commands;
using AutoMapper;
using System;

namespace Aurora.Platform.Services.Security.Mappers
{
    public class SecurityMapperProfile : Profile
    {
        private readonly IAuroraSession _auroraSession;

        public SecurityMapperProfile(IAuroraSession auroraSession)
        {
            _auroraSession = auroraSession;

            var userName = _auroraSession.GetSessionInfo().UserLoginName;

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
                .ForMember(d => d.CreatedBy, o => o.MapFrom(o => userName))
                .ForMember(d => d.CreatedDate, o => o.MapFrom(o => DateTime.Now))
                .ForMember(d => d.LastUpdatedBy, o => o.MapFrom(o => userName))
                .ForMember(d => d.LastUpdatedDate, o => o.MapFrom(o => DateTime.Now));

            CreateMap<UserCreateCommand, UserData>()
                .ForMember(d => d.LoginName, o => o.MapFrom(o => o.LoginName.Trim()))
                .ForMember(d => d.Description, o => o.MapFrom(o => o.Description.Trim().ToUpper()))
                .ForMember(d => d.Email, o => o.MapFrom(o => o.Email.Trim()))
                .ForMember(d => d.IsDefaultUser, o => o.MapFrom(o => false))
                .ForMember(d => d.IsActive, o => o.MapFrom(o => false))
                .ForMember(d => d.CreatedBy, o => o.MapFrom(o => userName))
                .ForMember(d => d.CreatedDate, o => o.MapFrom(o => DateTime.Now))
                .ForMember(d => d.LastUpdatedBy, o => o.MapFrom(o => userName))
                .ForMember(d => d.LastUpdatedDate, o => o.MapFrom(o => DateTime.Now));
        }
    }
}