using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Platform.Repositories.Security.Configurations
{
    public class UserCredentialLogConfiguration : IEntityTypeConfiguration<Domain.Security.Models.UserCredentialLogData>
    {
        public void Configure(EntityTypeBuilder<Domain.Security.Models.UserCredentialLogData> builder)
        {
            builder.ToTable("UsuarioCredencialHistorial", "SEG");

            builder.HasKey(e => e.LogId).HasName("PK_UsuarioCredencialHistorial");

            builder.Property(e => e.LogId).HasColumnName("IdHistorial").IsRequired().HasColumnType("int").UseIdentityColumn();
            builder.Property(e => e.UserId).HasColumnName("IdUsuario").IsRequired().HasColumnType("int");
            builder.Property(e => e.ChangeNumber).HasColumnName("NumeroCambio").IsRequired().HasColumnType("int");
            builder.Property(e => e.Password).HasColumnName("Contrasena").IsRequired().HasColumnType("varchar(200)");
            builder.Property(e => e.PasswordControl).HasColumnName("ContrasenaControl").IsRequired().HasColumnType("varchar(500)");
            builder.Property(e => e.MustChange).HasColumnName("DebeCambiar").IsRequired().HasColumnType("bit");
            builder.Property(e => e.ExpirationDate).HasColumnName("FechaExpiracion").HasColumnType("datetime");
            builder.Property(e => e.CreatedDate).HasColumnName("FechaCreacion").IsRequired().HasColumnType("datetime");
            builder.Property(e => e.EndDate).HasColumnName("FechaFinalizacion").HasColumnType("datetime");

            builder.HasOne(e => e.Credential).WithMany(e => e.CredentialLogs).HasForeignKey(e => e.UserId).HasConstraintName("FK_UsuarioCredencialHistorial_UsuarioCredencial");
        }
    }
}