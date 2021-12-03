using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Platform.Repositories.Security.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Domain.Security.Models.RoleData>
    {
        public void Configure(EntityTypeBuilder<Domain.Security.Models.RoleData> builder)
        {
            builder.ToTable("Rol", "SEG");

            builder.HasKey(e => e.RoleId).HasName("PK_Rol");

            builder.Property(e => e.RoleId).HasColumnName("IdRol").IsRequired().HasColumnType("int").UseIdentityColumn();
            builder.Property(e => e.RepositoryId).HasColumnName("IdRepositorio").IsRequired().HasColumnType("int");
            builder.Property(e => e.Name).HasColumnName("Nombre").IsRequired().HasColumnType("varchar(50)");
            builder.Property(e => e.Description).HasColumnName("Descripcion").IsRequired().HasColumnType("varchar(100)");
            builder.Property(e => e.IsDefaultRole).HasColumnName("EsPredeterminado").IsRequired().HasColumnType("bit");
            builder.Property(e => e.IsActive).HasColumnName("EsActivo").IsRequired().HasColumnType("bit");
            builder.Property(e => e.CreatedBy).HasColumnName("UsuarioCreacion").IsRequired().HasColumnType("varchar(35)");
            builder.Property(e => e.CreatedDate).HasColumnName("FechaCreacion").IsRequired().HasColumnType("datetime");
            builder.Property(e => e.LastUpdatedBy).HasColumnName("UsuarioModificacion").IsRequired().HasColumnType("varchar(35)");
            builder.Property(e => e.LastUpdatedDate).HasColumnName("FechaModificacion").IsRequired().HasColumnType("datetime");

            builder.HasIndex(e => new { e.RepositoryId, e.Name }).IsUnique().HasDatabaseName("UK_Rol");
        }
    }
}