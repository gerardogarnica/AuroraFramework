using Aurora.Common.Domain.Catalogs.Models;
using Aurora.Common.Domain.Locations.Models;
using Aurora.Common.Domain.Settings.Models;
using Aurora.Common.Repositories.Catalogs.Configurations;
using Aurora.Common.Repositories.Locations.Configurations;
using Aurora.Common.Repositories.Settings.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Common.Repositories
{
    public class CommonDataContext : DbContext
    {
        #region Propiedades de conjuntos de datos

        public DbSet<CatalogData> Catalogs { get; set; }
        public DbSet<CatalogItemData> CatalogItems { get; set; }
        public DbSet<CountryData> Countries { get; set; }
        public DbSet<CountryDivisionData> Divisions { get; set; }
        public DbSet<LocationData> Locations { get; set; }
        public DbSet<AttributeSettingData> AttributeSettings { get; set; }
        public DbSet<AttributeValueData> AttributeValues { get; set; }

        #endregion

        #region Constructores de la clase

        public CommonDataContext(DbContextOptions<CommonDataContext> options)
            : base(options) { }

        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (builder.IsConfigured) return;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Database schema
            builder.HasDefaultSchema("COM");

            // Modelo de catálogos
            ModelCatalogsConfig(builder);

            // Modelo de localidades
            ModelLocationsConfig(builder);

            // Modelo de configuraciones
            ModelSettingsConfig(builder);
        }

        private void ModelCatalogsConfig(ModelBuilder builder)
        {
            // Secuencias

            // Configuraciones
            builder.ApplyConfiguration(new CatalogConfiguration());
            builder.ApplyConfiguration(new CatalogItemConfiguration());
        }

        private void ModelLocationsConfig(ModelBuilder builder)
        {
            // Secuencias

            // Configuraciones
            builder.ApplyConfiguration(new CountryConfiguration());
            builder.ApplyConfiguration(new CountryDivisionConfiguration());
            builder.ApplyConfiguration(new LocationConfiguration());
        }

        private void ModelSettingsConfig(ModelBuilder builder)
        {
            // Secuencias

            // Configuraciones
            builder.ApplyConfiguration(new AttributeSettingConfiguration());
            builder.ApplyConfiguration(new AttributeValueConfiguration());
        }
    }
}