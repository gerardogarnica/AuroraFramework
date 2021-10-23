using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Platform.Repositories.Applications.Configurations
{
    public class ApplicationConfiguration
    {
        public ApplicationConfiguration(EntityTypeBuilder<Domain.Applications.Models.ApplicationData> builder)
        {
            builder.ToTable("Aplicacion", "APP");

            builder.HasKey(e => e.ApplicationId).HasName("PK_Aplicacion");

            builder.Property(e => e.ApplicationId).HasColumnName("IdAplicacion").IsRequired().HasColumnType("smallint").UseIdentityColumn();
            builder.Property(e => e.Code).HasColumnName("Codigo").IsRequired().HasColumnType("varchar(36)");
            builder.Property(e => e.Name).HasColumnName("Nombre").IsRequired().HasColumnType("varchar(50)");
            builder.Property(e => e.Description).HasColumnName("Descripcion").IsRequired().HasColumnType("varchar(100)");
            builder.Property(e => e.CreatedDate).HasColumnName("FechaCreacion").IsRequired().HasColumnType("datetime").HasDefaultValueSql("GETDATE()");

            builder.HasIndex(e => e.Code).IsUnique().HasDatabaseName("UK_Aplicacion");
        }
    }
}