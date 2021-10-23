using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Platform.Repositories.Security.Configurations
{
    public class UserCredentialConfiguration
    {
        public UserCredentialConfiguration(EntityTypeBuilder<Domain.Security.Models.UserCredentialData> builder)
        {
            builder.ToTable("UsuarioCredencial", "SEG");

            builder.HasKey(e => e.UserId).HasName("PK_UsuarioCredencial");

            builder.Property(e => e.UserId).HasColumnName("IdUsuario").IsRequired().HasColumnType("int");
            builder.Property(e => e.Password).HasColumnName("Contrasena").IsRequired().HasColumnType("varchar(200)");
            builder.Property(e => e.PasswordControl).HasColumnName("ContrasenaControl").IsRequired().HasColumnType("varchar(500)");
            builder.Property(e => e.MustChange).HasColumnName("DebeCambiar").IsRequired().HasColumnType("bit");
            builder.Property(e => e.ExpirationDate).HasColumnName("FechaExpiracion").HasColumnType("datetime");
            builder.Property(e => e.CreatedBy).HasColumnName("UsuarioCreacion").IsRequired().HasColumnType("varchar(35)");
            builder.Property(e => e.CreatedDate).HasColumnName("FechaCreacion").IsRequired().HasColumnType("datetime");
            builder.Property(e => e.LastUpdatedBy).HasColumnName("UsuarioModificacion").IsRequired().HasColumnType("varchar(35)");
            builder.Property(e => e.LastUpdatedDate).HasColumnName("FechaModificacion").IsRequired().HasColumnType("datetime");

            builder.HasOne(e => e.User).WithOne(e => e.Credential).HasConstraintName("FK_UsuarioCredencial_Usuario");
        }
    }
}