using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Platform.Repositories.Applications.Configurations
{
    public class RepositoryDetailConfiguration : IEntityTypeConfiguration<Domain.Applications.Models.RepositoryDetailData>
    {
        public void Configure(EntityTypeBuilder<Domain.Applications.Models.RepositoryDetailData> builder)
        {
            builder.ToTable("RepositorioDetalle", "APP");

            builder.HasKey(e => e.RepositoryDetailId).HasName("PK_RepositorioDetalle");

            builder.Property(e => e.RepositoryDetailId).HasColumnName("IdRepositorioDetalle").IsRequired().HasColumnType("int").UseIdentityColumn();
            builder.Property(e => e.RepositoryId).HasColumnName("IdRepositorio").IsRequired().HasColumnType("int");
            builder.Property(e => e.ComponentId).HasColumnName("IdComponente").IsRequired().HasColumnType("int");
            builder.Property(e => e.StringData).HasColumnName("Cadena").IsRequired().HasColumnType("varchar(1000)");
            builder.Property(e => e.CreatedBy).HasColumnName("UsuarioCreacion").IsRequired().HasColumnType("varchar(35)");
            builder.Property(e => e.CreatedDate).HasColumnName("FechaCreacion").IsRequired().HasColumnType("datetime");
            builder.Property(e => e.LastUpdatedBy).HasColumnName("UsuarioModificacion").IsRequired().HasColumnType("varchar(35)");
            builder.Property(e => e.LastUpdatedDate).HasColumnName("FechaModificacion").IsRequired().HasColumnType("datetime");

            builder.HasIndex(e => new { e.RepositoryId, e.ComponentId }).IsUnique().HasDatabaseName("UK_RepositorioDetalle");

            builder.HasOne(e => e.Repository).WithMany(e => e.Details).HasForeignKey(e => e.RepositoryId).HasConstraintName("FK_RepositorioDetalle_Repositorio");
        }
    }
}