using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Common.Repositories.Catalogs.Configurations
{
    public class CatalogConfiguration : IEntityTypeConfiguration<Domain.Catalogs.Models.CatalogData>
    {
        public void Configure(EntityTypeBuilder<Domain.Catalogs.Models.CatalogData> builder)
        {
            builder.ToTable("Catalog", "COM");

            builder.HasKey(e => e.CatalogId).HasName("PK_Catalog");

            builder.Property(e => e.CatalogId).HasColumnName("CatalogId").IsRequired().HasColumnType("int").UseIdentityColumn();
            builder.Property(e => e.Code).HasColumnName("Code").IsRequired().HasColumnType("varchar(40)");
            builder.Property(e => e.Name).HasColumnName("Name").IsRequired().HasColumnType("varchar(50)");
            builder.Property(e => e.Description).HasColumnName("Description").IsRequired().HasColumnType("nvarchar(200)");
            builder.Property(e => e.IsVisible).HasColumnName("IsVisible").IsRequired().HasColumnType("bit");
            builder.Property(e => e.IsEditable).HasColumnName("IsEditable").IsRequired().HasColumnType("bit");

            builder.HasIndex(e => e.Code).IsUnique().HasDatabaseName("UK_Catalog");
        }
    }
}