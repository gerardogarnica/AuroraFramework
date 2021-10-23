using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Platform.Repositories.Security.Configurations
{
    public class UserConfiguration
    {
        public UserConfiguration(EntityTypeBuilder<Domain.Security.Models.UserData> builder)
        {
            builder.ToTable("Usuario", "SEG");

            builder.HasKey(e => e.UserId).HasName("PK_Usuario");

            builder.Property(e => e.UserId).HasColumnName("IdUsuario").IsRequired().HasColumnType("int").UseIdentityColumn();
            builder.Property(e => e.LoginName).HasColumnName("NombreUsuario").IsRequired().HasColumnType("varchar(35)");
            builder.Property(e => e.Description).HasColumnName("Descripcion").IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(e => e.Email).HasColumnName("CorreoElectronico").IsRequired().HasColumnType("varchar(100)");
            builder.Property(e => e.IsDefaultUser).HasColumnName("EsPredeterminado").IsRequired().HasColumnType("bit");
            builder.Property(e => e.IsActive).HasColumnName("EsActivo").IsRequired().HasColumnType("bit");
            builder.Property(e => e.CreatedBy).HasColumnName("UsuarioCreacion").IsRequired().HasColumnType("varchar(35)");
            builder.Property(e => e.CreatedDate).HasColumnName("FechaCreacion").IsRequired().HasColumnType("datetime");
            builder.Property(e => e.LastUpdatedBy).HasColumnName("UsuarioModificacion").IsRequired().HasColumnType("varchar(35)");
            builder.Property(e => e.LastUpdatedDate).HasColumnName("FechaModificacion").IsRequired().HasColumnType("datetime");

            builder.HasIndex(e => e.LoginName).IsUnique().HasDatabaseName("UK_Usuario");
        }
    }
}