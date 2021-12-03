using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Common.Repositories.Locations.Configurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Domain.Locations.Models.CountryData>
    {
        public void Configure(EntityTypeBuilder<Domain.Locations.Models.CountryData> builder)
        {
            builder.ToTable("Pais", "COM");

            builder.HasKey(e => e.CountryId).HasName("PK_Pais");

            builder.Property(e => e.CountryId).HasColumnName("IdPais").IsRequired().HasColumnType("smallint").UseIdentityColumn();
            builder.Property(e => e.Name).HasColumnName("Nombre").IsRequired().HasColumnType("varchar(50)");
            builder.Property(e => e.OfficialName).HasColumnName("NombreOficial").IsRequired().HasColumnType("varchar(100)");
            builder.Property(e => e.TwoLettersCode).HasColumnName("CodigoDosLetras").IsRequired().HasColumnType("char(2)");
            builder.Property(e => e.ThreeLettersCode).HasColumnName("CodigoTresLetras").IsRequired().HasColumnType("char(3)");
            builder.Property(e => e.ThreeDigitsCode).HasColumnName("CodigoTresDigitos").IsRequired().HasColumnType("char(3)");
            builder.Property(e => e.InternetPrefix).HasColumnName("PrefijoInternet").HasColumnType("char(3)");
            builder.Property(e => e.IsActive).HasColumnName("EsActivo").IsRequired().HasColumnType("bit");

            builder.HasIndex(e => e.ThreeLettersCode).IsUnique().HasDatabaseName("UK_Pais");
        }
    }
}