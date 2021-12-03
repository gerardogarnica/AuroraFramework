using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Platform.Repositories.Security.Configurations
{
    public class UserMembershipConfiguration : IEntityTypeConfiguration<Domain.Security.Models.UserMembershipData>
    {
        public void Configure(EntityTypeBuilder<Domain.Security.Models.UserMembershipData> builder)
        {
            builder.ToTable("UsuarioPertenencia", "SEG");

            builder.HasKey(e => e.MembershipId).HasName("PK_UsuarioPertenencia");

            builder.Property(e => e.MembershipId).HasColumnName("IdPertenencia").IsRequired().HasColumnType("int").UseIdentityColumn();
            builder.Property(e => e.UserId).HasColumnName("IdUsuario").IsRequired().HasColumnType("int");
            builder.Property(e => e.RoleId).HasColumnName("IdRol").IsRequired().HasColumnType("int");
            builder.Property(e => e.IsDefaultMembership).HasColumnName("EsPredeterminado").IsRequired().HasColumnType("bit");
            builder.Property(e => e.IsActive).HasColumnName("EsActivo").IsRequired().HasColumnType("bit");
            builder.Property(e => e.CreatedBy).HasColumnName("UsuarioCreacion").IsRequired().HasColumnType("varchar(35)");
            builder.Property(e => e.CreatedDate).HasColumnName("FechaCreacion").IsRequired().HasColumnType("datetime");
            builder.Property(e => e.LastUpdatedBy).HasColumnName("UsuarioModificacion").IsRequired().HasColumnType("varchar(35)");
            builder.Property(e => e.LastUpdatedDate).HasColumnName("FechaModificacion").IsRequired().HasColumnType("datetime");

            builder.HasIndex(e => new { e.UserId, e.RoleId }).IsUnique().HasDatabaseName("UK_UsuarioPertenencia");

            builder.HasOne(e => e.User).WithMany(e => e.Memberships).HasForeignKey(e => e.UserId).HasConstraintName("FK_UsuarioPertenencia_Usuario");
            builder.HasOne(e => e.Role).WithMany(e => e.Memberships).HasForeignKey(e => e.RoleId).HasConstraintName("FK_UsuarioPertenencia_Rol");
        }
    }
}