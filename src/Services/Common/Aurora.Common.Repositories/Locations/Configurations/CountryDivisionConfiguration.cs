using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Common.Repositories.Locations.Configurations
{
    public class CountryDivisionConfiguration : IEntityTypeConfiguration<Domain.Locations.Models.CountryDivisionData>
    {
        public void Configure(EntityTypeBuilder<Domain.Locations.Models.CountryDivisionData> builder)
        {
            builder.ToTable("CountryDivision", "COM");

            builder.HasKey(e => e.DivisionId).HasName("PK_CountryDivision");

            builder.Property(e => e.DivisionId).HasColumnName("DivisionId").IsRequired().HasColumnType("smallint").UseIdentityColumn();
            builder.Property(e => e.CountryId).HasColumnName("CountryId").IsRequired().HasColumnType("smallint");
            builder.Property(e => e.Name).HasColumnName("Name").IsRequired().HasColumnType("varchar(50)");
            builder.Property(e => e.LevelNumber).HasColumnName("LevelNumber").IsRequired().HasColumnType("int");
            builder.Property(e => e.IsCityLevel).HasColumnName("IsCityLevel").IsRequired().HasColumnType("bit");
            builder.Property(e => e.IsActive).HasColumnName("IsActive").IsRequired().HasColumnType("bit");
            builder.Property(e => e.CreatedBy).HasColumnName("CreatedBy").IsRequired().HasColumnType("varchar(35)");
            builder.Property(e => e.CreatedDate).HasColumnName("CreatedDate").IsRequired().HasColumnType("datetime");
            builder.Property(e => e.LastUpdatedBy).HasColumnName("LastUpdatedBy").IsRequired().HasColumnType("varchar(35)");
            builder.Property(e => e.LastUpdatedDate).HasColumnName("LastUpdatedDate").IsRequired().HasColumnType("datetime");

            builder.HasOne(e => e.Country).WithMany(e => e.Divisions).HasForeignKey(e => e.CountryId).HasConstraintName("FK_CountryDivision_Country");
        }
    }
}