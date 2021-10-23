using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Common.Repositories.Catalogs.Configurations
{
    public class CatalogConfiguration
    {
        public CatalogConfiguration(EntityTypeBuilder<Domain.Catalogs.Models.CatalogData> builder)
        {
            builder.ToTable("Catalogo", "COM");

            builder.HasKey(e => e.CatalogId).HasName("PK_Catalogo");

            builder.Property(e => e.CatalogId).HasColumnName("IdCatalogo").IsRequired().HasColumnType("int").UseIdentityColumn();
            builder.Property(e => e.Code).HasColumnName("Codigo").IsRequired().HasColumnType("varchar(40)");
            builder.Property(e => e.Name).HasColumnName("Nombre").IsRequired().HasColumnType("varchar(50)");
            builder.Property(e => e.Description).HasColumnName("Descripcion").IsRequired().HasColumnType("nvarchar(200)");
            builder.Property(e => e.IsVisible).HasColumnName("EsVisible").IsRequired().HasColumnType("bit");
            builder.Property(e => e.IsEditable).HasColumnName("EsEditable").IsRequired().HasColumnType("bit");

            builder.HasIndex(e => e.Code).IsUnique().HasDatabaseName("UK_Catalogo");
        }
    }
}