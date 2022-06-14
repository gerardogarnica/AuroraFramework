using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Common.Repositories.Locations.Configurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Domain.Locations.Models.CountryData>
    {
        public void Configure(EntityTypeBuilder<Domain.Locations.Models.CountryData> builder)
        {
            builder.ToTable("Country", "COM");

            builder.HasKey(e => e.CountryId).HasName("PK_Country");

            builder.Property(e => e.CountryId).HasColumnName("CountryId").IsRequired().HasColumnType("smallint").UseIdentityColumn();
            builder.Property(e => e.Name).HasColumnName("Name").IsRequired().HasColumnType("varchar(50)");
            builder.Property(e => e.OfficialName).HasColumnName("OfficialName").IsRequired().HasColumnType("varchar(100)");
            builder.Property(e => e.TwoLettersCode).HasColumnName("TwoLettersCode").IsRequired().HasColumnType("char(2)");
            builder.Property(e => e.ThreeLettersCode).HasColumnName("ThreeLettersCode").IsRequired().HasColumnType("char(3)");
            builder.Property(e => e.ThreeDigitsCode).HasColumnName("ThreeDigitsCode").IsRequired().HasColumnType("char(3)");
            builder.Property(e => e.InternetPrefix).HasColumnName("InternetPrefix").HasColumnType("char(3)");
            builder.Property(e => e.IsActive).HasColumnName("IsActive").IsRequired().HasColumnType("bit");

            builder.HasIndex(e => e.ThreeLettersCode).IsUnique().HasDatabaseName("UK_Country");
        }
    }
}