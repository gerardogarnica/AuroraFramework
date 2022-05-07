using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Platform.Repositories.Security.Configurations
{
    public class UserMembershipConfiguration : IEntityTypeConfiguration<Domain.Security.Models.UserMembershipData>
    {
        public void Configure(EntityTypeBuilder<Domain.Security.Models.UserMembershipData> builder)
        {
            builder.ToTable("UserMembership", "SEC");

            builder.HasKey(e => e.MembershipId).HasName("PK_UserMembership");

            builder.Property(e => e.MembershipId).HasColumnName("MembershipId").IsRequired().HasColumnType("int").UseIdentityColumn();
            builder.Property(e => e.UserId).HasColumnName("UserId").IsRequired().HasColumnType("int");
            builder.Property(e => e.RoleId).HasColumnName("RoleId").IsRequired().HasColumnType("int");
            builder.Property(e => e.IsDefault).HasColumnName("IsDefault").IsRequired().HasColumnType("bit");
            builder.Property(e => e.IsActive).HasColumnName("IsActive").IsRequired().HasColumnType("bit");
            builder.Property(e => e.CreatedBy).HasColumnName("CreatedBy").IsRequired().HasColumnType("varchar(35)");
            builder.Property(e => e.CreatedDate).HasColumnName("CreatedDate").IsRequired().HasColumnType("datetime");
            builder.Property(e => e.LastUpdatedBy).HasColumnName("LastUpdatedBy").IsRequired().HasColumnType("varchar(35)");
            builder.Property(e => e.LastUpdatedDate).HasColumnName("LastUpdatedDate").IsRequired().HasColumnType("datetime");

            builder.HasIndex(e => new { e.UserId, e.RoleId }).IsUnique().HasDatabaseName("UK_UserMembership");

            builder.HasOne(e => e.User).WithMany(e => e.Memberships).HasForeignKey(e => e.UserId).HasConstraintName("FK_UserMembership_User");
            builder.HasOne(e => e.Role).WithMany(e => e.Memberships).HasForeignKey(e => e.RoleId).HasConstraintName("FK_UserMembership_Role");
        }
    }
}