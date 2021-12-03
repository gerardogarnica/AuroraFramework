using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Platform.Repositories.Applications.Configurations
{
    public class RepositoryConfiguration : IEntityTypeConfiguration<Domain.Applications.Models.RepositoryData>
    {
        public void Configure(EntityTypeBuilder<Domain.Applications.Models.RepositoryData> builder)
        {
            builder.ToTable("Repositorio", "APP");

            builder.HasKey(e => e.RepositoryId).HasName("PK_Repositorio");

            builder.Property(e => e.RepositoryId).HasColumnName("IdRepositorio").IsRequired().HasColumnType("int").UseIdentityColumn();
            builder.Property(e => e.ApplicationId).HasColumnName("IdAplicacion").IsRequired().HasColumnType("smallint");
            builder.Property(e => e.Code).HasColumnName("Codigo").IsRequired().HasColumnType("varchar(36)");
            builder.Property(e => e.Description).HasColumnName("Descripcion").IsRequired().HasColumnType("varchar(100)");
            builder.Property(e => e.CreatedDate).HasColumnName("FechaCreacion").IsRequired().HasColumnType("datetime").HasDefaultValueSql("GETDATE()");

            builder.HasIndex(e => new { e.ApplicationId, e.Code }).IsUnique().HasDatabaseName("UK_Repositorio");

            builder.HasOne(e => e.Application).WithMany(e => e.Repositories).HasForeignKey(e => e.ApplicationId).HasConstraintName("FK_Repositorio_Aplicacion");
        }
    }
}