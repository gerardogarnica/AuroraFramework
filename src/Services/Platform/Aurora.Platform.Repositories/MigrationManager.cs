using Aurora.Platform.Domain.Applications.Models;
using Aurora.Platform.Domain.Security.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aurora.Platform.Repositories
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                using (var context = scope.ServiceProvider.GetRequiredService<PlatformDataContext>())
                {
                    try
                    {
                        context.Database.Migrate();

                        var applicationId = CreatePlatformApplication(context);
                        var componentId = CreatePlatformComponent(context, applicationId);
                        var repositoryId = CreatePlatformRepository(context, applicationId);
                        var userId = CreateAdminUser(context);
                        CreateAdminPassword(context, userId);
                        CreateAdminRole(context, repositoryId, userId);
                    }
                    catch { }
                }
            }

            return host;
        }

        #region Métodos de carga inicial de registros

        private static short CreatePlatformApplication(PlatformDataContext context)
        {
            // Creación de aplicación 'Aurora Platform'
            var application = context
                .Applications
                .FirstOrDefault(x => x.Code.Equals("DBB1F084-0E5C-488F-8990-EA1FDF223A94"));

            if (application == null)
            {
                application = new ApplicationData()
                {
                    Code = "DBB1F084-0E5C-488F-8990-EA1FDF223A94",
                    Name = "Aurora Platform",
                    Description = "Plataforma de aplicaciones Aurora Soft.",
                    CreatedDate = DateTime.Now
                };

                context.Applications.Add(application);
                context.SaveChanges();
            }

            return application.ApplicationId;
        }

        private static int CreatePlatformComponent(PlatformDataContext context, short applicationId)
        {
            // Creación de componente 'Aurora.Platform'
            var component = context
                .Components
                .FirstOrDefault(x => x.ApplicationId.Equals(applicationId) && x.Code.Equals("Aurora.Platform"));

            if (component == null)
            {
                component = new ComponentData()
                {
                    ApplicationId = applicationId,
                    Code = "Aurora.Platform",
                    Description = "Componente de Administración de Aurora Platform.",
                    CreatedDate = DateTime.Now
                };

                context.Components.Add(component);
                context.SaveChanges();
            }

            return component.ComponentId;
        }

        private static int CreatePlatformRepository(PlatformDataContext context, short applicationId)
        {
            // Creación de repositorio '8FDB73DC-7514-447A-8B28-0C09A45879E7'
            var repository = context
                .Repositories
                .FirstOrDefault(x => x.ApplicationId.Equals(applicationId) && x.Code.Equals("8FDB73DC-7514-447A-8B28-0C09A45879E7"));

            if (repository == null)
            {
                repository = new RepositoryData()
                {
                    ApplicationId = applicationId,
                    Code = "8FDB73DC-7514-447A-8B28-0C09A45879E7",
                    Description = "Repositorio de Administración de Aurora Platform.",
                    CreatedDate = DateTime.Now
                };

                context.Repositories.Add(repository);
                context.SaveChanges();
            }

            return repository.RepositoryId;
        }

        private static int CreateAdminUser(PlatformDataContext context)
        {
            // Creación de usuario ADMINISTRADOR DE LA PLATAFORMA
            var user = context
                .Users
                .FirstOrDefault(x => x.LoginName.Equals("admin"));

            if (user == null)
            {
                user = new UserData()
                {
                    LoginName = "admin",
                    Description = "ADMINISTRADOR DE LA PLATAFORMA",
                    Email = "admin@aurorasoft.ec",
                    IsDefaultUser = true,
                    IsActive = true,
                    CreatedBy = "BATCH-USR",
                    CreatedDate = DateTime.Now,
                    LastUpdatedBy = "BATCH-USR",
                    LastUpdatedDate = DateTime.Now
                };

                context.Users.Add(user);
                context.SaveChanges();
            }

            return user.UserId;
        }

        private static void CreateAdminPassword(PlatformDataContext context, int userId)
        {
            // Creación de contraseña de usuario ADMINISTRADOR DE LA PLATAFORMA
            var credential = context
                .UserCredentials
                .FirstOrDefault(x => x.UserId.Equals(userId));

            if (credential == null)
            {
                var password = Framework
                    .Cryptography
                    .EncryptionProvider
                    .Protect("admin", out string passwordControl);

                credential = new UserCredentialData()
                {
                    UserId = userId,
                    Password = password,
                    PasswordControl = passwordControl,
                    MustChange = false,
                    CreatedBy = "BATCH-USR",
                    CreatedDate = DateTime.Now,
                    LastUpdatedBy = "BATCH-USR",
                    LastUpdatedDate = DateTime.Now,
                    CredentialLogs = new List<UserCredentialLogData>
                    {
                        new UserCredentialLogData()
                        {
                            UserId = userId,
                            ChangeNumber = 1,
                            Password = password,
                            PasswordControl = passwordControl,
                            MustChange = false,
                            CreatedDate = DateTime.Now
                        }
                    }
                };

                context.UserCredentials.Add(credential);
                context.SaveChanges();
            }
        }

        private static void CreateAdminRole(PlatformDataContext context, int repositoryId, int userId)
        {
            // Creación de rol ADMINISTRADORES
            var role = context
                .Roles
                .FirstOrDefault(x => x.RepositoryId.Equals(repositoryId) && x.Name.Equals("ADMINISTRADORES"));

            if (role == null)
            {
                role = new RoleData()
                {
                    RepositoryId = repositoryId,
                    Name = "ADMINISTRADORES",
                    Description = "GRUPO DE ADMINISTRADORES DE LA PLATAFORMA",
                    IsDefaultRole = true,
                    IsActive = true,
                    CreatedBy = "BATCH-USR",
                    CreatedDate = DateTime.Now,
                    LastUpdatedBy = "BATCH-USR",
                    LastUpdatedDate = DateTime.Now,
                    Memberships = new List<UserMembershipData>
                    {
                        new UserMembershipData()
                        {
                            UserId = userId,
                            IsDefaultMembership = true,
                            IsActive = true,
                            CreatedBy = "BATCH-USR",
                            CreatedDate = DateTime.Now,
                            LastUpdatedBy = "BATCH-USR",
                            LastUpdatedDate = DateTime.Now
                        }
                    }
                };

                context.Roles.Add(role);
                context.SaveChanges();
            }
        }

        #endregion
    }
}