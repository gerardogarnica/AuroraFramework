using Aurora.Platform.Domain.Applications.Models;
using Aurora.Platform.Domain.Security.Models;
using Aurora.Platform.Repositories.Applications.Configurations;
using Aurora.Platform.Repositories.Security.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Platform.Repositories
{
    public class PlatformDataContext : DbContext
    {
        #region Propiedades de conjuntos de datos

        public DbSet<ApplicationData> Applications { get; set; }
        public DbSet<ComponentData> Components { get; set; }
        public DbSet<ProfileData> Profiles { get; set; }
        public DbSet<ConnectionData> Connections { get; set; }
        public DbSet<UserData> Users { get; set; }
        public DbSet<UserCredentialData> UserCredentials { get; set; }
        public DbSet<UserCredentialLogData> UserCredentialLogs { get; set; }
        public DbSet<RoleData> Roles { get; set; }
        public DbSet<UserMembershipData> Memberships { get; set; }

        #endregion

        #region Constructores de la clase

        public PlatformDataContext(DbContextOptions<PlatformDataContext> options)
            : base(options) { }

        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (builder.IsConfigured) return;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Security model
            ModelSecurityConfig(builder);

            // Applications model
            ModelApplicationsConfig(builder);
        }

        private void ModelSecurityConfig(ModelBuilder builder)
        {
            // Database schema
            builder.HasDefaultSchema("SEC");

            // Secuencias
            //builder.HasSequence<int>("SeqUsuario", "SEG");

            // Configuraciones
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserCredentialConfiguration());
            builder.ApplyConfiguration(new UserCredentialLogConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserMembershipConfiguration());
        }

        private void ModelApplicationsConfig(ModelBuilder builder)
        {
            // Database schema
            builder.HasDefaultSchema("APP");

            // Secuencias

            // Configuraciones
            builder.ApplyConfiguration(new ApplicationConfiguration());
            builder.ApplyConfiguration(new ComponentConfiguration());
            builder.ApplyConfiguration(new ProfileConfiguration());
            builder.ApplyConfiguration(new ConnectionConfiguration());
        }
    }
}