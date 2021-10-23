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
        public DbSet<RepositoryData> Repositories { get; set; }
        public DbSet<RepositoryDetailData> RepositoryDetails { get; set; }
        public DbSet<UserData> Users { get; set; }
        public DbSet<UserCredentialData> UserCredentials { get; set; }
        public DbSet<UserCredentialLogData> UserCredentialLogs { get; set; }
        public DbSet<RoleData> Roles { get; set; }
        public DbSet<UserMembershipData> Memberships { get; set; }

        #endregion

        #region Constructores de la clase

        public PlatformDataContext(DbContextOptions<PlatformDataContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

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
            builder.HasDefaultSchema("SEG");

            // Secuencias
            //builder.HasSequence<int>("SeqUsuario", "SEG");

            // Configuraciones
            new UserConfiguration(builder.Entity<UserData>());
            new UserCredentialConfiguration(builder.Entity<UserCredentialData>());
            new UserCredentialLogConfiguration(builder.Entity<UserCredentialLogData>());
            new RoleConfiguration(builder.Entity<RoleData>());
            new UserMembershipConfiguration(builder.Entity<UserMembershipData>());
        }

        private void ModelApplicationsConfig(ModelBuilder builder)
        {
            // Database schema
            builder.HasDefaultSchema("APP");

            // Secuencias

            // Configuraciones
            new ApplicationConfiguration(builder.Entity<ApplicationData>());
            new ComponentConfiguration(builder.Entity<ComponentData>());
            new RepositoryConfiguration(builder.Entity<RepositoryData>());
            new RepositoryDetailConfiguration(builder.Entity<RepositoryDetailData>());
        }
    }
}