using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Common.Repositories.Catalogs.Configurations
{
    public class CatalogItemConfiguration
    {
        public CatalogItemConfiguration(EntityTypeBuilder<Domain.Catalogs.Models.CatalogItemData> builder)
        {
            builder.ToTable("CatalogoItem", "COM");

            builder.HasKey(e => e.CatalogItemId).HasName("PK_CatalogoItem");

            builder.Property(e => e.CatalogItemId).HasColumnName("IdCatalogoItem").IsRequired().HasColumnType("int").UseIdentityColumn();
            builder.Property(e => e.CatalogId).HasColumnName("IdCatalogo").IsRequired().HasColumnType("int");
            builder.Property(e => e.Code).HasColumnName("Codigo").IsRequired().HasColumnType("varchar(40)");
            builder.Property(e => e.Description).HasColumnName("Descripcion").IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(e => e.IsEditable).HasColumnName("EsEditable").IsRequired().HasColumnType("bit");
            builder.Property(e => e.IsActive).HasColumnName("EsActivo").IsRequired().HasColumnType("bit");
            builder.Property(e => e.CreatedBy).HasColumnName("UsuarioCreacion").IsRequired().HasColumnType("varchar(35)");
            builder.Property(e => e.CreatedDate).HasColumnName("FechaCreacion").IsRequired().HasColumnType("datetime");
            builder.Property(e => e.LastUpdatedBy).HasColumnName("UsuarioModificacion").IsRequired().HasColumnType("varchar(35)");
            builder.Property(e => e.LastUpdatedDate).HasColumnName("FechaModificacion").IsRequired().HasColumnType("datetime");

            builder.HasIndex(e => new { e.CatalogId, e.Code }).IsUnique().HasDatabaseName("UK_CatalogoItem");

            builder.HasOne(e => e.Catalog).WithMany(e => e.Items).HasForeignKey(e => e.CatalogId).HasConstraintName("FK_CatalogoItem_Catalogo");
        }
    }
}