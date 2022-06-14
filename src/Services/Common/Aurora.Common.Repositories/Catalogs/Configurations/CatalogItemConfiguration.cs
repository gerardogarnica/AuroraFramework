using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Common.Repositories.Catalogs.Configurations
{
    public class CatalogItemConfiguration : IEntityTypeConfiguration<Domain.Catalogs.Models.CatalogItemData>
    {
        public void Configure(EntityTypeBuilder<Domain.Catalogs.Models.CatalogItemData> builder)
        {
            builder.ToTable("CatalogItem", "COM");

            builder.HasKey(e => e.CatalogItemId).HasName("PK_CatalogItem");

            builder.Property(e => e.CatalogItemId).HasColumnName("CatalogItemId").IsRequired().HasColumnType("int").UseIdentityColumn();
            builder.Property(e => e.CatalogId).HasColumnName("CatalogId").IsRequired().HasColumnType("int");
            builder.Property(e => e.Code).HasColumnName("Code").IsRequired().HasColumnType("varchar(40)");
            builder.Property(e => e.Description).HasColumnName("Description").IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(e => e.IsEditable).HasColumnName("IsEditable").IsRequired().HasColumnType("bit");
            builder.Property(e => e.IsActive).HasColumnName("IsActive").IsRequired().HasColumnType("bit");
            builder.Property(e => e.CreatedBy).HasColumnName("CreatedBy").IsRequired().HasColumnType("varchar(35)");
            builder.Property(e => e.CreatedDate).HasColumnName("CreatedDate").IsRequired().HasColumnType("datetime");
            builder.Property(e => e.LastUpdatedBy).HasColumnName("LastUpdatedBy").IsRequired().HasColumnType("varchar(35)");
            builder.Property(e => e.LastUpdatedDate).HasColumnName("LastUpdatedDate").IsRequired().HasColumnType("datetime");

            builder.HasIndex(e => new { e.CatalogId, e.Code }).IsUnique().HasDatabaseName("UK_CatalogItem");

            builder.HasOne(e => e.Catalog).WithMany(e => e.Items).HasForeignKey(e => e.CatalogId).HasConstraintName("FK_CatalogItem_Catalog");
        }
    }
}