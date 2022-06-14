using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Common.Repositories.Locations.Configurations
{
    public class LocationConfiguration : IEntityTypeConfiguration<Domain.Locations.Models.LocationData>
    {
        public void Configure(EntityTypeBuilder<Domain.Locations.Models.LocationData> builder)
        {
            builder.ToTable("Location", "COM");

            builder.HasKey(e => e.LocationId).HasName("PK_Location");

            builder.Property(e => e.LocationId).HasColumnName("LocationId").IsRequired().HasColumnType("int").UseIdentityColumn();
            builder.Property(e => e.DivisionId).HasColumnName("DivisionId").IsRequired().HasColumnType("smallint");
            builder.Property(e => e.ParentLocationId).HasColumnName("ParentLocationId").HasColumnType("int");
            builder.Property(e => e.Name).HasColumnName("Name").IsRequired().HasColumnType("varchar(50)");
            builder.Property(e => e.Code).HasColumnName("Code").HasColumnType("varchar(5)");
            builder.Property(e => e.AlternativeCode).HasColumnName("AlternativeCode").HasColumnType("varchar(10)");
            builder.Property(e => e.IsActive).HasColumnName("IsActive").IsRequired().HasColumnType("bit");
            builder.Property(e => e.CreatedBy).HasColumnName("CreatedBy").IsRequired().HasColumnType("varchar(35)");
            builder.Property(e => e.CreatedDate).HasColumnName("CreatedDate").IsRequired().HasColumnType("datetime");
            builder.Property(e => e.LastUpdatedBy).HasColumnName("LastUpdatedBy").IsRequired().HasColumnType("varchar(35)");
            builder.Property(e => e.LastUpdatedDate).HasColumnName("LastUpdatedDate").IsRequired().HasColumnType("datetime");

            builder.HasOne(e => e.Division).WithMany(e => e.Locations).HasForeignKey(e => e.DivisionId).HasConstraintName("FK_Location_CountryDivision");
        }
    }
}