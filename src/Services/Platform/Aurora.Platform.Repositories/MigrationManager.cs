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
        private const string cBatchUser = "BATCH-USR";

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
                        var userId = CreateAdminUser(context);
                        CreateAdminPassword(context, userId);
                        CreateAdminRole(context, userId);
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
                    HasCustomConfig = false,
                    CreatedDate = DateTime.Now
                };

                context.Applications.Add(application);
                context.SaveChanges();
            }

            return application.ApplicationId;
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
                    FirstName = "Administrador",
                    LastName = "",
                    Email = "admin@aurorasoft.ec",
                    IsDefault = true,
                    IsActive = true,
                    CreatedBy = cBatchUser,
                    CreatedDate = DateTime.Now,
                    LastUpdatedBy = cBatchUser,
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
                    CreatedBy = cBatchUser,
                    CreatedDate = DateTime.Now,
                    LastUpdatedBy = cBatchUser,
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

        private static void CreateAdminRole(PlatformDataContext context, int userId)
        {
            // Creación de rol ADMINISTRADORES
            var role = context
                .Roles
                .FirstOrDefault(x => x.IsGlobal && x.Name.Equals("Administradores"));

            if (role == null)
            {
                role = new RoleData()
                {
                    Name = "Administradores",
                    Description = "Administradores de la Plataforma",
                    IsDefault = true,
                    IsGlobal = true,
                    ProfileId = 0,
                    IsActive = true,
                    CreatedBy = cBatchUser,
                    CreatedDate = DateTime.Now,
                    LastUpdatedBy = cBatchUser,
                    LastUpdatedDate = DateTime.Now,
                    Memberships = new List<UserMembershipData>
                    {
                        new UserMembershipData()
                        {
                            UserId = userId,
                            IsDefault = true,
                            IsActive = true,
                            CreatedBy = cBatchUser,
                            CreatedDate = DateTime.Now,
                            LastUpdatedBy = cBatchUser,
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